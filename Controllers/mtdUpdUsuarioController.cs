using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Web_API_Prueba.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class mtdUpdUsuarioController : ControllerBase
    {

        public IConfiguration Configuration { get; }

        private readonly DB_PRUEBA_BLAZORContext _context;

        public mtdUpdUsuarioController(DB_PRUEBA_BLAZORContext context)
        {
            _context = context;
        }

        // PUT api/<mtdUpdUsuarioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TblRusuario>> Put(long id, TblRusuario Usuario)
        {
            TblRusuario usu = new TblRusuario();
            usu = Usuario;

            try
            {
                var person = await _context.TblRusuarios.FindAsync(id);
                if (person == null)
                {
                    return NotFound();
                }

                person.UsuCdocumento = usu.UsuCdocumento;
                person.UsuCnombre = usu.UsuCnombre;
                person.UsuCapellido = usu.UsuCapellido;
                person.UsuCtelefono = usu.UsuCtelefono;
                person.UsuCdireccion = usu.UsuCdireccion;
                person.UsuOestado = usu.UsuOestado;
                await _context.SaveChangesAsync();

                return Usuario;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally { }

            
        }

    }
}
