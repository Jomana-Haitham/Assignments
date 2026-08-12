//using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Mapping;
using Task.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<Task.Services.TaskService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new OpenApiInfo
//     {
//        Title = "Task API",
//         Version = "v1",
//         Description = "Task Management API"
//     });

//     var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

//     options.IncludeXmlComments(xmlPath);
// });
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

