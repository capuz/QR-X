using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebOficinaDOM.Session
{
    public interface ISessionUsuario
    {
        int Rut { get; }
        string Usuario { get; }

        void Asignar(int rut, string usuario);
        void Limpiar();
    }
}
