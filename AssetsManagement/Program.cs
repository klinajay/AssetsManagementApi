using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;

using Microsoft.AspNetCore.Builder;
using AssetsManagement.DB;
using AssetsManagement.Services;
using AssetsManagement.Contracts;
using AssetsManagement.Respositories;
using AssetsManagement.Data; // Add this using directive

//using AssetsManagementBlazor.Components.Pages;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddRazorComponents();
builder.Services.AddSingleton<DbContext>();
builder.Services.AddSingleton<MachinesService>();
builder.Services.AddSingleton<IMachinesRepository, MachinesRepository>();
builder.Services.AddSingleton<AssetsService>();
builder.Services.AddSingleton<IAssetsRepository, AssetsRepository>();
//builder.Services.AddSingleton<IInputData, InputDataFromText>();
builder.Services.AddKeyedSingleton<IInputData, InputDataFromText>("text");
builder.Services.AddKeyedSingleton<IInputData, InputDataFromJson>("json");
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(); 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AssetsManagement API",
        Version = "v1",
        Description = "Latest version."
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssetsManagement API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Static files (if needed for Blazor)



app.UseCors("AllowAll");  // Enable CORS middleware

app.MapControllers(); // API should be routed to /api/{controller}


app.Run();
