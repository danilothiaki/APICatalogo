using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCategoriasProduto")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProduto()
        {
            var categorias = _context.Categorias.Include(a => a.Produtos).ToList();

            return categorias;
        }

        [HttpGet("GetCategorias")]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            var categorias = _context.Categorias.ToList();

            if (categorias == null)
                return NotFound("Categorias não encontradas");


            return categorias;
        }

        [HttpGet("GetCategoria-{id:int}", Name = "ConsultarCategoria")]
        public ActionResult<Categoria> GetCategoria(int id)
        {
            var categoria = _context.Categorias.Where(a => a.CategoriaId == id).FirstOrDefault();

            if (categoria == null)
                return NotFound("Categorias não encontrados");

            return categoria;
        }

        [HttpPost("Adicionar-Categoria")]
        public ActionResult PostCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null)
                return BadRequest();

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ConsultarCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("PutCategoria-{id:int}")]
        public ActionResult PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("DeleteCategoria-{id:int}")]
        public ActionResult DeleteCategoria(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(a => a.CategoriaId == id);

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            if (categoria == null)
                return NotFound("Categoria não encontrado");

            return Ok(categoria);
        }
    }
}
