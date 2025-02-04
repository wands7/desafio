using Desafio_DotNet.Data;
using Desafio_DotNet.Models;
using Desafio_DotNet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Desafio_DotNet.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // EXIBIR detalhes de uma tarefa específica
        public async Task<IActionResult> Details(int id)
        {
            var tarefa = await _context.Tasks
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        // CRIAR uma nova tarefa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Task model)
        {            
            if(ModelState.IsValid)
            {
                _context.Tasks.Add(model);                
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Tarefa adicionada com sucesso!";
                return RedirectToAction("Tasks");
            }
            TempData["ErrorMessage"] = "Erro ao adicionar Tarefa!";
            return RedirectToAction("Tasks");
        }

        // GET: Lista todas as tarefas
        public IActionResult Tasks()
        {
            ViewBag.Categorias = _context.Categories.ToList();
            ViewBag.Statuses = _context.Statuses.ToList();

            var tasks = _context.Tasks
                        .Include(t => t.Categoria)
                        .Include(t => t.Status)
                        .ToList();

            return View("Tasks", tasks);
        }

        // EXIBIR formulário de edição
        public async Task<IActionResult> Edit(int id)
        {
            var tarefa = await _context.Tasks.Include(t => t.Categoria).Include(t => t.Status).FirstOrDefaultAsync(t => t.Id == id);
            if (tarefa == null)
                return NotFound();
           
            ViewBag.Categorias = _context.Categories.ToList();
            ViewBag.Status = _context.Statuses.ToList();
            return View(tarefa);
        }

        // ATUALIZAR uma tarefa existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Task tarefa)
        {
            if (id != tarefa.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Erro ao editar tarefa!";
                    return RedirectToAction("Tasks");
                    throw;
                }
            }
            TempData["SuccessMessage"] = "Tarefa editada com sucesso!";
            return RedirectToAction("Tasks");
        }

        // EXIBIR formulário de exclusão
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tasks
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id);
                        

            if (tarefa == null)
                return NotFound();

            _context.Remove(tarefa);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Tarefa excluída com sucesso!";

            return RedirectToAction("Tasks");
        }

        // EXCLUIR uma tarefa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tasks.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tasks.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
