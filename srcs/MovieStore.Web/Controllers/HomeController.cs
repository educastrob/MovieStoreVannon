using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data.Context;
using MovieStore.Data.Entities;
using MovieStore.Services.Services;

namespace MovieStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUsuarioService _usuarioService;
        private readonly IClienteService _clienteService;
        private readonly IFilmeService _filmeService;
        private readonly ILocacaoService _locacaoService;

        public HomeController(AppDbContext context, IUsuarioService usuarioService, 
            IClienteService clienteService, IFilmeService filmeService, ILocacaoService locacaoService)
        {
            _context = context;
            _usuarioService = usuarioService;
            _clienteService = clienteService;
            _filmeService = filmeService;
            _locacaoService = locacaoService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.Where(u => u.Ativo).ToListAsync();
            var clientes = await _context.Clientes.Where(c => c.Ativo).ToListAsync();
            var filmes = await _context.Filmes.Where(f => f.Ativo).ToListAsync();
            var locacoes = await _context.Locacoes
                .Include(l => l.Cliente)
                .Include(l => l.Filme)
                .ToListAsync();

            ViewBag.Usuarios = usuarios;
            ViewBag.Clientes = clientes;
            ViewBag.Filmes = filmes;
            ViewBag.Locacoes = locacoes;
            ViewBag.TotalUsuarios = usuarios.Count;
            ViewBag.TotalClientes = clientes.Count;
            ViewBag.TotalFilmes = filmes.Count;
            ViewBag.TotalLocacoes = locacoes.Count;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _usuarioService.CreateUsuarioAsync(usuario);
                    TempData["SuccessMessage"] = "Usuário adicionado com sucesso!";
                }
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddCliente(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _clienteService.CreateClienteAsync(cliente);
                    TempData["SuccessMessage"] = "Cliente adicionado com sucesso!";
                }
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddFilme(Filme filme)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _filmeService.CreateFilmeAsync(filme);
                    TempData["SuccessMessage"] = "Filme adicionado com sucesso!";
                }
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddLocacao(Locacao locacao)
        {
            try
            {
                // Definir valor antes da validação
                var filme = await _context.Filmes.FindAsync(locacao.FilmeId);
                if (filme != null)
                {
                    locacao.Valor = filme.ValorLocacao;
                }
                else if (locacao.Valor == 0)
                {
                    locacao.Valor = 5.00m; // Valor padrão se filme não encontrado
                }

                // Revalidar o ModelState após definir o valor
                ModelState.Clear();
                TryValidateModel(locacao);

                if (ModelState.IsValid)
                {
                    await _locacaoService.CreateLocacaoAsync(locacao);
                    TempData["SuccessMessage"] = "Locação adicionada com sucesso!";
                }
                else
                {
                    // Se ModelState não é válido, mostrar erros
                    var errors = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["ErrorMessage"] = $"Erro de validação: {errors}";
                }
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro interno do servidor: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                await _usuarioService.DeleteUsuarioAsync(id);
                TempData["SuccessMessage"] = "Usuário excluído com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Erro ao excluir usuário. Tente novamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                await _clienteService.DeleteClienteAsync(id);
                TempData["SuccessMessage"] = "Cliente excluído com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Erro ao excluir cliente. Tente novamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            try
            {
                await _filmeService.DeleteFilmeAsync(id);
                TempData["SuccessMessage"] = "Filme excluído com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Erro ao excluir filme. Tente novamente.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLocacao(int id)
        {
            try
            {
                await _locacaoService.DeleteLocacaoAsync(id);
                TempData["SuccessMessage"] = "Locação excluída com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Erro ao excluir locação. Tente novamente.";
            }
            return RedirectToAction("Index");
        }
    }
}