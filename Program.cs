using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_liger.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

// ѕолучаем строку подключени€ из appsettings.json (дл€ локальной разработки)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ѕровер€ем наличие переменной DATABASE_URL (дл€ Render)
var databaseUrl = builder.Configuration["DATABASE_URL"];
if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    // ѕарсим URL, который предоставл€ет Render
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);

    // ≈сли порт не указан в URL (Render часто его опускает), используем стандартный 5432
    var port = uri.Port > 0 ? uri.Port : 5432;

    connectionString = $"Host={uri.Host};Port={port};Database={uri.AbsolutePath.TrimStart('/')};Username={Uri.UnescapeDataString(userInfo[0])};Password={Uri.UnescapeDataString(userInfo[1])};SSL Mode=Disable";
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");