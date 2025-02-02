using Desafio_DotNet.Data;
using Desafio_DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio_DotNet.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR todas as tarefas
        public async Task<IActionResult> Index()
        {
            var tarefas = await _context.Tarefas.Include(t => t.Categoria).ToListAsync();
            return View(tarefas);
        }

        // EXIBIR detalhes de uma tarefa específica
        public async Task<IActionResult> Details(int id)
        {
            var tarefa = await _context.Tarefas
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        // EXIBIR formulário de criação
        public IActionResult Create()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        // CRIAR uma nova tarefa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Task tarefa)
        {
            if (ModelState.IsValid)
            {
                // Atribuir a categoria correta (buscando pelo ID)
                tarefa.Categoria = await _context.Categorias.FindAsync(tarefa.Categoria.Id);

                _context.Tarefas.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = _context.Categorias.ToList();
            return View(tarefa);
        }

        // EXIBIR formulário de edição
        public async Task<IActionResult> Edit(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound();

            ViewBag.Categorias = _context.Categorias.ToList();
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
                    tarefa.Categoria = await _context.Categorias.FindAsync(tarefa.Categoria.Id);
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

            ViewBag.Categorias = _context.Categorias.ToList();
            return View(tarefa);
        }

        // EXIBIR formulário de exclusão
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas
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
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(e => e.Id == id);
        }
    }
}
