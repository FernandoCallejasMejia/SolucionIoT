using SolucionIoT.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.BIZ.API
{
    public static class FactoryManager
    {
        public static IUsuarioManager UsuarioManager()
        {
            return new UsuarioManager();
        }
        public static ILecturaManager LecturaManager()
        {
            return new LecturaManager();
        }
        public static IDispositivoManager DispositivoManager()
        {
            return new DispositivoManager();
        }
        public static IAccionManager AccionManager()
        {
            return new AccionManager();
        }



        
    }
}
