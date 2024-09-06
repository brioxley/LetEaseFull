using LetEase.Application.Interfaces;
using LetEase.Domain.Entities;
using LetEase.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Infrastructure.Repositories
{
	public class PropertyRepository : IPropertyRepository
	{
		private readonly ApplicationDbContext _context;

		public PropertyRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Property> GetByIdAsync(int id)
		{
			return await _context.Properties.FindAsync(id);
		}

		public async Task<IEnumerable<Property>> GetAllAsync()
		{
			return await _context.Properties.ToListAsync();
		}

		public async Task<Property> CreateAsync(Property property)
		{
			await _context.Properties.AddAsync(property);
			await _context.SaveChangesAsync();
			return property;
		}

		public async Task UpdateAsync(Property property)
		{
			_context.Entry(property).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var property = await _context.Properties.FindAsync(id);
			if (property != null)
			{
				_context.Properties.Remove(property);
				await _context.SaveChangesAsync();
			}
		}
	}
}
