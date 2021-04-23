using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Entidades
{
    public class Dispositivo:BaseDTO
    {
        public  string IdUsuario { get; set; }

        public string Ubicacion { get; set; }

        public string Notas { get; set; }

        public string UsoRelevador1 { get; set; }
        
        public string UsoRelevador2 { get; set; }

        public string UsoRelevador3 { get; set; }

        public string UsoRelevador4 { get; set; }

        public string UsoBuzzer { get; set; }

        public string UbicacionPIR { get; set; }


    }
}
