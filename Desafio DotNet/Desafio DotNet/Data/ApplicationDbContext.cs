using Desafio_DotNet.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Desafio_DotNet.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Adiciona os Status iniciais
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Nome = "Pendente" },
                new Status { Id = 2, Nome = "Em progresso" },
                new Status { Id = 3, Nome = "Concluído" }
            );

            // Adiciona as Categorias iniciais
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Nome = "Trabalho" },
                new Category { Id = 2, Nome = "Pessoal" },
                new Category { Id = 3, Nome = "Estudo" }
            );
        }
    }
}
