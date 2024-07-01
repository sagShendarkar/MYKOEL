using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace MyKoel_Domain.Configuration.CorsPolicyConfig
{
   
    public static class Startup
    {
        private const string CorsPolicy = nameof(CorsPolicy);

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IWebHostEnvironment env)
        {
            // return services.AddCors(opt =>
            //     opt.AddPolicy(CorsPolicy, policy => {
            //         if (env.IsDevelopment())
            //         {
            //             policy.AllowAnyHeader()
            //                 .WithMethods("GET", "POST","PUT")
            //                 .AllowAnyOrigin();
            //         }
            //         else
            //         {
            //             policy.AllowAnyHeader()
            //             .WithMethods("GET", "POST","PUT")
            //             .AllowCredentials()
            //             .WithOrigins("http://localhost:4200", "http://localhost:4200", "http://localhost:5038");
            //         }
            //     }));
           return       services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }

        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app) =>
            app.UseCors(CorsPolicy);
    }

}