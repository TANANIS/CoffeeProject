using Microsoft.EntityFrameworkCore;
using big.Models;
using big.Controllers;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProductService>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProjectContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("weilunConnstring")));
// Add services to the container.


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
    pattern: "{controller=Product}/{action=Detail}/{id=1}"
    );

app.Run();
