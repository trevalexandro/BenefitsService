using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = BenefitsService.Domain.Entities;
using Aggregates = BenefitsService.Domain.Aggregates;
using BenefitsService.Application.DTO;

namespace BenefitsService.Application
{
    public static class MappingConfig
    {
        public static TypeAdapterConfig GetMappings()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Entities.Dependent, Dependent>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Age, src => src.Age)
                .TwoWays();


            config.NewConfig<Aggregates.EmployeeAggregate, Employee>()
                .Map(src => src.Id, dest => dest.Id)
                .Map(src => src.FirstName, dest => dest.FirstName)
                .Map(src => src.LastName, dest => dest.LastName)
                .Map(src => src.DateOfBirth, dest => dest.DateOfBirth)
                .Map(dest => dest.AnnualGrossSalary, src => src.AnnualGrossSalary)
                .TwoWays();

            return config;
        }
    }
}
