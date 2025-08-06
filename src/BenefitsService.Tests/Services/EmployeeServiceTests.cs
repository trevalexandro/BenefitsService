using BenefitsService.Application.DTO;
using BenefitsService.Application.Services;
using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Enums;
using BenefitsService.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Tests.Services
{
    public class EmployeeServiceTests
    {
        public static TheoryData<NotFoundApiResponse<Employee>, EmployeeAggregate?> NotFoundTheory => new()
        {
            {
                new NotFoundApiResponse<Employee>(),
                null
            }
        };

        public static TheoryData<ApiResponse<Employee>, EmployeeAggregate?> EmployeeAggregateTheory => new()
        {
            {
                new ApiResponse<Employee>(),
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

        [Theory]
        [MemberData(nameof(NotFoundTheory))]
        [MemberData(nameof(EmployeeAggregateTheory))]
        public async Task GetEmployeeByIdAsyncTest(ApiResponse<Employee> expected, EmployeeAggregate? entity)
        {
            var employeeAggregateRepositoryMock = new Mock<IEmployeeAggregateRepository>();
            employeeAggregateRepositoryMock.Setup(employeeAggregateRepository => 
                employeeAggregateRepository.GetEmployeeWithDependentsAsync(It.IsAny<Guid>())).ReturnsAsync(entity);
            var employeeService = new EmployeeService(employeeAggregateRepositoryMock.Object);

            var response = await employeeService.GetEmployeeByIdAsync(default);

            Assert.Equal(expected.Success, response.Success);
            Assert.Equal(expected.Error, response.Error);
            Assert.Equal(expected.StatusCode, response.StatusCode);
            if (response.Success)
            {
                Assert.NotNull(response.Data);
            }
            employeeAggregateRepositoryMock.Verify(employeeAggregateRepository => 
                employeeAggregateRepository.GetEmployeeWithDependentsAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(null, null)]
        public async Task GetEmployeesAsyncTest(int? pageSize, int? offset)
        {
            var entities = new List<EmployeeAggregate>
            {
                new() {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateOnly.MinValue,
                    AnnualGrossSalary = 100000,
                    Id = default
                }
            };
            var employeeAggregateRepositoryMock = new Mock<IEmployeeAggregateRepository>();
            employeeAggregateRepositoryMock.Setup(employeeAggregateRepository => 
                employeeAggregateRepository.CountAsync<EmployeeAggregate>()).ReturnsAsync(1);
            employeeAggregateRepositoryMock.Setup(employeeAggregateRepository => employeeAggregateRepository
                .GetAllAsync<EmployeeAggregate>(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(entities);
            var employeeService = new EmployeeService(employeeAggregateRepositoryMock.Object);

            var response = await employeeService.GetEmployeesAsync(pageSize, offset);

            Assert.NotNull(response.Data);
            Assert.True(response.Success);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Single(response.Data);
            Assert.Null(response.Error);
            Assert.Equal(1, response.TotalCount);
            employeeAggregateRepositoryMock.Verify(employeeAggregateRepository => 
                employeeAggregateRepository.CountAsync<EmployeeAggregate>(), Times.Once);
            employeeAggregateRepositoryMock.Verify(employeeAggregateRepository => employeeAggregateRepository
                .GetAllAsync<EmployeeAggregate>(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(NotFoundTheory))]
        [MemberData(nameof(EmployeeAggregateTheory))]
        public async Task UpdateEmployeeAsyncTest(ApiResponse<Employee> expected, EmployeeAggregate? entity)
        {
            var employeeAggregateRepositoryMock = new Mock<IEmployeeAggregateRepository>();
            employeeAggregateRepositoryMock.Setup(employeeAggregateRepository => 
                employeeAggregateRepository.GetEmployeeWithDependentsAsync(It.IsAny<Guid>())).ReturnsAsync(entity);
            employeeAggregateRepositoryMock.Setup(employeeAggregateRepository => employeeAggregateRepository
                .SaveChangesAsync(It.IsAny<EmployeeAggregate>()))
                .ReturnsAsync(Guid.NewGuid());
            var employeeService = new EmployeeService(employeeAggregateRepositoryMock.Object);

            var response = await employeeService.UpdateEmployeeAsync(default, new EmployeeUpdate());

            Assert.Equal(expected.Success, response.Success);
            Assert.Equal(expected.Error, response.Error);
            Assert.Equal(expected.StatusCode, response.StatusCode);
            if (response.Success)
            {
                Assert.NotNull(response.Data);
                employeeAggregateRepositoryMock.Verify(employeeAggregateRepository => 
                    employeeAggregateRepository.SaveChangesAsync(It.IsAny<EmployeeAggregate>()), Times.Once);
            }
            employeeAggregateRepositoryMock.Verify(employeeAggregateRepository => 
                employeeAggregateRepository.GetEmployeeWithDependentsAsync(It.IsAny<Guid>()), Times.Once);
        }

        public static TheoryData<NotFoundApiResponse<Employee>, NewDependent, EmployeeAggregate?> 
            AddDependentAsyncTheoryOne => new()
        {
            {
                new NotFoundApiResponse<Employee>(),
                new NewDependent
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Age = 30,
                    Relationship = string.Empty
                },
                null
            }
        };

        public static TheoryData<BadRequestApiResponse<Employee>, NewDependent, EmployeeAggregate?> 
            AddDependentAsyncTheoryTwo => new()
        {
            {
                new BadRequestApiResponse<Employee>(),
                new NewDependent
                {
                    FirstName = "Marcia",
                    LastName = "Doe",
                    Age = 25,
                    Relationship = string.Empty
                },
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

        public static TheoryData<BadRequestApiResponse<Employee>, NewDependent, EmployeeAggregate?> 
            AddDependentAsyncTheoryThree => new()
        {
            {
                new BadRequestApiResponse<Employee>(),
                new NewDependent
                {
                    FirstName = "Marcia",
                    LastName = "Doe",
                    Age = 25,
                    Relationship = "Spouse"
                },
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

        public static TheoryData<ApiResponse<Employee>, NewDependent, EmployeeAggregate?> 
            AddDependentAsyncTheoryFour => new()
        {
            {
                new ApiResponse<Employee>
                {
                    StatusCode = HttpStatusCode.Created
                },
                new NewDependent
                {
                    FirstName = "Marcia",
                    LastName = "Doe",
                    Age = 11,
                    Relationship = "Child"
                },
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

        [Theory]
        [MemberData(nameof(AddDependentAsyncTheoryOne))]
        [MemberData(nameof(AddDependentAsyncTheoryTwo))]
        [MemberData(nameof(AddDependentAsyncTheoryThree))]
        [MemberData(nameof(AddDependentAsyncTheoryFour))]
        public async Task AddDependentAsyncTest(ApiResponse<Employee> expected, NewDependent newDependent, 
            EmployeeAggregate? entity)
        {
            var employeeAggregateRepositoryMock = new Mock<IEmployeeAggregateRepository>();
            employeeAggregateRepositoryMock.Setup(employeeAggregateRepository => 
                employeeAggregateRepository.GetEmployeeWithDependentsAsync(It.IsAny<Guid>())).ReturnsAsync(entity);
            employeeAggregateRepositoryMock.Setup(employeeAggregateRepository => employeeAggregateRepository
                .SaveChangesAsync(It.IsAny<EmployeeAggregate>()))
                .ReturnsAsync(Guid.NewGuid());
            var employeeService = new EmployeeService(employeeAggregateRepositoryMock.Object);

            var response = await employeeService.AddDependentAsync(default, newDependent);

            Assert.Equal(expected.Success, response.Success);
            Assert.Equal(expected.StatusCode, response.StatusCode);
            Assert.Null(response.TotalCount);
            employeeAggregateRepositoryMock.Verify(employeeAggregateRepository => 
                employeeAggregateRepository.GetEmployeeWithDependentsAsync(It.IsAny<Guid>()), Times.Once);
            if (response.Success)
            {
                Assert.NotNull(response.Data);
                employeeAggregateRepositoryMock.Verify(employeeAggregateRepository => 
                    employeeAggregateRepository.SaveChangesAsync(It.IsAny<EmployeeAggregate>()), Times.Once);
            }
            else
            {
                Assert.NotEqual(string.Empty, response.Error);
            }

        }
    }
}
