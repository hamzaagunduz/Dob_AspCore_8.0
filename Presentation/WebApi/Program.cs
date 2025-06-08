using Application.Interfaces;
using Application.Interfaces.ICourseRepository;
using Application.Interfaces.IExamRepository;
using Application.Interfaces.IFileStorageService;
using Application.Interfaces.IFlashCardRepository;
using Application.Interfaces.IQuestionRepository;
using Application.Interfaces.IShopRepository;
using Application.Interfaces.ITestGroupRepository;


//using Application.Interfaces.IShopRepository;
using Application.Interfaces.ITopicRepository;
using Application.Interfaces.IUserDailyMissionRepository;
using Application.Interfaces.IUserRepository;
using Application.Interfaces.IUserStatisticsRepository;
using Application.Interfaces.IUserTopicPerformanceRepository;
using Application.Services;
using Application.Tools;
using Domain.Entities;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.SemanticKernel;
using OpenAI;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.Repository;
using Persistence.Repositories.Repository.Infrastructure.Persistence.Repositories;
using Persistence.Services;
using System.ClientModel;
using System.Text;
using WebApi.Hubs;
using WebApi.Services;
using static WebApi.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddProblemDetails();

builder.Services.AddSignalR();
builder.Services.AddSingleton<AIService>();


builder.Services
    .AddKernel()
    .AddOpenAIChatCompletion(
        modelId: "google/gemini-flash-1.5-8b",
        openAIClient: new OpenAIClient(
            credential: new ApiKeyCredential("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"),
            options: new OpenAIClientOptions
            {
                Endpoint = new Uri("https://openrouter.ai/api/v1")
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



builder.Services.AddScoped<DobContext>();

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

builder.Services.AddScoped<IUserRepository, UserRepository>();

//can yenileme
builder.Services.AddHostedService<LifeRegenerationService>();
builder.Services.AddHostedService<ResetUserDailyMissionsService>();


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

app.UseHttpsRedirection();

app.UseAuthentication();  // Add this line to ensure authentication middleware is used
app.UseAuthorization();

app.MapHub<AIHub>("ai-hub");

app.MapControllers();



app.MapPost("/chat2", async (AIService aiService, ChatRequestVM request) =>
{
    string response = await aiService.GetMessageAsync(request.Prompt);
    return Results.Ok(new { Message = response });
});

app.Run();
