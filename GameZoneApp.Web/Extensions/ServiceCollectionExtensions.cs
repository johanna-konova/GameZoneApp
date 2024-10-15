using GameZoneApp.Core.Contracts;
using GameZoneApp.Core.Services;
using GameZoneApp.Infrastructure.Common;
using GameZoneApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Models.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGenreService, GenreService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<GameZoneDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository, Repository>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount =
                        configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                    options.Password.RequireDigit =
                        configuration.GetValue<bool>("Identity:Password:RequireDigit");
                    options.Password.RequireLowercase =
                        configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                    options.Password.RequireNonAlphanumeric =
                        configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                    options.Password.RequireUppercase =
                        configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<GameZoneDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
            });

            return services;
        }
    }
}
