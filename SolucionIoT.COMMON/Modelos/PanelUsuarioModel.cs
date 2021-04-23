using SolucionIoT.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Modelos
{
    public class PanelUsuarioModel
    {
        public List<Dispositivo> Dispositivos { get; set; }
        public Dispositivo DispositivoSeleccionado { get; set; }
        public Usuario Usuario { get; set; }
        public List<Lectura> LecturasDelDispositivo { get; set; }
    }
}
