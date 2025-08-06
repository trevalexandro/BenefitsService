using System.Net;

namespace BenefitsService.Application.DTO
{
    public class NotFoundApiResponse<TData> : ApiResponse<TData>
    {
        public override bool Success => false;
        public override string Error => "The requested resource was not found.";
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
