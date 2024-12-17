using Application.Interfaces;
using Application.Services;
using Application.Tools;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using Persistence.Repositories;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = JwtTokenDefaults.ValidIssuer,
        ValidAudience = JwtTokenDefaults.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();



builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<DobContext>();

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
})
.AddEntityFrameworkStores<DobContext>()
.AddDefaultTokenProviders();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthentication();  // Add this line to ensure authentication middleware is used
app.UseAuthorization();

app.MapControllers();

app.Run();
