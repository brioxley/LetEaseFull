using LetEase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Application.Interfaces
{
	public interface IUserRepository
	{
		Task<User> GetByEmailAsync(string email);
		Task AddAsync(User user);
		Task<User> GetByIdAsync(int id);
		Task<IEnumerable<User>> GetAllAsync();
		Task<User> CreateAsync(User user);
		Task UpdateAsync(User user);
		Task DeleteAsync(int id);
	}
}
