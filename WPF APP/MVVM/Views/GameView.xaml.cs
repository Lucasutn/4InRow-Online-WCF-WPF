using MaterialDesignThemes.Wpf;
using MVVM.Models;
using MVVM.Service;
using MVVM.ServiceReference1;
using MVVM.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MVVM.Views
{
    /// <summary>
    /// Lógica de interacción para GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {

        // Opciones
        public OpcionesUsuario Opciones { get; private set; }


        //---------------Atributos------------


        private static JuegoDataService _service = new JuegoDataService();

        private Juego _juegoServer = _service.iniciar();

        private string juegoServerID;

        private Jugador _jugador;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();


        public JuegoCliente juegoCliente { get; set; } = new JuegoCliente();


        //---------------Atributos------------



        public GameView()
        {

            InitializeComponent();

            Opciones = MainWindow.mainWindow.JuegoViewModel.Opciones;

            juegoServerID = _juegoServer.Id;

            EstablecerJugadorEnCliente();

            EstablecerJugadorColorEnGrid();

            BuildPlayGrid();


            //  DispatcherTimer setup

            dispatcherTimer = new DispatcherTimer();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);

            //dispatcherTimer.Interval = new TimeSpan(0,0,2);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);

            //dispatcherTimer.Start();


            if (_jugador.Status == false)
            {

                BuscandoJugadorDialog();
                dispatcherTimer.Start();


            }



        }


        //---------Funciones----------------//

        private void EstablecerJugadorEnCliente()
        {


            if (_juegoServer.JugadorAzul == null)
            {

                _jugador = _juegoServer.JugadorRojo;



            }
            else
            {

                _jugador = _juegoServer.JugadorAzul;


            }


            JugadorCliente j1 = new JugadorCliente(1, "Jugador 1", true, Opciones.ColoresFicha.ColorJ1);

            JugadorCliente j2 = new JugadorCliente(2, "Jugador 2", false, Opciones.ColoresFicha.ColorJ2);



            juegoCliente.Jugadores = new JugadorCliente[] { j1, j2 };

            juegoCliente.Tablero = Converter.ArrayToMatrix(_juegoServer.Matriz);

            juegoCliente.Contador = _juegoServer.Contadores;

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            

            _juegoServer = _service.getJuego(juegoServerID);

            if (_juegoServer.JugadorAzul != null && _juegoServer.JugadorRojo != null)
            {

                // Si hay un jugador azul cerramos dialogo de busqueda
                if (_juegoServer.JugadorAzul != null) Dialog.IsOpen = false;

                if (_jugador.Color == "Rojo")
                {

                    if (_juegoServer.JugadorRojo.Status)
                    {

                        ActualizarCliente(_juegoServer.JugadorRojo);


                    }

                }
                else
                {

                    if (_juegoServer.JugadorAzul.Status)
                    {

                        ActualizarCliente(_juegoServer.JugadorAzul);

                    }
                }

            }
            else {

                dispatcherTimer.Interval = new TimeSpan();
            }

        }

        private void ActualizarCliente(Jugador jugadorActualizado)
        {
            dispatcherTimer.Stop();



            juegoCliente.Tablero = Converter.ArrayToMatrix(_juegoServer.Matriz);
            juegoCliente.Contador = _juegoServer.Contadores;

            BuildPlayGrid();

            _jugador = jugadorActualizado;
        }

        public void ActualizarServidor()
        {

            _juegoServer.Matriz = Converter.MatrixToArray(juegoCliente.Tablero);
            _juegoServer.Contadores = juegoCliente.Contador;

            if (_jugador.Color == "Rojo")
            {

                _juegoServer.JugadorRojo.Status = false;
                _juegoServer.JugadorAzul.Status = true;

            }
            else
            {

                _juegoServer.JugadorAzul.Status = false;

                _juegoServer.JugadorRojo.Status = true;
            }



            _service.updateGame(_juegoServer);

        }

        //---------Funciones----------------//

        public void CambiarColorGridTurno(bool turno)
        {

            //podria llamar la funcion en el timer y en markCell luego de hacer una marca

            if (turno)
            {

                if (_jugador.Color == "Rojo")
                {

                    BottomLeft.Foreground = Opciones.ColoresFicha.ColorJ2;


                    BottomRight.Foreground = Opciones.ColoresFicha.ColorJ1;



                }
                else
                {

                    BottomLeft.Foreground = Opciones.ColoresFicha.ColorJ1;


                    BottomRight.Foreground = Opciones.ColoresFicha.ColorJ2;


                }
            }
            else
            {

                //invertir los colores

            }

        }
        public void EstablecerJugadorColorEnGrid()
        {


            if (_jugador.Color == "Rojo") // es el J2
            {

                BottomLeft.Foreground = Opciones.ColoresFicha.ColorJ2;
                BottomTextLeft.Text = Opciones.NombreJugador;

                BottomRight.Foreground = Opciones.ColoresFicha.ColorJ1;
                BottomTextRight.Text = "Adversario";


            }
            else // es el J1
            {

                BottomLeft.Foreground = Opciones.ColoresFicha.ColorJ1;
                BottomTextLeft.Text = Opciones.NombreJugador;

                BottomRight.Foreground = Opciones.ColoresFicha.ColorJ2;
                BottomTextRight.Text = "Adversario";

            }

        }
        public void BuildPlayGrid()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    var cell = new PackIcon
                    {
                        Kind = PackIconKind.Circle,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 70,
                        Height = 70,
                        Foreground = new SolidColorBrush(Color.FromRgb(50, 50, 50))

                    };

                    if (juegoCliente.Tablero[row, col] == 1)
                    {
                        // Marca jugador 1
                        cell.Foreground = juegoCliente.Jugadores[0].Color;
                        cell.Kind = Opciones.DibujoFicha;
                    }
                    else if (juegoCliente.Tablero[row, col] == 2)
                    {
                        // Marca jugador 2
                        cell.Foreground = juegoCliente.Jugadores[1].Color;
                        cell.Kind = Opciones.DibujoFicha;
                    }

                    PlayGrid.Children.Add(cell);
                    Grid.SetColumn(cell, col);
                    Grid.SetRow(cell, row);

                }


            }

            if (juegoCliente.VerificarGanador())
            {
                if (_jugador.Color == "Rojo")
                {
                    GameOverDialog(_juegoServer.JugadorRojo.Status == false ? "GANASTE" : "PERDISTE");
                }
                else
                {
                    GameOverDialog(_juegoServer.JugadorAzul.Status == false ? "GANASTE" : "PERDISTE");
                }

                //----------------//-------Actualizar servidor--------////////

                ActualizarServidor();

                dispatcherTimer.Stop();


                //----------------//-------Actualizar servidor--------////////

                //Dispatcher.InvokeShutdown();
                dispatcherTimer.Interval = new TimeSpan();

                EliminarJuegoIfGanadorEmpate();
            }

            if (juegoCliente.VerificarEmpate())
            {
                GameOverDialog("EMPATE");
                ActualizarServidor();
                dispatcherTimer.Stop();
                dispatcherTimer.Interval = new TimeSpan();
                EliminarJuegoIfGanadorEmpate();
            }

           


        }

        private void EliminarJuegoIfGanadorEmpate() {



            if (_juegoServer.Ganador == false)
            {

                _juegoServer.Ganador = true;

                ActualizarServidor();
            }
            else
            {

                _service.EliminarJuego(_juegoServer);
            }

        }



        #region Testing


        public void MarkCell(object sender, MouseButtonEventArgs e)
        {
            if (_jugador.Status == true)
            {

                // Determinamos la columna según la posición del mouse
                var point = Mouse.GetPosition(PlayGrid);
                int col = 0;
                double accumulatedWidth = 0.0;
                foreach (var columnDefinition in PlayGrid.ColumnDefinitions)
                {
                    accumulatedWidth += columnDefinition.ActualWidth;
                    if (accumulatedWidth >= point.X)
                        break;
                    col++;
                }



                if (juegoCliente.Contador[col] >= 0)
                {


                    if (_jugador.Footprint == juegoCliente.Jugadores[0].Id)
                    {

                        juegoCliente.Anotacion(juegoCliente.Jugadores[0], col);

                    }
                    else
                    {

                        juegoCliente.Anotacion(juegoCliente.Jugadores[1], col);

                    }


                    _jugador.Status = false;

                    //  BuildPlayGrid();

                    //----------------//-------Actualizar servidor--------////////


                    ActualizarServidor();

                    dispatcherTimer.Start();

                    BuildPlayGrid();

                    //----------------//-------Actualizar servidor--------////////





                }


            }

        }



        public void Mark(UIElement item, int col, int row)
        {
            PlayGrid.Children.Add(item);
            Grid.SetColumn(item, col);
            Grid.SetRow(item, row);
        }


        public void GameOverDialog(string mensaje)
        {
            var delay = new DispatcherTimer();
            delay.Interval = TimeSpan.FromSeconds(2);
            delay.Tick += (obj, e) => Dialog.ShowDialog(new GameOverViewModel(mensaje));
            delay.Start();
        }

        public void BuscandoJugadorDialog()
        {
            Dialog.ShowDialog(new BuscandoJugadorViewModel());
        }

        #endregion
    }
}
