using Microsoft.EntityFrameworkCore;
using PROJECT.Controllers;
using PROJECT.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// -----------------------------------------------------------------------------
// 取得json連線
builder.Services.AddDbContext<ProjectContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectConnection")));
// -----------------------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
