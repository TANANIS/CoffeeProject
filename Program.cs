using Microsoft.EntityFrameworkCore;
using WebApplication1_1105_TSET_member5.Models;

var builder = WebApplication.CreateBuilder(args);

//°O±o¥[-----------------
builder.Services.AddDbContext<ProjectContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("PROJECTConnstring")));
//°O±o¥[-----------------

// Add services to the container.
builder.Services.AddControllersWithViews();

//°O±o¥[-----------------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//°O±o¥[-----------------

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

//°O±o¥[-----------------
app.UseSession();
//°O±o¥[-----------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
