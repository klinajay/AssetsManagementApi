using AssetsManagementBlazor.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services for controllers (API)
builder.Services.AddControllers();

// Add Razor Pages and Blazor services for Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents(); // For Blazor Server components

// Add CORS if your Web API and Blazor UI are on different ports
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Enable CORS middleware
app.UseCors("AllowAll");

// Serve static files for Blazor UI (if needed for Blazor WASM) - **Not necessary for Blazor Server**
app.UseStaticFiles(); // Ensure static files are served for any resources

// Configure routing
app.UseRouting();

// Map Blazor components and specify the root component (App)
app.MapRazorComponents<App>(); // Ensure the root Blazor component is correctly mapped

// Map API controllers
app.MapControllers();

// Blazor Server doesn't require "index.html" to function, so no fallback needed here
// Commenting out MapFallbackToFile for Blazor Server usage
// app.MapFallbackToFile("index.html"); // This is for WASM, not needed for Blazor Server

app.Run();
