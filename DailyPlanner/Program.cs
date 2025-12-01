using DailyPlanner;
using DailyPlanner.Models;
using DailyPlanner.Services;
using DailyPlanner.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure DB connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register EF DbContext factory
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register app services
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<TodoState>();

// Add Razor Components and Interactive Server Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
