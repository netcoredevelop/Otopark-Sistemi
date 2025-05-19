using Application.Interfaces;
using Application.Services;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Infrastructure;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Authentication ve Authorization servislerini ekle
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.Cookie.Name = "Cookies";
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Dependency Injection
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<WebUI.Auth.AuthHelper>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuditLogActionFilter>();
builder.Services.AddScoped<IAuditLogService,AuditLogService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Bu sıralama önemli: önce Authentication, sonra Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
