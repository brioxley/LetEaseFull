using LetEase.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Application.Interfaces
{
	public interface ICompanyRepository
	{
		Task<Company> GetByIdAsync(int id);
		Task<IEnumerable<Company>> GetAllAsync();
		Task<Company> CreateAsync(Company company);
		Task UpdateAsync(Company company);
		Task DeleteAsync(int id);
	}
}
