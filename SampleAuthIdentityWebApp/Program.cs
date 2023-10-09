using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleAuthIdentityWebApp.Data;
using SampleAuthIdentityWebApp.Services;
using SampleAuthIdentityWebApp.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


// completes the di for Identity; must tell the Identity system which Db (dbContext) it will use
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{

    // many other things can be configured here
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    // many other things can be configured here
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";

});

// using the options pattern of the Configure method to setup smtp access for the email confirmation
// builder.Configuration... gets us access to the appsettings.json
builder.Services.Configure<SmtpSetting>(builder.Configuration.GetSection("SMTP"));

// this is a singleton because it is a service shared everywhere; it doesn't have to be recreated each time it is being used
// you can use transient (Singleton saves a little bit of resource
builder.Services.AddSingleton<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.Run();
