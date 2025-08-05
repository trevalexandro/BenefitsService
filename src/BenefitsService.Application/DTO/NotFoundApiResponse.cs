using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Application.DTO
{
    public class NotFoundApiResponse<TData> : ApiResponse<TData>
    {
        public override bool Success => false;
        public override string Error => "The requested resource was not found.";
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
