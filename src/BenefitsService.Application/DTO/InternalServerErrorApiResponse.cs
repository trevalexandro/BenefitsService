using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Application.DTO
{
    public class InternalServerErrorApiResponse<TData> : ApiResponse<TData>
    {
        public override string? Error => "Something went wrong when trying to get the requested resource";
        public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
        public override bool Success => false;
    }
}
