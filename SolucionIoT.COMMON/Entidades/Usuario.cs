using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Entidades
{
    public class Usuario:BaseDTO
    {
        public string Nombre { get; set; }
        
        public string Correo { get; set; }
        
        public string Password { get; set; }
    }
}
