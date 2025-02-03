using System.ComponentModel.DataAnnotations;

namespace Desafio_DotNet.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome do status deve ter no máximo 15 caracteres")]
        public string Nome { get; set; }

        // Construtor padrão
        public Status() { }

        // Construtor parametrizado
        public Status(string nome)
        {
            Nome = nome;
        }
    }
}
