using LibraryManagement.Api.ExceptionHandler;
using LibraryManagement.Application.Configuration;
using LibraryManagement.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appConfig = builder.Configuration.LoadConfigurations();
builder.Services.AddSingleton(appConfig);



builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();


builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddControllers();

//builder.Services.AddHostedService<TimedHostedService>();
builder.Services.AddHostedService<TimedBackgroundService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V1",
        Title = "Library",
        Description = "Management books"
    });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();



app.MapControllers();

app.Run();
