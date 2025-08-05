using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Application.DTO
{
    public class BadRequestApiResponse<TData> : ApiResponse<TData>
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}
