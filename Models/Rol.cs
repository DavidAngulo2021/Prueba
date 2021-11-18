using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_gestion_caso.Models
{
    public class Rol
    {
        public int id_ticket { get; set; }
        public string usuario { get; set; }
        public int estado { get; set; }
        public DateTime f_creacion { get; set; }
        public DateTime f_actualizacion { get; set; }
    }
}