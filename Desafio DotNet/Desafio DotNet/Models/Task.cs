using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Desafio_DotNet.Models
{
    [Authorize]
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int CategoryId { get; set; }
        public int StatusId { get; set; }
        public DateTime? DataConclusao { get; set; }
        public virtual Category Categoria { get; set; }
        public virtual Status Status { get; set; } // false = Pendente, true = Concluído

        // Construtor padrão
        public Task() { }

        // Construtor parametrizado
        public Task(Status status, Category categoria, DateTime dataConclusao, int categoryId, int statusId)
        {
            Status = status;
            Categoria = categoria;
            DataConclusao = dataConclusao;
            CategoryId = categoryId;
            StatusId = statusId;
        }
    }
}
