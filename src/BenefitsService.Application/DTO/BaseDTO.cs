using BenefitsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Application.DTO
{
    public abstract class BaseDTO
    {
        public Guid? Id { get; set; }
    }
}
