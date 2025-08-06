﻿using System.Net;
using System.Text.Json.Serialization;

namespace BenefitsService.Application.DTO
{
    public class ApiResponse<TData>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual TData? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual int? TotalCount { get; set; }
        public virtual bool Success { get; set; } = true;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual string? Error { get; set; }
        public virtual HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
