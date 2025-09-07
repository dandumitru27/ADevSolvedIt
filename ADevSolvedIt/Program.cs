using ADevSolvedIt;
using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using ADevSolvedIt.Interfaces;
using ADevSolvedIt.Services;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
services.AddDatabaseDeveloperPageExceptionFilter();

services
    .AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

services
    .AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = configuration["Authentication:Google:ClientId"];
        options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    })
    .AddGitHub(options =>
    {
        options.ClientId = configuration["Authentication:GitHub:ClientId"];
        options.ClientSecret = configuration["Authentication:GitHub:ClientSecret"];
        options.Scope.Add("user:email");
    });

services.AddAuthorization(config =>
{
    config.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/PostEdit");

    options.Conventions.AuthorizeAreaFolder("Admin", "/", "RequireAdminRole");

    options.Conventions.Add(new PageRouteTransformerConvention(new SlugifyParameterTransformer()));

    options.Conventions.AddPageRoute("/PostEdit", "/posts/add");
});

services.AddReCaptcha(configuration.GetSection("ReCaptcha"));
services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

services.AddTransient<ITagService, TagService>();
services.AddTransient<ISendGridService, SendGridService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    context?.Database.Migrate();

    CreateAdminRoleIfNeeded(serviceScope.ServiceProvider).Wait();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();


static async Task CreateAdminRoleIfNeeded(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var adminRoleName = "Admin";

    if (!await roleManager.RoleExistsAsync(adminRoleName))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRoleName));
    }
}