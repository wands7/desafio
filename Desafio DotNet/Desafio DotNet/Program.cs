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
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
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

// Configurar middleware
app.UseAuthentication(); // Habilita autentica��o
app.UseAuthorization(); // Habilita autoriza��o
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tarefa}/{action=Index}/{id?}");

// Certifica-se de que est� mapeando as Razor Pages
app.MapRazorPages();
app.Run();
