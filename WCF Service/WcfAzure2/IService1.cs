using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfAzure2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IService1
    {


        [OperationContract]
        List<Jugador> Players();

        [OperationContract]
        Juego Iniciar();

        [OperationContract]
        void Limpiar();

        [OperationContract]
        void ActualizarJuego(Juego game);

        [OperationContract]
        Juego getJuego(string id);

        [OperationContract]
        void EliminarJuego(Juego juego);

        [OperationContract]
        List<Juego> ListaJuego();

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.


    [DataContract]
    public class Juego
    {
        
        private int[] matriz;
       
        private int[] contadores;

        private Jugador jugadorAzul;
       
        private Jugador jugadorRojo;

        private string id;

        bool ganador = false;

        public Juego()
        {
            Id = Guid.NewGuid().ToString();

            Matriz = new int[42];
            Contadores = new int[] { 5, 5, 5, 5, 5, 5, 5 };
        }

        [DataMember]
        public int[] Matriz { get => matriz; set => matriz = value; }
        [DataMember]
        public int[] Contadores { get => contadores; set => contadores = value; }
        [DataMember]
        public Jugador JugadorAzul { get => jugadorAzul; set => jugadorAzul = value; }
        [DataMember]
        public Jugador JugadorRojo { get => jugadorRojo; set => jugadorRojo = value; }
        [DataMember]
        public string Id { get => id; set => id = value; }
        [DataMember]
        public bool Ganador { get => ganador; set => ganador = value; }
    }

    [DataContract]
    public class Jugador
    {
       
        private int footprint;
       
        private string color;
        
        private bool status;

        public Jugador(int footprint)
        {
            Footprint = footprint;
        }

        public Jugador(int footprint, string color) : this(footprint)
        {
            this.Color = color;
        }

        public Jugador(int footprint, string color, bool status) : this(footprint, color)
        {
            this.Status = status;
        }

        public Jugador()
        {

        }

        [DataMember]
        public int Footprint { get => footprint; set => footprint = value; }
        [DataMember]
        public bool Status { get => status; set => status = value; }
        [DataMember]
        public string Color { get => color; set => color = value; }
    }


}
