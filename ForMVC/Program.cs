using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskProject.Infrastructure.Data;
using TaskProject.Infrastructure;
using TaskProject.Service;
using TaskProject.Core;
using Microsoft.AspNetCore.Identity;
using TaskProject.Data.Entities.Identity;
using TaskProject.Infrastructure.Seeder;
using Microsoft.Extensions.Options;
using ForMVC.LocalStorageService;
using TaskProject.Core.MiddleWare;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;
using ForMVC.Action_Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
});

builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthenticationFilter>();
builder.Services.AddSession();

#region Dependency injections
builder.Services.AddinfrastructureDependencies()
                .AddServiceDependencies()
                .AddCoreDependencies()
                .AddServiceRegisteration(builder.Configuration)
                .AddHttpContextAccessor();
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
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager);

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Index}/{id?}");

app.Run();
