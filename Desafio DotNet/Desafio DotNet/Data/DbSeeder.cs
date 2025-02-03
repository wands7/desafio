using Desafio_DotNet.Models;

namespace Desafio_DotNet.Data
{
    public class DbSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(
                    new Status { Id = 1, Nome = "Pendente" },
                    new Status { Id = 2, Nome = "Em progresso" },
                    new Status { Id = 3, Nome = "Concluído" }
                );
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Id = 1, Nome = "Trabalho" },
                    new Category { Id = 2, Nome = "Pessoal" },
                    new Category { Id = 3, Nome = "Estudo" }
                );
            }

            context.SaveChanges();
        }
    }
}
