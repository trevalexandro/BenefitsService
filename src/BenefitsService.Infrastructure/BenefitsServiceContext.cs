using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Infrastructure
{
    public class BenefitsServiceContext(DbContextOptions<BenefitsServiceContext> options) : DbContext(options)
    {
        public required DbSet<EmployeeAggregate> Employees { get; set; }
        public required DbSet<Dependent> Dependents { get; set; }
        public required DbSet<DependentRelationship> DependentRelationships { get; set; }
    }
}
