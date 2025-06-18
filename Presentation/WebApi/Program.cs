using Application.Interfaces;
using Application.Interfaces.IAppUserRepository;
using Application.Interfaces.ICourseRepository;
using Application.Interfaces.ICurrentUserContext;
using Application.Interfaces.IExamRepository;
using Application.Interfaces.IFileStorageService;
using Application.Interfaces.IFlashCardRepository;
using Application.Interfaces.IPaymentService;
using Application.Interfaces.IQuestionRepository;
using Application.Interfaces.IShopRepository;
using Application.Interfaces.ISystemMetricsService.cs;
using Application.Interfaces.ITestGroupRepository;


//using Application.Interfaces.IShopRepository;
using Application.Interfaces.ITopicRepository;
using Application.Interfaces.IUserDailyMissionRepository;
using Application.Interfaces.IUserLoginHistoryRepository;
using Application.Interfaces.IUserRepository;
using Application.Interfaces.IUserStatisticsRepository;
using Application.Interfaces.IUserTopicPerformanceRepository;
using Application.Services;
using Application.Tools;
using Application.Validators;
using Domain.Entities;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.SemanticKernel;
using OpenAI;
using Persistence.Context;
using Persistence.Hubs;
using Persistence.Identity;
using Persistence.Providers;
using Persistence.Repositories;
using Persistence.Repositories.Repository;
using Persistence.Repositories.Repository.Infrastructure.Persistence.Repositories;
using Persistence.Services;
using System.ClientModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddProblemDetails();

builder.Services.AddSignalR();


builder.Services.AddDbContext<DobContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<AIServiceV2>();
builder.Services.AddScoped<IyzipaySettingsProvider>();

builder.Services.AddScoped<AIConfigurationProvider>();

var provider = builder.Services.BuildServiceProvider();
var aiConfigProvider = provider.GetRequiredService<AIConfigurationProvider>();
var aiConfig = await aiConfigProvider.GetConfigAsync(); 

builder.Services
    .AddKernel()
    .AddOpenAIChatCompletion(
        modelId: aiConfig.ModelId,
        openAIClient: new OpenAIClient(
            credential: new ApiKeyCredential(aiConfig.ApiKey),
            options: new OpenAIClientOptions
            {
                Endpoint = new Uri(aiConfig.Endpoint)
            })
    );


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





builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IExamRepository), typeof(ExamRepository));
builder.Services.AddScoped(typeof(ITopicRepository), typeof(TopicRepository));
builder.Services.AddScoped(typeof(ICourseRepository), typeof(CourseRepository));
builder.Services.AddScoped(typeof(IQuestionRepository), typeof(QuestionRepository));
builder.Services.AddScoped(typeof(IFlashCardRepository), typeof(FlashCardRepository));
builder.Services.AddScoped(typeof(IUserStatisticsRepository), typeof(UserStatisticsRepository));
builder.Services.AddScoped(typeof(IUserDailyMissionRepository), typeof(UserDailyMissionRepository));
builder.Services.AddScoped(typeof(IShopRepository), typeof(ShopRepository));
builder.Services.AddScoped(typeof(IUserTopicPerformanceRepository), typeof(UserTopicPerformanceRepository));
builder.Services.AddScoped(typeof(IFileStorageService), typeof(FileStorageService));
builder.Services.AddScoped(typeof(ITestGroupRepository), typeof(TestGroupRepository));
builder.Services.AddScoped(typeof(ICurrentUserContext), typeof(CurrentUserContext));
builder.Services.AddScoped(typeof(IAppUserRepository), typeof(AppUserRepository));
builder.Services.AddScoped(typeof(IUserLoginHistoryRepository), typeof(UserLoginHistoryRepository));
builder.Services.AddScoped<ISystemMetricsService, SystemMetricsService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordValidator>();
builder.Services.AddTransient<IUserValidator<AppUser>, CustomUserValidator>();


builder.Services.AddScoped<IUserRepository, UserRepository>();

//can yenileme
builder.Services.AddHostedService<LifeRegenerationService>();
builder.Services.AddHostedService<ResetUserDailyMissionsService>();


builder.Services.AddApplicationService(builder.Configuration);


var corsOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(corsOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();

        // Sadece Development ortamýnda AllowCredentials eklenebilir
        if (builder.Environment.IsDevelopment())
        {
            policy.AllowCredentials();
        }
    });
});


builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    // Validasyon ayarlarý
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;

    options.User.RequireUniqueEmail = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
})
.AddEntityFrameworkStores<DobContext>()
.AddErrorDescriber<CustomIdentityErrorDescriber>();


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
if (builder.Environment.IsProduction())
{
    builder.WebHost.UseUrls("http://0.0.0.0:80"); // Sadece Docker'da çalýþýr
}










var app = builder.Build();

// Roll seed etmek için
//async Task SeedRolesAsync(IServiceProvider serviceProvider)
//{
//    using var scope = serviceProvider.CreateScope();
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

//    string[] roles = new[] { "Admin", "Teacher", "Student" };

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new AppRole { Name = role });
//        }
//    }
//}

//await SeedRolesAsync(app.Services);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{


    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}



app.UseCors();
app.UseStaticFiles();  // wwwroot altýndaki dosyalarý açar

//if (!app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//}


app.UseAuthentication();  // Add this line to ensure authentication middleware is used
app.UseAuthorization();

app.MapHub<PayHub>("/payHub");
app.MapHub<AIHubV2>("aihubv2");

app.MapControllers();




app.MapGet("/", () => "API is running");

app.Run();
