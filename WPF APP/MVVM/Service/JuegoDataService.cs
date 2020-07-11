using MVVM.Models;
using MVVM.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Service
{
    public class JuegoDataService
    {

        private BasicHttpsBinding_IService1 serviceClient = new BasicHttpsBinding_IService1();


        public Juego iniciar()
        {

            return serviceClient.Iniciar();

        }

        public void updateGame(Juego juego)
        {

            serviceClient.ActualizarJuego(juego);

        }

        public void EliminarJuego(Juego juego)
        {

           serviceClient.EliminarJuego(juego);

        }

        public Juego getJuego(string id)
        {


            return serviceClient.getJuego(id);


        }
    }
}
