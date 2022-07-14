using System;
using Microsoft.AspNetCore.Builder;

namespace Shop.ShopAPI.AuthenticationService.Middlewares
{
    public static class AccessTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenManagerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccessTokenManagerMiddleware>();
        }
    }
}