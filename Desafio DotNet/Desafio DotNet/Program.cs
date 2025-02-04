using Desafio_DotNet.Data;
using Desafio_DotNet.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.WebHost.UseIIS();

// Entity Framework banco de dados em mem�ria
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TarefaDB"));

// Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;          // Exige pelo menos um n�mero
    options.Password.RequireLowercase = true;      // Exige pelo menos uma letra min�scula
    options.Password.RequireUppercase = true;      // Exige pelo menos uma letra mai�scula
    options.Password.RequireNonAlphanumeric = true; // Exige pelo menos um caractere especial (!, @, #, etc.)
    options.Password.RequiredLength = 8;           // Define o m�nimo de caracteres (ex: 8)
}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();


// Autentica��o por cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Configura��es do cookie
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddSession(options =>
{
    // Configura��es de sess�o (se necess�rio)
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbSeeder.SeedData(context);
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

// Certifica-se de que est� mapeando as Razor Pages
app.MapRazorPages();
app.Run();
