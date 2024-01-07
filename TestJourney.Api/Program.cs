using Serilog;
using TestJourney.Business;
using TestJourney.Business.Class;
using TestJourney.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessService();
builder.Services.AddDataAccessServices();

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
                            builder => builder.AllowAnyOrigin()
                                              .AllowAnyHeader()
                                              .AllowAnyMethod()));

var localConfigurations = builder.Configuration.Get<ContextConfiguration>();
builder.Services.AddSingleton(localConfigurations.ApplicationContext);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
