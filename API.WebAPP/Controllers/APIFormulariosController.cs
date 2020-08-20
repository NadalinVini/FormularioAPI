using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.WebAPP.Data;
using API.WebAPP.Models;

namespace API.WebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIFormulariosController : ControllerBase
    {
        private readonly DataContext _context;

        public APIFormulariosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/APIFormularios
        [HttpGet]
        public IEnumerable<Formulario> GetFormulario()
        {
            return _context.Formulario;
        }

        // GET: api/APIFormularios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormulario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formulario = await _context.Formulario.FindAsync(id);

            if (formulario == null)
            {
                return NotFound();
            }

            return Ok(formulario);
        }

        // PUT: api/APIFormularios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormulario([FromRoute] int id, [FromBody] Formulario formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != formulario.ID)
            {
                return BadRequest();
            }

            _context.Entry(formulario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormularioExists(id))
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

        // POST: api/APIFormularios
        [HttpPost]
        public async Task<IActionResult> PostFormulario([FromBody] Formulario formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Formulario.Add(formulario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormulario", new { id = formulario.ID }, formulario);
        }

        // DELETE: api/APIFormularios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormulario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formulario = await _context.Formulario.FindAsync(id);
            if (formulario == null)
            {
                return NotFound();
            }

            _context.Formulario.Remove(formulario);
            await _context.SaveChangesAsync();

            return Ok(formulario);
        }

        private bool FormularioExists(int id)
        {
            return _context.Formulario.Any(e => e.ID == id);
        }
    }
}