using Application.Interfaces;
using Application.Services;
using MediatR;
using Persistence.Context;
using Persistence.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
// Add services to the container.
builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<DobContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
