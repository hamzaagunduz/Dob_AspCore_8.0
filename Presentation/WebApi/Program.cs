using Application.Interfaces;
using Application.Interfaces.IExamRepository;
using Application.Interfaces.ITopicRepository;
using Application.Services;
using Application.Tools;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.Repository;
using Persistence.Repositories.Repository.Infrastructure.Persistence.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddProblemDetails();


//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT token in the format 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
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
            new List<string>()
        }
    });
});



builder.Services.AddScoped<DobContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IExamRepository), typeof(ExamRepository));
builder.Services.AddScoped(typeof(ITopicRepository), typeof(TopicRepository));


builder.Services.AddApplicationService(builder.Configuration);


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:5173", "https://localhost:5173", "https://localhost:7172").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));


builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    //options.User.RequireUniqueEmail = false;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
})
.AddEntityFrameworkStores<DobContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.UseSecurityTokenValidators = true;
    options.IncludeErrorDetails = true;
    options.Events = new JwtBearerEvents
    {



        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Hata: {context.Exception.Message}");
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = JwtTokenDefaults.ValidIssuer,
        ValidAudience = JwtTokenDefaults.ValidAudience,
        ValidateLifetime = true, // Bu, token'ýn geçerlilik süresini doðrular
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ClockSkew = TimeSpan.Zero,
    };
});






builder.Services.AddHttpContextAccessor();










var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{


    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

//Default Þema yazdýmra
//app.Use(async (context, next) =>
//{
//    var defaultScheme = context.RequestServices
//        .GetRequiredService<IOptions<AuthenticationOptions>>()
//        .Value.DefaultAuthenticateScheme;

//    Console.WriteLine($"Default Authenticate Scheme: {defaultScheme}");

//    await next();
//});


app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();  // Add this line to ensure authentication middleware is used
app.UseAuthorization();

app.MapControllers();

app.Run();
