using Azure.Storage.Blobs;
using E_commerceOnlineStore.Azure;
using E_commerceOnlineStore.Data;
using E_commerceOnlineStore.Models;
using E_commerceOnlineStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
