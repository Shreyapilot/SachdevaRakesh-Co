using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.Repository;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;
var builder = WebApplication.CreateBuilder(args);

{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<SachdevaCoDbContext>(options =>
        options.UseSqlServer(connectionString));

    builder.Services.AddScoped<ILoginRepository, LoginRepository>();
    builder.Services.AddScoped<IContactRepository, ContactRepository>();
    builder.Services.AddScoped<IAboutRepository, AboutRepository>();
    builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
    builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();


    builder.Services.Configure<SmtpViewModels>(builder.Configuration.GetSection("SmtpSettings"));

    builder.Services.AddControllersWithViews();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.Cookie.Name = "RememberMeSachdevaCo";
        options.LoginPath = "/Login/Login";

        options.ExpireTimeSpan = TimeSpan.FromDays(30);

    });


    builder.Services.AddMvc();



    var app = builder.Build();


    var customError = Convert.ToBoolean(builder.Configuration.GetSection("CustomError").Value);

    if (customError)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }
    }

    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Index}/{id?}");

    app.Run();
}
