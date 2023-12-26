using Application;
using Identity;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Persistance;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();
//builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    //swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Name = "Authorization",
    //    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
    //        Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n
    //        Example: 'Bearer abcdef123456'",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer"
    //});

    //swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Name = "Bearer",
    //            Scheme = "oauth2",
    //            Reference = new OpenApiReference
    //            {
    //                Id = "Bearer",
    //                Type = ReferenceType.SecurityScheme
    //            },
    //            In = ParameterLocation.Header
    //        },
    //        new List<string>()
    //    }
    //});

    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Game Store API"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policyBuilder => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
