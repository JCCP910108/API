using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API_Prueba.Models
{
    public partial class TblRusuario
    {
        public long UsuNid { get; set; }
        public string UsuCdocumento { get; set; }
        public string UsuCnombre { get; set; }
        public string UsuCapellido { get; set; }
        public string UsuCtelefono { get; set; }
        public string UsuCdireccion { get; set; }
        public DateTime? UsuDfechaCreacion { get; set; }
        public bool? UsuOestado { get; set; }


    }


}
