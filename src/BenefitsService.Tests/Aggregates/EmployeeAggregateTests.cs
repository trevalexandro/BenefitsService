using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Entities;
using BenefitsService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Tests.Aggregates
{
    public class EmployeeAggregateTests
    {
        public static TheoryData<bool, EmployeeAggregate> ValidateEntityTheoryOne => new()
        {
            {
                true,
                new EmployeeAggregate
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateOnly.MinValue,
                    AnnualGrossSalary = 100000,
                    Id = default,
                    Dependents =
                    [
                        new() 
                        {
                            Age = 30,
                            FirstName = "Jane",
                            LastName = "Doe",
                            Relationship = Relationship.Spouse,
                            Id = default,
                            Parent = new EmployeeAggregate
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                DateOfBirth = DateOnly.MinValue,
                                AnnualGrossSalary = 100000,
                                Id = default
                            }
                        },
                        new()
                        {
                            Age = 11,
                            FirstName = "Marcia",
                            LastName = "Doe",
                            Relationship = Relationship.Child,
                            Id = default,
                            Parent = new EmployeeAggregate
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                DateOfBirth = DateOnly.MinValue,
                                AnnualGrossSalary = 100000,
                                Id = default
                            }
                        }
                    ]
                }
            }
        };

        public static TheoryData<bool, EmployeeAggregate> ValidateEntityTheoryTwo => new()
        {
            {
                false,
                new EmployeeAggregate
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateOnly.MinValue,
                    AnnualGrossSalary = 100000,
                    Id = default,
                    Dependents =
                    [
                        new()
                        {
                            Age = 30,
                            FirstName = "Jane",
                            LastName = "Doe",
                            Relationship = Relationship.Spouse,
                            Id = default,
                            Parent = new EmployeeAggregate
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                DateOfBirth = DateOnly.MinValue,
                                AnnualGrossSalary = 100000,
                                Id = default
                            }
                        },
                        new()
                        {
                            Age = 25,
                            FirstName = "Marcia",
                            LastName = "Doe",
                            Relationship = Relationship.DomesticPartner,
                            Id = default,
                            Parent = new EmployeeAggregate
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                DateOfBirth = DateOnly.MinValue,
                                AnnualGrossSalary = 100000,
                                Id = default
                            }
                        }
                    ]
                }
            }
        };

        public static TheoryData<bool, EmployeeAggregate> ValidateEntityTheoryThree => new()
        {
            {
                false,
                new EmployeeAggregate
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateOnly.MinValue,
                    AnnualGrossSalary = 100000,
                    Id = default,
                    Dependents =
                    [
                        new()
                        {
                            Age = 30,
                            FirstName = "Jane",
                            LastName = "Doe",
                            Relationship = Relationship.Spouse,
                            Id = default,
                            Parent = new EmployeeAggregate
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                DateOfBirth = DateOnly.MinValue,
                                AnnualGrossSalary = 100000,
                                Id = default
                            }
                        },
                        new()
                        {
                            Age = 11,
                            FirstName = "Marcia",
                            LastName = "Doe",
                            Relationship = Relationship.Spouse,
                            Id = default,
                            Parent = new EmployeeAggregate
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                DateOfBirth = DateOnly.MinValue,
                                AnnualGrossSalary = 100000,
                                Id = default
                            }
                        }
                    ]
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidateEntityTheoryOne))]
        [MemberData(nameof(ValidateEntityTheoryTwo))]
        [MemberData(nameof(ValidateEntityTheoryThree))]
        public void ValidateEntityTest(bool expected, EmployeeAggregate entity)
        {
            (bool Valid, string Error) = entity.ValidateEntity();
            
            Assert.Equal(expected, Valid);
            if (!Valid)
            {
                Assert.NotEqual(string.Empty, Error);
            }
        }

        public static TheoryData<decimal, EmployeeAggregate> CalculateNetSalaryTheoryOne => new()
        {
            {
                38000,
                new EmployeeAggregate
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateOnly.MinValue,
                    AnnualGrossSalary = 50000,
                    Id = default
                }
            }
        };

        public static TheoryData<decimal, EmployeeAggregate> CalculateNetSalaryTheoryTwo => new()
        {
            {
                86000,
                new EmployeeAggregate
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateOnly.MinValue,
                    AnnualGrossSalary = 100000,
                    Id = default
                }
            }
        };

        public static TheoryData<decimal, EmployeeAggregate> CalculateNetSalaryTheoryThree => new()
        {
            {
                78800,
                new EmployeeAggregate
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateOnly.MinValue,
                    AnnualGrossSalary = 100000,
                    Id = default,
                    Dependents =
                    [
                        new()
                        {
                            Age = 30,
                            FirstName = "Jane",
                            LastName = "Doe",
                            Relationship = Relationship.Spouse,
                            Id = default,
                            Parent = new EmployeeAggregate
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                DateOfBirth = DateOnly.MinValue,
                                AnnualGrossSalary = 100000,
                                Id = default
                            }
                        }
                    ]
                }
            }
        };

        public static TheoryData<decimal, EmployeeAggregate> CalculateNetSalaryTheoryFour => new()
        {
            {
                76400,
                new EmployeeAggregate
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateOnly.MinValue,
                    AnnualGrossSalary = 100000,
                    Id = default,
                    Dependents =
                    [
                        new()
                        {
                            Age = 51,
                            FirstName = "Jane",
                            LastName = "Doe",
                            Relationship = Relationship.Spouse,
                            Id = default,
                            Parent = new EmployeeAggregate
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                DateOfBirth = DateOnly.MinValue,
                                AnnualGrossSalary = 100000,
                                Id = default
                            }
                        }
                    ]
                }
            }
        };

        [Theory]
        [MemberData(nameof(CalculateNetSalaryTheoryOne))]
        [MemberData(nameof(CalculateNetSalaryTheoryTwo))]
        [MemberData(nameof(CalculateNetSalaryTheoryThree))]
        [MemberData(nameof(CalculateNetSalaryTheoryFour))]
        public void CalculateNetSalaryTest(decimal expected,  EmployeeAggregate entity)
        {
            decimal result = entity.CalculateNetSalary();

            Assert.Equal(expected, result);
        }
    }
}
