using AutoMapper;
using LetEase.Application.DTOs;
using LetEase.Application.Interfaces;
using LetEase.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Application.Services
{
	public class CompanyService : ICompanyService
	{
		private readonly ICompanyRepository _companyRepository;
		private readonly IMapper _mapper;

		public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
		{
			_companyRepository = companyRepository;
			_mapper = mapper;
		}

		public async Task<CompanyDto> GetCompanyByIdAsync(int id)
		{
			var company = await _companyRepository.GetByIdAsync(id);
			return _mapper.Map<CompanyDto>(company);
		}

		public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
		{
			var companies = await _companyRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<CompanyDto>>(companies);
		}

		public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto createCompanyDto)
		{
			var company = _mapper.Map<Company>(createCompanyDto);
			var createdCompany = await _companyRepository.CreateAsync(company);
			return _mapper.Map<CompanyDto>(createdCompany);
		}

		public async Task UpdateCompanyAsync(UpdateCompanyDto updateCompanyDto)
		{
			var company = await _companyRepository.GetByIdAsync(updateCompanyDto.Id);
			if (company == null)
				throw new KeyNotFoundException($"Company with ID {updateCompanyDto.Id} not found.");

			_mapper.Map(updateCompanyDto, company);
			await _companyRepository.UpdateAsync(company);
		}

		public async Task DeleteCompanyAsync(int id)
		{
			await _companyRepository.DeleteAsync(id);
		}
	}
}
