using MaterialDesignThemes.Wpf;
using MVVM.Models;
using MVVM.Service;
using MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MVVM.Views
{
    /// <summary>
    /// Lógica de interacción para LocalGameView.xaml
    /// </summary>
    public partial class LocalGameView : UserControl
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public LocalGameView()
        {


            InitializeComponent();

            Opciones = MainWindow.mainWindow.JuegoLocalViewModel.Opciones;

            Juego = new JuegoCliente();
            Juego.Jugadores = new JugadorCliente[]
            {
                new JugadorCliente(1, "Jugador 1", true, Opciones.ColoresFicha.ColorJ1), 
                new JugadorCliente(2, "Jugador 2", false, Opciones.ColoresFicha.ColorJ2) 
            };

            BuildPlayGrid();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();

            JugadorCliente jugadorActual = null;

            // Verificamos si algún jugador puede jugar
            // Si ninguno puede jugar "jugadorActual" seguirá siendo null
            if (Juego.Jugadores[0].CanPlay)
                jugadorActual = Juego.Jugadores[0];
            else if (Juego.Jugadores[1].CanPlay)
                jugadorActual = Juego.Jugadores[1];

            if (jugadorActual != null && jugadorActual.Id == 2)
            {


                int col = new Random().Next(7);


                if (Juego.Contador[col] >= 0)
                {

                    Juego.Anotacion(jugadorActual, col);

                    // Cambiamos de turno
                    Juego.Jugadores[0].CanPlay = !Juego.Jugadores[0].CanPlay;
                    Juego.Jugadores[1].CanPlay = !Juego.Jugadores[1].CanPlay;

                    BuildPlayGrid();


                }
            }


        }

        #region CAMPOS
        private JuegoCliente _juego;
        #endregion

        #region PROPIEDADES

        public OpcionesUsuario Opciones { get; set; }

        public JuegoCliente Juego
        {
            get { return _juego; }
            set { _juego = value; }
        }
        #endregion

        #region METODOS
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

                    if (Juego.Tablero[row, col] == 1)
                    {
                        // Marca jugador 1
                        cell.Foreground = Juego.Jugadores[0].Color;
                        cell.Kind = Opciones.DibujoFicha;
                    }
                    else if (Juego.Tablero[row, col] == 2)
                    {
                        // Marca jugador 2
                        cell.Foreground = Juego.Jugadores[1].Color;
                        cell.Kind = Opciones.DibujoFicha;
                    }

                    PlayGrid.Children.Add(cell);
                    Grid.SetColumn(cell, col);
                    Grid.SetRow(cell, row);

                }


            }

            if (Juego.VerificarGanador())
            {
                foreach (var j in Juego.Jugadores)
                {
                    j.CanPlay = false;
                }
                GameOverDialog("Fin del Juego");
            }

            if (Juego.VerificarEmpate())
            {
                foreach (var j in Juego.Jugadores)
                {
                    j.CanPlay = false;
                }
                GameOverDialog("Empate");
            }

        }


        public void MarkCell(object sender, MouseButtonEventArgs e)
        {


            dispatcherTimer = new DispatcherTimer();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);

            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);




            JugadorCliente jugadorActual = null;

            // Verificamos si algún jugador puede jugar
            // Si ninguno puede jugar "jugadorActual" seguirá siendo null
            if (Juego.Jugadores[0].CanPlay)
                jugadorActual = Juego.Jugadores[0];
            else if (Juego.Jugadores[1].CanPlay)
                jugadorActual = Juego.Jugadores[1];

            // Si ninguno puede jugar significa que el juego termino
            if (jugadorActual != null && jugadorActual.Id == 1)
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

                if (Juego.Contador[col] >= 0)
                {

                    Juego.Anotacion(jugadorActual, col);

                    // Cambiamos de turno
                    Juego.Jugadores[0].CanPlay = !Juego.Jugadores[0].CanPlay;
                    Juego.Jugadores[1].CanPlay = !Juego.Jugadores[1].CanPlay;

                    BuildPlayGrid();
                }
            }


            dispatcherTimer.Start();
            
           

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
            delay.Interval = TimeSpan.FromSeconds(1);
            delay.Tick += (obj, e) => Dialog.ShowDialog(new GameOverViewModel(mensaje));
            delay.Start();
        }

        #endregion
    }
}
