using Desafio_DotNet.Data;
using Desafio_DotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Adicionar serviços
builder.Services.AddRazorPages();
builder.WebHost.UseIIS();

// Configurar Entity Framework para usar banco de dados em memória
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TarefaDB"));

// Adicionar Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;          // Exige pelo menos um número
    options.Password.RequireLowercase = true;      // Exige pelo menos uma letra minúscula
    options.Password.RequireUppercase = true;      // Exige pelo menos uma letra maiúscula
    options.Password.RequireNonAlphanumeric = true; // Exige pelo menos um caractere especial (!, @, #, etc.)
    options.Password.RequiredLength = 8;           // Define o mínimo de caracteres (ex: 8)
}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();


// Configuração de autenticação por cookie
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
app.UseAuthentication(); // Habilita autenticação
app.UseAuthorization(); // Habilita autorização
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

// Certifica-se de que está mapeando as Razor Pages
app.MapRazorPages();
app.Run();
