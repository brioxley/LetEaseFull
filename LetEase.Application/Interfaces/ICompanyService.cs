using LetEase.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Application.Interfaces
{
	public interface ICompanyService
	{
		Task<CompanyDto> GetCompanyByIdAsync(int id);
		Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
		Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto createCompanyDto);
		Task UpdateCompanyAsync(UpdateCompanyDto updateCompanyDto);
		Task DeleteCompanyAsync(int id);
	}
}
