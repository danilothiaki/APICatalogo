using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = _context.Produtos.ToList();

            if (produtos == null)
                return NotFound("Produtos não encontrados");


            return produtos;
        }

        [HttpGet("{id:int}", Name = "ConsultarProduto")]
        public ActionResult<Produto> GetProduto(int id)
        {
            var produto = _context.Produtos.Where(a => a.ProdutoId == id).FirstOrDefault();

            if (produto == null)
                return NotFound("Produtos não encontrados");

            return produto;
        }

        [HttpPost]
        public ActionResult PostProduto([FromBody] Produto produto)
        {
            if (produto == null)
                return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ConsultarProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult PutProduto(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteProduto(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(a => a.ProdutoId == id);

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            if (produto == null) 
                return NotFound("Produto não encontrado");

            return Ok(produto);
        }
    }
}
