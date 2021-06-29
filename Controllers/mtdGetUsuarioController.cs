using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Prueba.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Prueba.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class mtdGetUsuarioController : ControllerBase
    {

        private readonly DB_PRUEBA_BLAZORContext _context;

        public mtdGetUsuarioController(DB_PRUEBA_BLAZORContext context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<TblRusuario>> Get()
        {
            try
            {
                return await _context.TblRusuarios.ToListAsync();
            }            
            catch (Exception ex)
            {
                return (IEnumerable<TblRusuario>)BadRequest(ex.Message);
            }
            finally { }
        }

        // GET: api/<ValuesController>

        [HttpGet("{UsuNid}")]
        public async Task<ActionResult<TblRusuario>> Get(long UsuNid)
        {
            try
            {
                var Result = await _context.TblRusuarios.FindAsync(UsuNid);
                if (Result == null)
                {
                    return NotFound();
                }

                return Result;
            }          
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally { }
        }

        /*
          
         public static IConfiguration Configuration { get; set; } 
         
         [HttpGet("{idUsu}")]
        public IActionResult  Get(int idUsu)
        {
            List<DataTable> List = new List<DataTable>();
            DataTable dt = new DataTable();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                conn.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                conn.Open();

                cmd = new SqlCommand("SPR_GET_USUARIO", conn);
                cmd.Parameters.Add(new SqlParameter("@PI_USU_NID", idUsu));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0) { List.Add(dt); } else { return NotFound("No hay Informacion a Retornar"); };                    
                }
                else
                {
                    return NotFound("Error al Consumir el Procedimiento");
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                cmd.Dispose();
            }
            return Ok(List);
        }

         */

    }
}
