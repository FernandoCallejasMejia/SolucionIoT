using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolucionIoT.BIZ
{
    public class DispositivoManager : GenericManager<Dispositivo>, IDispositivoManager
    {
        IGenericRepository<Usuario> usuarioRepository;

        public DispositivoManager(IGenericRepository<Dispositivo> repository,IGenericRepository<Usuario> usuarioRepository) : base(repository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public Dispositivo DispositivoPerteneceAUsuario(string idDispositivo, string idUsuario)
        {
            return repository.Query(d => d.Id == idDispositivo && d.IdUsuario == idUsuario).SingleOrDefault();
        }

        public IEnumerable<Dispositivo> DispositivosDeUsuarioPorEmail(string email)
        {
            Usuario user = usuarioRepository.Query(u=>u.Correo==email).SingleOrDefault();
            return DispositivosDeUsuarioPorId(user.Id);
        }

        public IEnumerable<Dispositivo> DispositivosDeUsuarioPorId(string id)
        {
            return repository.Query(u => u.IdUsuario == id);
        }
    }
}
