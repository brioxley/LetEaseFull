using LetEase.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Application.Interfaces
{
	public interface IPropertyRepository
	{
		Task<Property> GetByIdAsync(int id);
		Task<IEnumerable<Property>> GetAllAsync();
		Task<Property> CreateAsync(Property property);
		Task UpdateAsync(Property property);
		Task DeleteAsync(int id);
	}
}
