using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Adom.Framework.AspNetCore.Sample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            app.UseAuthorization();
            app.MapControllers();
            app.UseExceptionToHttpResult();
            app.Run();
        }
    }
}