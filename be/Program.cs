using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
using be.Repositories.ModRepository;
using be.Models;
using be.Services.ModService;
using be.Services.PostService;
using be.Services.PostcommentService;
using be.Services.SubjectService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<DbZotsystemContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbZotSystem")));
builder.Services.AddCors();

var services = builder.Services;
services.AddHttpContextAccessor();

services.AddScoped<IModRepository, ModRepository>();
services.AddScoped<IPostService, PostService>();
services.AddScoped<IPostcommentService, PostcommentService>();
services.AddScoped<ISubjectService, SubjectService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
