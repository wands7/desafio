using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Desafio_DotNet.Models
{
    [Authorize]
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        public string Status { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória")]
        public Category Categoria { get; set; }

        [Required(ErrorMessage = "A data de conclusão é obrigatória")]
        public DateTime DataConclusao { get; set; }

        // Construtor padrão
        public Task() { }

        // Construtor parametrizado
        public Task(string status, Category categoria, DateTime dataConclusao)
        {
            Status = status;
            Categoria = categoria;
            DataConclusao = dataConclusao;
        }
    }
}
