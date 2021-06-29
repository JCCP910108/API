using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Prueba.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Prueba.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class mtdInsUsuarioController : ControllerBase
    {
        private readonly DB_PRUEBA_BLAZORContext _context;

        public mtdInsUsuarioController(DB_PRUEBA_BLAZORContext context)
        {
            _context = context;
        }
        
        // POST api/<mtdInsUsuarioController>
        [HttpPost]
        public async Task<ActionResult<TblRusuario>> Post([FromBody] TblRusuario value)
        {
            try
            {
                TblRusuario usu = new TblRusuario();
                usu = value;
                var Result = await _context.TblRusuarios.AddAsync(value);
                await _context.SaveChangesAsync();


                if (Result == null)
                {
                    return NotFound();
                }
                return Result.Entity;
            }                      
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally { }

        }

        /*public static IConfiguration Configuration { get; set; }

        // GET: api/<mtdInsUsuarioController>
        [HttpGet]
        public IActionResult Get(string UsuCdocumento, string UsuCnombre, string UsuCapellido, string UsuCtelefono, string UsuCdireccion, bool UsuOestado)
        {

            TblRusuario usu = new TblRusuario();

            usu.UsuCdocumento = UsuCdocumento;
            usu.UsuCnombre = UsuCnombre;
            usu.UsuCapellido = UsuCapellido;
            usu.UsuCtelefono = UsuCtelefono;
            usu.UsuCdireccion = UsuCdireccion;
            usu.UsuOestado = UsuOestado;

            List<string> list = new List<string>();

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

                cmd = new SqlCommand("SPR_INS_USUARIO", conn);
                cmd.Parameters.Add(new SqlParameter("@PI_CUSU_CDOCUMENTO", usu.UsuCdocumento));
                cmd.Parameters.Add(new SqlParameter("@PI_CUSU_CNOMBRE", usu.UsuCnombre));
                cmd.Parameters.Add(new SqlParameter("@PI_CUSU_CAPELLIDO", usu.UsuCapellido));
                cmd.Parameters.Add(new SqlParameter("@PI_CUSU_CTELEFONO", usu.UsuCtelefono));
                cmd.Parameters.Add(new SqlParameter("@PI_CUSU_CDIRECCION", usu.UsuCdireccion));
                cmd.Parameters.Add(new SqlParameter("@PI_OUSU_OESTADO", usu.UsuOestado));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0) 
                    { 
                        if (dt.Rows[0][0].ToString() != "0"){ return Ok(dt.Rows[0][0].ToString());} else { return NotFound("Usuario ya Existe"); }                       
                    } 
                    else 
                    { 
                        return NotFound("No hay Informacion a Retornar");
                    }
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

        }
        */



    }
}
