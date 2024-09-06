using AutoMapper;
using LetEase.Application.DTOs;
using LetEase.Application.Interfaces;
using LetEase.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetEase.Application.Services
{
	public class PropertyService : IPropertyService
	{
		private readonly IPropertyRepository _propertyRepository;
		private readonly IMapper _mapper;

		public PropertyService(IPropertyRepository propertyRepository, IMapper mapper)
		{
			_propertyRepository = propertyRepository;
			_mapper = mapper;
		}

		public async Task<PropertyDto> GetPropertyByIdAsync(int id)
		{
			var property = await _propertyRepository.GetByIdAsync(id);
			return _mapper.Map<PropertyDto>(property);
		}

		public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync()
		{
			var properties = await _propertyRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<PropertyDto>>(properties);
		}

		public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createPropertyDto)
		{
			var property = _mapper.Map<Property>(createPropertyDto);
			var createdProperty = await _propertyRepository.CreateAsync(property);
			return _mapper.Map<PropertyDto>(createdProperty);
		}

		public async Task UpdatePropertyAsync(UpdatePropertyDto updatePropertyDto)
		{
			var property = await _propertyRepository.GetByIdAsync(updatePropertyDto.Id);
			if (property == null)
				throw new KeyNotFoundException($"Property with ID {updatePropertyDto.Id} not found.");

			_mapper.Map(updatePropertyDto, property);
			await _propertyRepository.UpdateAsync(property);
		}

		public async Task DeletePropertyAsync(int id)
		{
			await _propertyRepository.DeleteAsync(id);
		}
	}
}
