using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio_DotNet.Models
{
    [Authorize]
    public class Task
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status? Status { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Categoria { get; set; }

        public DateTime DataConclusao { get; set; }
    }
}
