﻿using System.ComponentModel.DataAnnotations;

namespace Desafio_DotNet.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome da categoria deve ter no máximo 50 caracteres")]
        public string Nome { get; set; }

        // Construtor padrão
        public Category() { }

        // Construtor parametrizado
        public Category(string nome)
        {
            Nome = nome;
        }
    }
}
