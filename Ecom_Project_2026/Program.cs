 using Ecom_Project_2026.Data;
using Ecom_Project_2026.DataAccess.Repository;
using Ecom_Project_2026.DataAccess.Repository.IRepository;
using Ecom_Project_2026.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PayPalCheckoutSdk.Core;
using Stripe;

    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("constr") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr"))); builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//var environment = new SandboxEnvironment("CLIENT_ID", "CLIENT_SECRET");
//var client = new PayPalHttpClient(environment);


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.Configure<StripeSettings>
    (builder.Configuration.GetSection("StripeSettings"));

var emailSettings = builder.Configuration.GetSection("EmailSettings");

//''//
var smtpDomain = builder.Configuration["EmailSettings:PrimaryDomain"];
var smtpPort = builder.Configuration["EmailSettings:PrimaryPort"];
var smtpUser = builder.Configuration["EmailSettings:UsernameEmail"];
var smtpPass = builder.Configuration["EmailSettings:UsernamePassword"];
var fromEmail = builder.Configuration["EmailSettings:FromEmail"];
var toEmail = builder.Configuration["EmailSettings:ToEmail"];
var ccEmail = builder.Configuration["EmailSettings:CcEmail"];
//""//
builder.Services.Configure<EmailSettings>(emailSettings);
//builder.Services.AddSingleton(new PayPalHttpClient(
//    new SandboxEnvironment("CLIENT_ID", "CLIENT_SECRET")));
//builder.Services.Configure<PaypalSettings>(
//builder.Configuration.GetSection("PayPalSettings"));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddAuthentication();
builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
    options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
});
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});
builder.Services.AddAuthentication()
    .AddInstagram(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Instagram:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Instagram:ClientSecret"];
    });
//.AddLinkedIn(options =>
//{
//    options.ClientId = "";
//    options.ClientSecret = "";
//});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

StripeConfiguration.ApiKey = builder.Configuration.
    GetSection("StripeSettings")["SecretKey"];

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
