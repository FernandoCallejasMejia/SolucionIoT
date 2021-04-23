using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.BIZ
{
    public class LecturasManager : GenericManager<Lectura>, ILecturaManager
    {
        public LecturasManager(IGenericRepository<Lectura> repository) : base(repository)
        {
        }

        public IEnumerable<Lectura> LecturasDelDispositivo(string id)
        {
            return repository.Query(l => l.IdDispositivo == id);
        }

        public IEnumerable<Lectura> LecturasDelDispositivo(string id, DateTime inicio, DateTime fin)
        {
            return repository.Query(l => l.IdDispositivo == id && l.FechaHora>=inicio && l.FechaHora<=fin);
        }
    }
}
