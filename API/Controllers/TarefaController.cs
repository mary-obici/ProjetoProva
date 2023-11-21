using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers;

[Route("api/tarefa")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly AppDataContext _context;

    public TarefaController(AppDataContext context) =>
        _context = context;

    // GET: api/tarefa/listar
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria).ToList();
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // POST: api/tarefa/cadastrar
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Tarefa tarefa)
    {
        try
        {
            Categoria? categoria = _context.Categorias.Find(tarefa.CategoriaId);
            if (categoria == null)
            {
                return NotFound();
            }
            tarefa.Categoria = categoria;
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
        [Route("naoconcluidas")]
        public IActionResult GetByStreet([FromRoute] string StatusTarefa)
        {
            try
            {
                Tarefa? tarefa = _context.Tarefas.FirstOrDefault(x => x.StatusTarefa == StatusTarefa);
                if (tarefa != null)
                {
                    return Ok(tarefa);
                }
                return NotFound($"Endereço com a rua '{StatusTarefa}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao buscar endereço: {e.Message}");
            }
        }


          [HttpPut]
        [Route("alterar{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Tarefa tarefa)
        {
            try
            {
                Tarefa? existingTarefa = _context.Tarefas.FirstOrDefault(x => x.TarefaId == id);
                if (existingTarefa != null)
                {
                    existingTarefa.Titulo = tarefa.Titulo;
                    existingTarefa.Descricao = tarefa.Descricao;
                    existingTarefa.Categoria = tarefa.Categoria;
                    existingTarefa.StatusTarefa = tarefa.StatusTarefa;

                    _context.Tarefas.Update(existingTarefa);
                    _context.SaveChanges();

                    return Ok();
                }
                return NotFound($"Endereço com ID '{id}' não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao atualizar endereço: {e.Message}");
            }
        }
}
