using DriveIt.Components;
using DriveIt.Components.Account;
using DriveIt.Data;
using DriveIt.EmailSenders;
using DriveIt.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddBlazorBootstrap();
builder.Services.AddHttpContextAccessor();

// TODO
var URI = /*Environment.GetEnvironmentVariable("DRIVEITAPI_URI") ??*/ "https://localhost:7289";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(URI) });
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<OfferService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<RentalService>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = Environment.GetEnvironmentVariable("AUTHENTICATION_GOOGLE_CLIENTID");
    googleOptions.ClientSecret = Environment.GetEnvironmentVariable("AUTHENTICATION_GOOGLE_CLIENTSECRET");
})
.AddIdentityCookies();

var connectionString = /*Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTION_STRING_BLAZOR") ??*/  builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<CarRentalContext>(opt =>
    opt.UseSqlServer(connectionString));

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

//builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
var key = Environment.GetEnvironmentVariable("DRIVEIT_SENDGRID_API_KEY");
builder.Services.AddSingleton<IGeneralEmailSender>(sp =>
    new SendGridEmailSender(Environment.GetEnvironmentVariable("DRIVEIT_SENDGRID_API_KEY")!));

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, MyIdentityEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
