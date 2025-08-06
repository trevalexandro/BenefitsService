using BenefitsService.Application.DTO;
using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Enums;
using Entities = BenefitsService.Domain.Entities;

namespace BenefitsService.Application.Extensions
{
    public static class DependentExtensions
    {
        public static Dependent ToDto(this Entities.Dependent entity)
        {
            return new Dependent
            {
                Id = entity.Id,
                Age = entity.Age,
                Relationship = Enum.GetName(entity.Relationship) ?? string.Empty,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }

        public static Entities.Dependent ToEntity(this NewDependent dto, EmployeeAggregate parent, 
            Relationship relationship)
        {
            return new Entities.Dependent
            {
                Id = default,
                Age = dto.Age,
                Relationship = relationship,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Parent = parent
            };
        }
    }
}
