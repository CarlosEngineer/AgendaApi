using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaApi.Context;
using AgendaApi.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return Ok(contato);
        }


        [HttpGet("{Id}")]
        public IActionResult ObterPorId(int  Id)
        {
            var contato = _context.Contatos.Find(Id);
            if(contato == null)
            return NotFound();

            return Ok(contato);
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));
            return Ok(contatos);
        }

        [HttpPut("{Id}")]
        public IActionResult Atualizar(int Id, Contato contato)
        {
            var contatBanco = _context.Contatos.Find(Id);   

            if(contatBanco == null )
                return NotFound();


                contatBanco.Nome =  contato.Nome;
                contatBanco.Telefone = contato.Telefone;
                contatBanco.Ativo = contato.Ativo;  

                _context.Contatos.Update(contatBanco);
                _context.SaveChanges();

                return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult Deletar(int Id)
        {
            var contatBanco = _context.Contatos.Find(Id);   

            if(contatBanco == null )
                return NotFound();

            _context.Contatos.Remove(contatBanco);
            _context.SaveChanges();
            return NoContent();
        }

    }
}