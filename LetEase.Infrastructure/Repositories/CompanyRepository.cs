﻿using LetEase.Application.Interfaces;
using LetEase.Domain.Entities;
using LetEase.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Infrastructure.Repositories
{
	public class CompanyRepository : ICompanyRepository
	{
		private readonly ApplicationDbContext _context;

		public CompanyRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Company> GetByIdAsync(int id)
		{
			return await _context.Companies.FindAsync(id);
		}

		public async Task<IEnumerable<Company>> GetAllAsync()
		{
			return await _context.Companies.ToListAsync();
		}

		public async Task<Company> CreateAsync(Company company)
		{
			await _context.Companies.AddAsync(company);
			await _context.SaveChangesAsync();
			return company;
		}

		public async Task UpdateAsync(Company company)
		{
			_context.Entry(company).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var company = await _context.Companies.FindAsync(id);
			if (company != null)
			{
				_context.Companies.Remove(company);
				await _context.SaveChangesAsync();
			}
		}
	}
}
