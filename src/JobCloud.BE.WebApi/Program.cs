using JobCloud.BE.Application.IoC;
using JobCloud.BE.ReadModel.Application.IoC;
using JobCloued.BE.Migrations.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddReadModelServices();
builder.Services.AddMigrationsServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.RegisterOffersReadModelsEndpoints();

app.UseMigrations();

app.Run();