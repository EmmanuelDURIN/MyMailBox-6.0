using Microsoft.EntityFrameworkCore;
using MyMailBox.Controllers;
using MyMailBox.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
string connectionString = configuration["MailBoxConnectionString"];
MailBoxContextFactory.ConnectionString = connectionString;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MailBoxContext>(
        options => options.UseSqlServer(MailBoxContextFactory.ConnectionString));
//builder.Services.FillDb();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
//app.UseStatusCodePages(async context =>
//{
//  context.HttpContext.Response.ContentType = "text/plain";
//  await context.HttpContext.Response.WriteAsync(
//      "Sorry, status code page, status code: " +
//      context.HttpContext.Response.StatusCode);
//});
app.UseStatusCodePagesWithRedirects("~/errors/error-{0}");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "discount",
    pattern: "boites-aux-lettres/{reference}",
    defaults: new { controller = "MailBox", action = nameof(MailBoxesController.DetailsByReference) }
    );
app.MapControllerRoute(
                    name: "ErrorRoute",
                    pattern: "errors/error-{status}",
                    defaults: new { controller = "Home", action = nameof(HomeController.CustomError) },
                    constraints: new { status = "\\d+" }
                    );

app.Run();

//void ConfigureServices(IServiceCollection services, IConfiguration configuration)
//{
//  string connectionString = configuration["MailBoxConnectionString"];
//  MailBoxContextFactory.ConnectionString = connectionString;
//}

