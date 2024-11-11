var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 根路由重定向到 /language/coffeeMap
app.MapGet("/", (HttpContext httpContext) =>
{
    httpContext.Response.Redirect("/language/coffeeMap");
});

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

// 設定 Controller 路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Map}/{action=CoffeeMap}/{id?}");

app.Run();
