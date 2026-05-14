using FrameShare.Domain.Entity;
using FrameShare.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameShare.Infa.Data.Identity;
using FrameShare.Application.Interfaces;
using FrameShare.Infra.Data.Cloudinary;
using FrameShare.Application.Services;

namespace FrameShare.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
            , b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
                    options.AccessDeniedPath = "/Account/Login");
            
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IFotoService, FotoService>();
            services.AddScoped<IMissaoService, MissaoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            return services;
        }
        }
}
