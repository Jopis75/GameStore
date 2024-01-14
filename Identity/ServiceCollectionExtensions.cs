using Application.Dtos.Identity;
using Application.Interfaces.Identity;
using Application.Models.Identity;
using Identity.DbContexts;
using Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            serviceCollection.AddDbContext(configuration);
            serviceCollection.AddIdentity();
            serviceCollection.AddServices();
            serviceCollection.AddAuthentication(configuration);

            return serviceCollection;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddAuthentication(authenticationOptions =>
                {
                    authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                });

            return serviceCollection;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<GameStoreIdentityDbContext>(dbContextOptionsBuilder =>
                dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("GameStoreIdentityConnectionString"))); //, 
                                                                                                                               //sqlServerDbContextOptionsBuilder => sqlServerDbContextOptionsBuilder.MigrationsAssembly(typeof(GameStoreIdentityDbContext).Assembly.FullName)));

            return serviceCollection;
        }

        private static IServiceCollection AddIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<GameStoreIdentityDbContext>()
                .AddDefaultTokenProviders();

            return serviceCollection;
        }

        private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<IUserService, UserService>();

            return serviceCollection;
        }
    }
}
