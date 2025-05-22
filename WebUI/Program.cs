using Application.Interfaces;
using Application.Services;
using Data.Seeds;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Infrastructure;
using Infrastructure.Persistence.Context;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

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

// Veritabanı migration ve seed işlemleri
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Migration'ları uygula
        var seedData = new SeedData(context);
        await seedData.SeedAsync(); // Seed dataları ekle
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı migration veya seed işlemi sırasında bir hata oluştu.");
    }
}

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
    pattern: "{controller=PublicPage}/{action=Index}/{id?}");

app.Run();
