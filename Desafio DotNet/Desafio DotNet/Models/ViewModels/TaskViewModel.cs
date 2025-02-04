using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Desafio_DotNet.Models.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Selecione um status.")]
        public List<SelectListItem> Status { get; set; } // Ex: "Pendente", "Em andamento", "Concluído"

        [Required(ErrorMessage = "A data de conclusão é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime CompletionDate { get; set; }
        [Required(ErrorMessage = "Selecione uma Categoria.")]
        public List<SelectListItem> Category { get; set; } // Ex: "Trabalho", "Escola", "Pessoal"

        public TaskViewModel() { }

        public TaskViewModel(int id, string title, string description, List<SelectListItem> status, DateTime completionDate, List<SelectListItem> category)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = status;
            CompletionDate = completionDate;
            Category = category;
        }
    }
}
