using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity
{
    public static class ServiceCollectionExtensions
    {
        //public static IServiceCollection AddIdentityServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        //{
        //    serviceCollection.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        //    serviceCollection.AddDbContext<RaceDayIdentityDbContext>(dbContextOptionsBuilder =>
        //        dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("RaceDayIdentityConnectionString"),
        //            sqlServerDbContextOptionsBuilder => sqlServerDbContextOptionsBuilder.MigrationsAssembly(typeof(RaceDayIdentityDbContext).Assembly.FullName)));

        //    serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
        //        .AddEntityFrameworkStores<RaceDayIdentityDbContext>().AddDefaultTokenProviders();

        //    serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();

        //    serviceCollection.AddAuthentication(authenticationOptions =>
        //    {
        //        authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(jwtBearerOptions =>
        //    {
        //        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ClockSkew = TimeSpan.Zero,
        //            ValidIssuer = configuration["JwtSettings:Issuer"],
        //            ValidAudience = configuration["JwtSettings:Audience"],
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
        //        };
        //    });

        //    return serviceCollection;
        //}
    }
}
