﻿using LetEase.Application.DTOs;
using System.Threading.Tasks;

namespace LetEase.Application.Interfaces
{
	public interface IAuthService
	{
		Task<UserDto> RegisterUserAsync(RegisterUserDto registerUserDto);
		Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
		Task<bool> ValidateTokenAsync(string token);
	}
}
