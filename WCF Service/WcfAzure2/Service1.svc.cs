using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfAzure2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {


        List<Jugador> jugadores = new List<Jugador>();

        List<Juego> juegos = new List<Juego>();



        //  Juego juego = new Juego();
        // Juego juego ;


        public void Limpiar()
        {
            jugadores.Clear();
        }

        public Juego getJuego(string id)
        {
            return juegos.Find(J => J.Id == id);
        }

        public Juego Iniciar()
        {
            Juego juego = juegos.Find(J => J.JugadorAzul == null);

            if (juego != null)
            {



                Jugador jugador = new Jugador(1, "Azul", true);



                juego.JugadorAzul = jugador;


                return juego;

            }
            else
            {

                juego = new Juego();

                Jugador jugador = new Jugador(2, "Rojo", false);


                juego.JugadorRojo = jugador;

                juegos.Add(juego);


            }



            return juego;

        }



        public List<Jugador> Players()
        {
            return jugadores;
        }

        public void ActualizarJuego(Juego juegoActualizado)
        {


            juegos[juegos.FindIndex(ind => ind.Id.Equals(juegoActualizado.Id))] = juegoActualizado;


        }

        public void EliminarJuego(Juego juego)
        {
            juegos.RemoveAt(juegos.FindIndex(ind => ind.Id.Equals(juego.Id)));
        }

        public List<Juego> ListaJuego()
        {
            return juegos;
        }
    }
}
