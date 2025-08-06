using System.Net;

namespace BenefitsService.Application.DTO
{
    public class BadRequestApiResponse<TData> : ApiResponse<TData>
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public override bool Success => false;
    }
}
