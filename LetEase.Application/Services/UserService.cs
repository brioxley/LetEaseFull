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

namespace LetEase.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_configuration = configuration;
		}

		public async Task<UserDto> GetUserByIdAsync(int id)
		{
			var user = await _userRepository.GetByIdAsync(id);
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
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
			user.DateRegistered = DateTime.UtcNow;

			var createdUser = await _userRepository.CreateAsync(user);
			return _mapper.Map<UserDto>(createdUser);
		}

		public async Task UpdateUserAsync(UpdateUserDto updateUserDto)
		{
			var user = await _userRepository.GetByIdAsync(updateUserDto.Id);
			if (user == null)
				throw new KeyNotFoundException($"User with ID {updateUserDto.Id} not found.");

			_mapper.Map(updateUserDto, user);
			await _userRepository.UpdateAsync(user);
		}

		public async Task DeleteUserAsync(int id)
		{
			await _userRepository.DeleteAsync(id);
		}

		public async Task<bool> ValidateUserAsync(string email, string password)
		{
			var user = await _userRepository.GetByEmailAsync(email);
			if (user == null)
				return false;

			return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
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
	}
}
