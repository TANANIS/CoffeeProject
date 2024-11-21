using Microsoft.EntityFrameworkCore;
using WebApplication1_1105_TSET_member5.Models;

var builder = WebApplication.CreateBuilder(args);

//記得加-----------------
builder.Services.AddDbContext<ProjectContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("PROJECTConnstring")));
//記得加-----------------

// Add services to the container.
builder.Services.AddControllersWithViews();

//記得加-----------------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // 設置 Session 過期時間
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//記得加-----------------

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
