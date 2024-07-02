using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mapper;
using industry4_Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyKoel_Domain.Interfaces;
using MyKoel_Domain.Repositories;
using MyKoel_Domain.Services;


namespace MyKoel_Domain.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService,TokenService>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<ICurrentUserService,CurrentUserService>();
            services.AddScoped<IDateTime,DateTimeService>();
            services.AddScoped<IMenuHierarchyRepository,MenuHierarchyRepository>();
            
            return services;
        }
    }
}