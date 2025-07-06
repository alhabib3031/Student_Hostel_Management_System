using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MudBlazor.Services;
using Student_Hostel_Management_System.Components;
using Student_Hostel_Management_System.Components.Account;
using Student_Hostel_Management_System.Components.Pages.AdministrationComponents;
using Student_Hostel_Management_System.Data;
using Student_Hostel_Management_System.Services;
using Student_Hostel_Management_System.Services.Interfaces;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

//=====================Added Services=====================//
builder.Services.AddScoped<IAdminstrationDataService, AdminstrationDataService>();
builder.Services.AddScoped<IStudentsDataService, StudentsDataService>();

builder.Services.AddSyncfusionBlazor();

//TODO: // DI Dependency Injection  Read Read Read !



builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Action = Lambda expression to configure the DbContext with SQL Server.

builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(connectionString,
    sqlServerOptionsAction: SqlOperation =>
    {
        SqlOperation.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// we don't register services after this line !!!!!! but not always .

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
app.UseAuthentication(); // ✅ ضروري للمصادقة
app.UseAuthorization();  // ✅ ضروري للسماح / الرفض

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
