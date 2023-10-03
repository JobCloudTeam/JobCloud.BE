using JobCloud.BE.Application.IoC;
using JobCloud.BE.DB.IoC;
using JobCloud.BE.ReadModel.Offers;
using JobCloued.BE.DB.Migrations.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.RegisterOffersReadModelServices();
builder.Services.AddDbServices(builder.Configuration);
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