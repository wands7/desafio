using Desafio_DotNet.Data;
using Desafio_DotNet.Models;
using Desafio_DotNet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
            var category = await _context.Categories.FindAsync(model.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Categoria inválida.");
                return View(model);
            }
            
            var status = await _context.Statuses.FindAsync(model.StatusId);
            if (status == null)
            {
                ModelState.AddModelError("StatusId", "Status inválido.");
                return View(model); 
            }
            
            model.Status = status;
            model.Categoria = category;

            if (ModelState.IsValid)
            {
                _context.Tasks.Add(model);
                TempData["SuccessMessage"] = "Tarefa adicionada com sucesso!";
                return RedirectToAction("Tasks");
            }
            return RedirectToAction("Tasks");
        }

        // GET: Lista todas as tarefas
        public IActionResult Tasks()
        {
           var tasks = _context.Tasks.Include(t => t.Categoria).ToList();
            return View("Tasks", tasks);
        }

        // EXIBIR formulário de edição
        public async Task<IActionResult> Edit(int id)
        {
            var tarefa = await _context.Tasks.FindAsync(id);
            if (tarefa == null)
                return NotFound();

            ViewBag.Categorias = _context.Categories.ToList();
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
                    tarefa.Categoria = await _context.Categories.FindAsync(tarefa.Categoria.Id);
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = _context.Categories.ToList();
            return View(tarefa);
        }

        // EXIBIR formulário de exclusão
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tasks
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa == null)
                return NotFound();

            return View(tarefa);
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
