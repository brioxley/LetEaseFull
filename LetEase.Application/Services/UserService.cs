using AutoMapper;
using LetEase.Application.DTOs;
using LetEase.Application.Interfaces;
using LetEase.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

namespace LetEase.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly IEmailService _emailService;
		private readonly UserManager<User> _userManager;


		public UserService(IUserRepository userRepository, 
			IMapper mapper, 
			IConfiguration configuration, 
			IEmailService emailService,
			UserManager<User> userManager)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_configuration = configuration;
			_userManager = userManager;
		}

		public async Task<UserDto> GetUserByIdAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> GetUserByEmailAsync(string email)
		{
			var user = await _userRepository.GetByEmailAsync(email);
			return _mapper.Map<UserDto>(user);
		}

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
		{
			var users = await _userRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
		{
			var user = _mapper.Map<User>(createUserDto);
			var result = await _userManager.CreateAsync(user, createUserDto.Password);

			   if (!result.Succeeded)
				   {
				throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));
				   }
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
			user.DateRegistered = DateTime.UtcNow;

			return _mapper.Map<UserDto>(user);
		}

		public async Task UpdateUserAsync(UpdateUserDto updateUserDto)
		{
			var user = await _userManager.FindByIdAsync(updateUserDto.Id);
			if (user == null)
				throw new KeyNotFoundException($"User with ID {updateUserDto.Id} not found.");

			_mapper.Map(updateUserDto, user);
			await _userManager.UpdateAsync(user);
		}

		public async Task DeleteUserAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				await _userManager.DeleteAsync(user);
			}
		}

		public async Task<bool> ValidateUserAsync(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return false;
			return await _userManager.CheckPasswordAsync(user, password);
		}

		public async Task<string> GenerateJwtTokenAsync(UserDto user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		public async Task<UserDto> RegisterUserAsync(RegisterUserDto registerUserDto)
		{
			// Check if user exists
			var existingUser = await _userRepository.GetByEmailAsync(registerUserDto.Email);
			if (existingUser != null)
			{
				throw new ApplicationException("User with this email already exists.");
			}

			// Create new user
			var user = new User
			{
				Email = registerUserDto.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
				EmailConfirmed = false,
				EmailConfirmationToken = GenerateEmailConfirmationToken(),
				EmailConfirmationTokenExpires = DateTime.UtcNow.AddHours(24)
			};

			await _userRepository.CreateAsync(user);

			// Send confirmation email
			await SendConfirmationEmailAsync(user);

			return new UserDto { /* map user properties */ };
		}

		public async Task<bool> ConfirmEmailAsync(string email, string token)
		{
			var user = await _userRepository.GetByEmailAsync(email);
			if (user == null || user.EmailConfirmationToken != token || user.EmailConfirmationTokenExpires < DateTime.UtcNow)
			{
				return false;
			}

			user.EmailConfirmed = true;
			user.EmailConfirmationToken = null;
			user.EmailConfirmationTokenExpires = null;

			await _userRepository.UpdateAsync(user);
			return true;
		}

		private string GenerateEmailConfirmationToken()
		{
			return Guid.NewGuid().ToString();
		}

		private async Task SendConfirmationEmailAsync(User user)
		{
			var confirmationLink = $"{_configuration["AppUrl"]}/confirm-email?email={user.Email}&token={user.EmailConfirmationToken}";
			var emailBody = $"Please confirm your email by clicking this link: {confirmationLink}";
			await _emailService.SendEmailAsync(user.Email, "Confirm your email", emailBody);
		}

	}
}
