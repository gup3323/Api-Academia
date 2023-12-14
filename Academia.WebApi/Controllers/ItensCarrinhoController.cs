using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academia.WebApi.Data;
using Models;

namespace Academia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensCarrinhoController : ControllerBase
    {
        private readonly AppAcademiaContext _context;

        public ItensCarrinhoController(AppAcademiaContext context)
        {
            _context = context;
        }

        // GET: api/ItensCarrinho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCarrinho>>> GetItensCarrinho()
        {
          if (_context.ItensCarrinho == null)
          {
              return NotFound();
          }
            return await _context.ItensCarrinho.ToListAsync();
        }

        // GET: api/ItensCarrinho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCarrinho>> GetItemCarrinho(int id)
        {
          if (_context.ItensCarrinho == null)
          {
              return NotFound();
          }
            var itemCarrinho = await _context.ItensCarrinho.FindAsync(id);

            if (itemCarrinho == null)
            {
                return NotFound();
            }

            return itemCarrinho;
        }
        [HttpGet("itemcarrinho/{carrinhoId}")]
        public async Task<ActionResult<IEnumerable<ItemCarrinhoDTO>>> GetProdutosPorId(int IdProduto)
        {
            
            var itensCarrinho = await _context.Carrinho.Include(p => p.Produtos).Where(p => p.IdProduto == IdProduto).ToListAsync();
            var itensCarrinhoDTO = new List<ItemCarrinhoDTO>();
            foreach (var item in itensCarrinho)
            {
                var ItemCarrinhoDTO = new ItemCarrinhoDTO
                {
                    Id = item.Id,
                    IdProduto = item.IdProduto,
                    Preco = item.ValorTotal,
                    Avaliacao = item.Avaliacao,
                    Quantidade = item.QtdProduto
                };
                itensCarrinhoDTO.Add(ItemCarrinhoDTO);
            }
            return itensCarrinhoDTO;
        }
        // PUT: api/ItensCarrinho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCarrinho(int id, ItemCarrinho itemCarrinho)
        {
            if (id != itemCarrinho.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemCarrinho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCarrinhoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItensCarrinho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemCarrinho>> PostItemCarrinho(ItemCarrinho itemCarrinho)
        {
          if (_context.ItensCarrinho == null)
          {
              return Problem("Entity set 'AppAcademiaContext.ItensCarrinho'  is null.");
          }
            _context.ItensCarrinho.Add(itemCarrinho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCarrinho", new { id = itemCarrinho.Id }, itemCarrinho);
        }

        // DELETE: api/ItensCarrinho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCarrinho(int id)
        {
            if (_context.ItensCarrinho == null)
            {
                return NotFound();
            }
            var itemCarrinho = await _context.ItensCarrinho.FindAsync(id);
            if (itemCarrinho == null)
            {
                return NotFound();
            }

            _context.ItensCarrinho.Remove(itemCarrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemCarrinhoExists(int id)
        {
            return (_context.ItensCarrinho?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
