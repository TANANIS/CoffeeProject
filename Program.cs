using Coffee.Models;
using Microsoft.EntityFrameworkCore;
using Coffee.Controllers;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductServiceEn>();
builder.Services.AddScoped<ProductServiceJp>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//記得加-----------------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//記得加-----------------



builder.Services.AddDbContext<ProjectContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connstring")));
builder.Services.AddScoped<DBCNcart>();



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


//記得加-----------------
app.UseSession();
//記得加-----------------



//New
app.MapControllerRoute(
    name: "Query",
    pattern: "List/Query/{column}/{category?}",
    defaults: new { controller = "List", action = "Query" });


app.MapControllerRoute(
    name: "GetModal",
    pattern: "List/ShowProductModal",
    defaults: new { controller = "List", action = "ShowProductModal" });

app.MapControllerRoute(
    name: "ProductJp",
    pattern: "List/AllJp/{column?}/{category?}",
    defaults: new { controller = "List", action = "AllJp" });

app.MapControllerRoute(
    name: "ProductEn",
    pattern: "List/AllEn/{column?}/{category?}",
    defaults: new { controller = "List", action = "AllEn" });

app.MapControllerRoute(
    name: "Product",
    pattern: "List/{column?}/{category?}",
    defaults: new { controller = "List", action = "All" });


//
app.MapControllerRoute(
	name: "Detail",
	 pattern: "Detail/Detail/{id}",
	 defaults: new { controller = "Detail", action = "Detail" });

app.MapControllerRoute(
	name: "DetailEn",
	 pattern: "DetailEn/DetailEn/{id}",
	 defaults: new { controller = "DetailEn", action = "DetailEn" });

app.MapControllerRoute(
	name: "DetailJp",
	 pattern: "DetailJp/DetailJp/{id}",
	 defaults: new { controller = "DetailJp", action = "DetailJp" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
