using LetEase.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Application.Interfaces
{
	public interface IPropertyService
	{
		Task<PropertyDto> GetPropertyByIdAsync(int id);
		Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync();
		Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createPropertyDto);
		Task UpdatePropertyAsync(UpdatePropertyDto updatePropertyDto);
		Task DeletePropertyAsync(int id);
	}
}
