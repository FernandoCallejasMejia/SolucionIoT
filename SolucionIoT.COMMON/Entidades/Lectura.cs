using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Entidades
{
    public class Lectura:BaseDTO
    {
        public string IdDispositivo { get; set; }

        public float Temperatura { get; set; }

        public float Luminosidad { get; set; }

        public float Humedad { get; set; }
    }
}
