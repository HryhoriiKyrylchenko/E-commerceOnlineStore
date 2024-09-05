using Azure.Storage.Blobs;
using E_commerceOnlineStore.Azure;
using E_commerceOnlineStore.Data;
using E_commerceOnlineStore.Models.DataModels.Account;
using E_commerceOnlineStore.Services.Business;
using E_commerceOnlineStore.Services.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton((serviceProvider) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    string connectionString = configuration.GetConnectionString("AzureBlobStorage") ?? "";

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("The Azure Blob Storage connection string is not configured.");
    }

    return new BlobServiceClient(connectionString);
});

// Ensure that configuration values are not null
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Key not found in configuration.");
var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("JWT Issuer not found in configuration.");
var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("JWT Audience not found in configuration.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Log detailed information about the challenge
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
            logger.LogWarning("JWT Challenge: {Message}", "Invalid token");
            context.HandleResponse();
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            // Log detailed information about the failure
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
            logger.LogError(context.Exception, "JWT Authentication failed: {Message}", context.Exception.Message);
            context.NoResult();
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();

var smtpSection = builder.Configuration.GetSection("Smtp");

var smtpHost = smtpSection["Host"] ?? throw new InvalidOperationException("SMTP host is not configured.");
var smtpPortStr = smtpSection["Port"] ?? throw new InvalidOperationException("SMTP port is not configured.");
var smtpUser = smtpSection["User"] ?? throw new InvalidOperationException("SMTP user is not configured.");
var smtpPass = smtpSection["Pass"] ?? throw new InvalidOperationException("SMTP password is not configured.");

if (!int.TryParse(smtpPortStr, out var smtpPort))
{
    throw new InvalidOperationException("SMTP port is not a valid integer.");
}

builder.Services.AddSingleton<IEmailService>(new EmailService(smtpHost, smtpPort, smtpUser, smtpPass));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the token in the format: Bearer {your token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    var logger = app.Logger;
    logger.LogInformation("Handling request: {RequestPath}", context.Request.Path);
    await next.Invoke();
    logger.LogInformation("Finished handling request.");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
