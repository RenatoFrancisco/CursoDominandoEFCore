using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Multitenant.Extensions;
using Multitenant.Provider;

namespace Multitenant.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var tenant = httpContext.RequestServices.GetRequiredService<TenantData>();
            tenant.TenantId = httpContext.GetTenantId();

            await _next(httpContext);
        }
    }
}