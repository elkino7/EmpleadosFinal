using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumirApi.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string str_status { get; set; }
        public string dat_fecha { get; set; }
        public string str_nombre { get; set; }
        public string str_direccion { get; set; }
        public string str_departamento { get; set; }
        public string int_telefono { get; set; }
    }
}