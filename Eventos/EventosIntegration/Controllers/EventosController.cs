using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evento_Domain.Models;
using Eventos_Data.Context;
using Eventos_Data.Repository;
using Eventos_Data.Repository.Interfaces;
using Eventos_Data.UoW.Interfaces;
using Eventos_Data.UoW;
using EventosIntegration.DataContracts;

namespace EventosIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly EventosDbContext _context;
        private IRepository<Evento> eventoRepository;
        private IUnitOfWork unitOfWork;
        public EventosController(EventosDbContext context)
        {
            _context = context;
            eventoRepository = new EventoRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        // GET: api/Eventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
        {
            return await eventoRepository.ReadAll();
        }

        // GET: api/Eventos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(Guid id)
        {
            var evento = await eventoRepository.Read(id);

            if (evento == null)
            {
                return NotFound();
            }

            return evento;
        }

        // PUT: api/Eventos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(Guid id, Evento evento)
        {
            if (id != evento.Id)
            {
                return BadRequest();
            }

            _context.Entry(evento).State = EntityState.Modified;

            try
            {
                await unitOfWork.Commit();
                unitOfWork.Dispose();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await EventoExists(id))
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

        // POST: api/Eventos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento(EventoContract evento)
        {
            var Evento = new Evento
                (
                evento.Nome,
                evento.Descricao,
                evento.DataInicio,
                evento.DataFim
                );
            eventoRepository.Create(Evento);
            await unitOfWork.Commit();
            unitOfWork.Dispose();

            return CreatedAtAction("GetEvento", new { id = Evento.Id }, evento);
        }

        // DELETE: api/Eventos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evento>> DeleteEvento(Guid id)
        {
            var evento = await eventoRepository.Read(id);
            if (evento == null)
            {
                return NotFound();
            }

            eventoRepository.Delete(evento.Id);
            await unitOfWork.Commit();
            unitOfWork.Dispose();
            return evento;
        }

        private async Task<bool> EventoExists(Guid id)
        {
            var eventos = await eventoRepository.ReadAll();
            return eventos.Any(e => e.Id == id);
        }
    }
}
