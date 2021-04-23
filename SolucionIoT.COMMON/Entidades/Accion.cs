using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Entidades
{
    public enum Actuador
    {
        Relevador1,
        Relevador2,
        Relevador3,
        Relevador4,
        Buzzer,
        Pir
    }

    public class Accion:BaseDTO
    {
        public string IdDispositivo { get; set; }

        public string Actuador { get; set; }

        public bool Estado { get; set; }
    }
}
