    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Api;
    using Api.Identity;
    using Api.Interfaces;
    using Api.Models;
    using Api.Models.DTOs;
    using Api.Repository;
    using Api.Services;
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json.Converters;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp",
            policy =>
            {
                policy.WithOrigins("http://localhost:5174")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
    });
    // Add services to the container.
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
    builder.Services.AddScoped<IGameRepository, GameRepository>();
    builder.Services.AddScoped<IDetailsGameRepository, DetailsGameRepository>();

    builder.Services.AddControllers();


    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),new MySqlServerVersion(new Version(8,0,32))));

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(IdentityData.AdminPolicyName, p =>
            p.RequireClaim(IdentityData.RoleUserClaimName, "admin"));
        options.AddPolicy(IdentityData.DeveloperPolicyName, p =>
            p.RequireClaim(IdentityData.RoleUserClaimName, "developer"));
    });
    
    builder.Services.AddAuthentication();
    builder.Services.AddScoped<JwtService>();

    var app = builder.Build();

    app.UseCors("AllowReactApp");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();