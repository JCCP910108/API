using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Prueba.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class mtdDelUsuarioController : ControllerBase
    {

        private readonly DB_PRUEBA_BLAZORContext _context;

        public mtdDelUsuarioController(DB_PRUEBA_BLAZORContext context)
        {
            _context = context;
        }

        // DELETE api/<mtdDelUsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var Result = await _context.TblRusuarios.FindAsync(id);
                if (Result == null)
                {
                    return NotFound();
                }

                _context.TblRusuarios.Remove(Result);
                await _context.SaveChangesAsync();

                return NoContent();
            }           
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally { }
        }
    }
}
