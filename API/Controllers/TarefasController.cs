using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/tarefa")]
public class TarefasController : ControllerBase
{   

    private readonly AppContext _ctx;

    public TarefasController(AppContext context) {
        _ctx = context;
    }

    [HttpGet("listartarefas")]
    public IActionResult ListarTarefas() 
    {
      try {
        List<Tarefa> tarefas = _ctx.Tarefas.ToList();
            return Ok();

      } catch(Exception e) {
        return BadRequest(e.Message);
      }
    }

     [HttpGet("listartarefasconcluida")]
    public IActionResult ListarTarefasConcluidas() 
    {
      try {
        List<Tarefa> tarefas = _ctx.Tarefas.Where(t => t.Status.Equals("Concluida")).ToList();
            return Ok();

      } catch(Exception e) {
        return BadRequest(e.Message);
      }
    }

         [HttpGet("listartarefasnaoconcluida")]
    public IActionResult ListarTarefasNaoConcluidas() 
    {
      try {
        List<Tarefa> tarefas = _ctx.Tarefas.Where(t => t.Status.Equals("Nao Concluidas") || t.Status.Equals("Em Andamento")).ToList();
            return Ok();

      } catch(Exception e) {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("cadastrartarefa")]
        public IActionResult CadastrarTarefa([FromBody] Tarefa tarefaObj) {

            try{
                if(tarefaObj.Status == null){
                    tarefaObj.Status = "Em Andamento";

                }

                Tarefa tarefa = new Tarefa{
                    Descricao = tarefaObj.Descricao,
                    Nome = tarefaObj.Nome,
                    Status = tarefaObj.Status
                };

                _ctx.Tarefas.Add(tarefa);
                _ctx.SaveChanges();
                return Created("", tarefa);

            } catch(Exception e ) {
                return BadRequest(e.Message);

            }

        }

        [HttpPatch("atualizartarefa/{tarefaId}")]
    public IActionResult AtualizarTarefa([FromRoute] int tarefaId)
    {
        try
        {   
           Tarefa tarefa = _ctx.Tarefas.Find(tarefaId);
           if(tarefa == null){
            return NotFound();
           }


           if(tarefa.Status == "Nao Iniciada" || tarefa.Status == "Em Andamento"){
            tarefa.Status = "Concluida";
           } else {
            tarefa.Status = "Em andamento";
           }

            _ctx.Tarefas.Update(tarefa);
            _ctx.SaveChanges(); //sempre passar isso apos alguma alteracao seja envio atualizacao ou delete
            return Ok();
        }
        catch(Exception e) { 
            return BadRequest(e.Message);
        }
    }

}
