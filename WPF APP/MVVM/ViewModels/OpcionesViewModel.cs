using MaterialDesignThemes.Wpf;
using MVVM.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM.ViewModels
{
    public class OpcionesViewModel : ObservableObject
    {


        #region CAMPOS

        // Opciones globales del usuario
        public OpcionesUsuario _opcionesUsuario = new OpcionesUsuario();

        // Nombre del jugador
        private string _nombreJugador;

        // Dibujo de la ficha del usuario
        private PackIconKind _fichaKind;

        // Lista de los tipos de fichas posibles
        private ObservableCollection<string> _coleccionFichas = new ObservableCollection<string>()
        {
            "Default",
            "Cuadrado",
            "Corazón",
            "Ojo",
            "Basura"
        };

        // Ficha seleccionada en el ComboBox de tipos de fichas
        private string _fichaSeleccionada;

        // Colores de las fichas del usuario
        private ColorTheme _colores;

        // Lista de colores posibles
        private ObservableCollection<string> _coleccionColores = new ObservableCollection<string>()
        {
            "Moderno",
            "Clásico",
            "Alternativo"
        };

        // Color seleccionado en el ComboBox  de colores
        private string _coloresSeleccionados;
        #endregion

        #region PROPIEDADES

        public OpcionesUsuario OpcionesUsuario
        {
            get { return _opcionesUsuario; }
            set { OnPropertyChanged(ref _opcionesUsuario, value); }
        }

        public string NombreJugador
        {
            get { return _nombreJugador; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    OnPropertyChanged(ref _nombreJugador, value);
                else
                    OnPropertyChanged(ref _nombreJugador, "Visitante");

                OpcionesUsuario.NombreJugador = _nombreJugador; // actualizamos opciónes globales
            }
        }

        public PackIconKind FichaKind
        {
            get { return _fichaKind; }
            set { OnPropertyChanged(ref _fichaKind, value); }
        }
        
        public ObservableCollection<string> ColeccionFichas 
        {
            get { return _coleccionFichas; } 
            set { OnPropertyChanged(ref _coleccionFichas, value); }
        }

        public string FichaSeleccionada
        {
            get { return _fichaSeleccionada; }
            set 
            {
                FichaKind = StringToKind(value);
                OpcionesUsuario.DibujoFicha = FichaKind; // actualizamos opciónes globales
                OnPropertyChanged(ref _fichaSeleccionada, value);
            }
        }

        public ColorTheme Colores
        {
            get { return _colores; }
            set { OnPropertyChanged(ref _colores, value); }
        }

        public ObservableCollection<string> ColeccionColores
        {
            get { return _coleccionColores; }
            set { OnPropertyChanged(ref _coleccionColores, value); }
        }

        public string ColoresSeleccionados
        {
            get { return _coloresSeleccionados; }
            set
            {
                Colores = StringToColorTheme(value);
                OpcionesUsuario.ColoresFicha = Colores; // actualizamos opciónes globales
                OnPropertyChanged(ref _coloresSeleccionados, value);
            }
        }
        #endregion

        #region COMANDOS
        public ICommand VolverCommand { get; private set; }
        #endregion

        // Constructor
        public OpcionesViewModel()
        {
            NombreJugador = "Visitante";
            FichaSeleccionada = "Default";
            ColoresSeleccionados = "Moderno";

            VolverCommand = new RelayCommand(VolverInicio);
        }

        #region METODOS

        /// <summary>
        /// Permite volver al menú inicial
        /// </summary>
        private void VolverInicio()
        {
            MainWindow.mainWindow.VolverInicio();
        }

        private PackIconKind StringToKind(string value)
        {
            if (value.Equals("Cuadrado"))
                return PackIconKind.StopCircle;
            if (value.Equals("Corazón"))
                return PackIconKind.HeartCircle;
            if (value.Equals("Ojo"))
                return PackIconKind.EyeCircle;
            if (value.Equals("Basura"))
                return PackIconKind.TrashCircle;

            return PackIconKind.Circle;
        }

        private ColorTheme StringToColorTheme(string value)
        {
            // Colores por defecto (Moderno)
            SolidColorBrush j1 = new SolidColorBrush(Color.FromRgb(0, 105, 92));
            SolidColorBrush j2 = new SolidColorBrush(Color.FromRgb(233, 30, 99));

            if (value == "Clásico")
            {
                j1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0277BD"));
                j2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC62828"));
            }
            else if (value == "Alternativo")
            {
                j1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00C853"));
                j2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF00"));
            }

            return new ColorTheme(j1, j2);
        }
        #endregion
    }
}
