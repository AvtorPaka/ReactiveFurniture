using Management.Service.Api.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Management.Service.Api.FiltersAttributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class ErrorResponseTypeAttribute : ProducesResponseTypeAttribute
{
    public ErrorResponseTypeAttribute(int statusCode) : base(typeof(ErrorResponse), statusCode)
    {
    }
}