namespace App.Web.Infrastructure.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class MyCustomMiddlewares
    {
        public static IApplicationBuilder UseCheckShopkeeperPasswordMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseCheckShopkeeperPasswordMiddleware>();
        }
    }
}