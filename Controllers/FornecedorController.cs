using Microsoft.AspNetCore.Mvc;
using AS.Models;
using AS.Repositories;
using System.Collections.Generic;

namespace AS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorRepository _repository;

        public FornecedoresController(IFornecedorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Fornecedor>> Get()
        {
            var fornecedores = _repository.GetAll();
            return Ok(fornecedores);
        }

        [HttpGet("{id}")]
        public ActionResult<Fornecedor> Get(int id)
        {
            var fornecedor = _repository.GetById(id);
            if (fornecedor == null)
                return NotFound();
            return Ok(fornecedor);
        }

        [HttpPost]
        public ActionResult Post(Fornecedor fornecedor)
        {
            _repository.Add(fornecedor);
            return CreatedAtAction(nameof(Get), new { id = fornecedor.Id }, fornecedor);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Fornecedor fornecedorAtualizado)
        {
            var fornecedor = _repository.GetById(id);
            if (fornecedor == null)
                return NotFound();

             fornecedor.Nome = fornecedorAtualizado.Nome;
             fornecedor.CNPJ = fornecedorAtualizado.CNPJ;  // Atualização do CNPJ
             fornecedor.Telefone = fornecedorAtualizado.Telefone;
             fornecedor.Email = fornecedorAtualizado.Email;
             fornecedor.Endereco = fornecedorAtualizado.Endereco;
            _repository.Update(fornecedor);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var fornecedor = _repository.GetById(id);
            if (fornecedor == null)
                return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
    }
}
