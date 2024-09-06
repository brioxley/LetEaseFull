using LetEase.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Application.Interfaces
{
	public interface IUserService
	{
		Task<UserDto> GetUserByIdAsync(int id);
		Task<UserDto> GetUserByEmailAsync(string email);
		Task<IEnumerable<UserDto>> GetAllUsersAsync();
		Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
		Task UpdateUserAsync(UpdateUserDto updateUserDto);
		Task DeleteUserAsync(int id);
		Task<bool> ValidateUserAsync(string email, string password);
		Task<string> GenerateJwtTokenAsync(UserDto user);
	}
}
