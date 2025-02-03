using Desafio_DotNet.Data;
using Desafio_DotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Adicionar servi�os
builder.Services.AddRazorPages();
builder.WebHost.UseIIS();

// Configurar Entity Framework para usar banco de dados em mem�ria
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TarefaDB"));

// Adicionar Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;          // Exige pelo menos um n�mero
    options.Password.RequireLowercase = true;      // Exige pelo menos uma letra min�scula
    options.Password.RequireUppercase = true;      // Exige pelo menos uma letra mai�scula
    options.Password.RequireNonAlphanumeric = true; // Exige pelo menos um caractere especial (!, @, #, etc.)
    options.Password.RequiredLength = 8;           // Define o m�nimo de caracteres (ex: 8)
}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();


// Configura��o de autentica��o por cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbSeeder.SeedData(context);
}

// Configurar middleware
app.UseAuthentication(); // Habilita autentica��o
app.UseAuthorization(); // Habilita autoriza��o
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

// Certifica-se de que est� mapeando as Razor Pages
app.MapRazorPages();
app.Run();
