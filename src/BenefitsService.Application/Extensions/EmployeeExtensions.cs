using BenefitsService.Application.DTO;
using BenefitsService.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Application.Extensions
{
    public static class EmployeeExtensions
    {
        public static Employee ToDtoWithoutNetPay(this EmployeeAggregate entity)
        {
            var employee = new Employee
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                AnnualGrossSalary = entity.AnnualGrossSalary
            };

            return employee;
        }

        public static Employee ToFullDto(this EmployeeAggregate entity)
        {
            var dto = entity.ToDtoWithoutNetPay();
            dto.NetSalary = entity.CalculateNetSalary();
            dto.NetMonthlyPay = dto.NetSalary / 12;
            dto.NetBiweeklyPay = dto.NetMonthlyPay / 2;
            return dto;
        }

        public static EmployeeAggregate ToEntity(this Employee dto)
        {
            return new EmployeeAggregate
            {
                Id = dto.Id ?? default,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                AnnualGrossSalary = dto.AnnualGrossSalary
            };
        }
    }
}
