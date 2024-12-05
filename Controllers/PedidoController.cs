using Microsoft.AspNetCore.Mvc;
using AS.Models;
using AS.Repositories;
using System.Collections.Generic;

namespace AS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _repository;

        public PedidosController(IPedidoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> Get()
        {
            var pedidos = _repository.GetAll();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> Get(int id)
        {
            var pedido = _repository.GetById(id);
            if (pedido == null)
                return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        public ActionResult Post(Pedido pedido)
        {
            _repository.Add(pedido);
            return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Pedido pedidoAtualizado)
        {
            var pedido = _repository.GetById(id);
            if (pedido == null)
                return NotFound();

             pedido.Descricao = pedidoAtualizado.Descricao;
             pedido.ValorTotal = pedidoAtualizado.ValorTotal;
             pedido.Status = pedidoAtualizado.Status;
             pedido.Data = pedidoAtualizado.Data;
 
            _repository.Update(pedido);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var pedido = _repository.GetById(id);
            if (pedido == null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();

            
        }
    }
}
