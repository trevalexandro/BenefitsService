using BenefitsService.Application.DTO;
using BenefitsService.Domain.Aggregates;

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
            dto.Dependents = entity.Dependents.ToList().Select(dependent => dependent.ToDto());
            return dto;
        }

        public static EmployeeAggregate ToEntity(this EmployeeUpdate dto, EmployeeAggregate existingEmployee)
        {
            existingEmployee.FirstName = dto.FirstName ?? existingEmployee.FirstName;
            existingEmployee.LastName = dto.LastName ?? existingEmployee.LastName;
            existingEmployee.DateOfBirth = dto.DateOfBirth ?? existingEmployee.DateOfBirth;
            existingEmployee.AnnualGrossSalary = dto.AnnualGrossSalary ?? existingEmployee.AnnualGrossSalary;
            return existingEmployee;
        }

        public static EmployeeAggregate ToEntity(this Employee dto)
        {
            return new EmployeeAggregate
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                AnnualGrossSalary = dto.AnnualGrossSalary
            };
        }
    }
}
