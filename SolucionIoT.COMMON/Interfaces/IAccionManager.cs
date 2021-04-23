using SolucionIoT.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Interfaces
{
    public interface IAccionManager: IGenericManager<Accion>
    {
        IEnumerable<Accion> AccionesDelDispositivo(string id);
        
        IEnumerable<Accion> AccionesDelDispositivo(string id, DateTime inicio, DateTime fin);
        
        IEnumerable<Accion> AccionesDelDispositivo(string id, string actuador, DateTime inicio, DateTime fin);
    }
}
