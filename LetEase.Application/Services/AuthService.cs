using LetEase.Application.DTOs;
using LetEase.Application.Interfaces;
using LetEase.Domain.Entities;
using LetEase.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;




namespace LetEase.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userRepository = userRepository;
			_configuration = configuration;
			_mapper = mapper;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<UserDto> RegisterUserAsync(RegisterUserDto registerUserDto)
		{
			// Check if user already exists
			if (await _userRepository.GetByEmailAsync(registerUserDto.Email) != null)
			{
				throw new ApplicationException("User with this email already exists.");
			}

			// Create new user
			var user = new User
			{
				Username = registerUserDto.Username,
				Email = registerUserDto.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
				FirstName = registerUserDto.FirstName,
				LastName = registerUserDto.LastName,
				DateRegistered = DateTime.UtcNow,
				EmailConfirmed = false, // You might want to implement email confirmation later
				Type = registerUserDto.Type,
				Role = registerUserDto.Role ?? UserRole.Client, // Default to Client if not specified
				CompanyId = registerUserDto.CompanyId
			};

			await _userRepository.AddAsync(user);

			return new UserDto
			{
				Id = user.Id,
				Username = user.Username,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Type = user.Type,
				Role = user.Role,
				CompanyId = user.CompanyId
			};
		}

		public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
		{
			var user = await _userRepository.GetByEmailAsync(loginDto.Email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
			{
				throw new ApplicationException("Invalid email or password.");
			}

			var token = GenerateJwtToken(user);

			return new LoginResponseDto
			{
				Token = token,
				User = new UserDto
				{
					Id = user.Id,
					Username = user.Username,
					Email = user.Email,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Type = user.Type,
					Role = user.Role,
					CompanyId = user.CompanyId
				}
			};
		}

		public Task<bool> ValidateTokenAsync(string token)
		{
			// Implement token validation logic here
			// For now, we'll just return true
			return Task.FromResult(true);
		}

		public async Task<UserDto> GetUserByIdAsync(int userId)
		{
			var user = await _userRepository.GetByIdAsync(userId);
			if (user == null)
			{
				throw new ApplicationException("User not found.");
			}

			return _mapper.Map<UserDto>(user);
		}

		private string GenerateJwtToken(User user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.Type.ToString()),
				new Claim("UserRole", user.Role.ToString())
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

		public async Task<(bool Succeeded, string Token)> RefreshTokenAsync(string token)
		{
			var principal = GetPrincipalFromExpiredToken(token);
			if (principal == null)
			{
				return (false, null);
			}

			var user = await _userManager.FindByNameAsync(principal.Identity.Name);
			if (user == null)
			{
				return (false, null);
			}

			var newToken = GenerateJwtToken(user);
			return (true, newToken);
		}

		private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
				ValidateLifetime = false
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken securityToken;
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
			var jwtSecurityToken = securityToken as JwtSecurityToken;
			if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Invalid token");

			return principal;
		}
	}
}
