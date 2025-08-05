using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Application.DTO
{
    public class ApiResponse<TData>
    {
        public virtual TData? Data { get; set; }
        public virtual int? TotalCount { get; set; }
        public virtual bool Success { get; set; } = true;
        public virtual string? Error { get; set; }
        public virtual HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
