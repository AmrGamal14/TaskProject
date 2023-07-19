using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using TaskProject.Core;
using TaskProject.Core.MiddleWare;
using TaskProject.Data.Entities.Identity;
using TaskProject.Infrastructure;
using TaskProject.Infrastructure.Abstracts;
using TaskProject.Infrastructure.Data;
using TaskProject.Infrastructure.Repositories;
using TaskProject.Infrastructure.Seeder;
using TaskProject.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Connection To SQL Server
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
});


#region Dependency injections
builder.Services.AddinfrastructureDependencies()
                .AddServiceDependencies()
                .AddCoreDependencies()
                .AddServiceRegisteration(builder.Configuration);
#endregion
#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt => { opt.ResourcesPath=""; });
builder.Services.Configure<RequestLocalizationOptions>(Options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG")
    };
    Options.DefaultRequestCulture=new RequestCulture("en-US");
    Options.SupportedCultures=supportedCultures;
    Options.SupportedUICultures=supportedCultures;

});
#endregion

var app = builder.Build();
using (var scope=app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager);

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "default",
      //defaults: new { controller = "Auth", action = "Login" },
      pattern: "{controller=Authentication}/{action=Index}/{id?}");

    //endpoints.MapSwagger();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
