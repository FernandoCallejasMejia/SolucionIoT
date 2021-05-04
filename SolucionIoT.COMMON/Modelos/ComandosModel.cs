using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Modelos
{
    public class ComandosModel
    {
        public string Comando { get; set; }
        public string Descripcion { get; set; }
        public ComandosModel(string comando, string descripcion)
        {
            Comando = comando;
            Descripcion = descripcion;
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
