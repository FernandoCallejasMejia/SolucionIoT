using SolucionIoT.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Interfaces
{
    public interface ILecturaManager: IGenericManager<Lectura>
    {
        IEnumerable<Lectura> LecturasDelDispositivo(string id);
        
        IEnumerable<Lectura> LecturasDelDispositivo(string id, DateTime inicio, DateTime fin);

     
    }
}
