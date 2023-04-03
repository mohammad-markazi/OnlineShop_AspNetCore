using _0_Framework.Application;
using _0_Framework.Infrastructure.ZarinPal;
using _0_Framework.Infrastructure;
using _01_LampShadeQuery.Contracts;
using _01_LampShadeQuery.Query;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceHost;
using ShopManagement.Configuration;
using System.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.Extensions.Configuration;
using AccountManagement.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration= builder.Configuration;
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
var connectionString = Configuration.GetConnectionString("LampShade");
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);
builder.Services.AddSingleton<IFileUploader, FileUploader>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<ICartCalculatorService, CartCalculatorService>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
//find text persian encoding in meta tag
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
builder.Services.AddScoped<IDataInitializer, DataInitializer>();
builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    options.Filters.Add<SecurityPageFilter>();
}).AddRazorPagesOptions(option =>
{
    option.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
});

// services.Configure<CookiePolicyOptions>(options =>
// {
//     options.CheckConsentNeeded = context => true;
//     options.MinimumSameSitePolicy = SameSiteMode.Lax;
// });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea", optionBuilder =>
    {
        optionBuilder.RequireAuthenticatedUser();
        optionBuilder.RequireClaim("Permissions");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();
app.IntializeDatabase<AccountContext>();

app.MapRazorPages();
app.MapAreaControllerRoute("default", "Administration", "{controller=Home}/{action=Index}/{id?}");
app.Run();