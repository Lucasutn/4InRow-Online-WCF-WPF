using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        #region CAMPOS

        // Vista actual
        private object _currentView;

        // Vistas ViewModels
        private InicioViewModel _inicioVM;
        private OpcionesViewModel _opcionesVM;
        private GameViewModel _juegoVM;
        private LocalGameViewModel _juegoLocalVM;

        // Opciones de usuario  para crear nuevo juego
        private OpcionesUsuario _opciones;
        #endregion

        #region PROPIEDADES
        public object CurrentView
        {
            get { return _currentView; }
            set { OnPropertyChanged(ref _currentView, value); }
        }

        public InicioViewModel InicioViewModel
        {
            get { return _inicioVM; }
            private set { OnPropertyChanged(ref _inicioVM, value); }
        }

        public OpcionesViewModel OpcionesViewModel
        {
            get { return _opcionesVM; }
            private set { OnPropertyChanged(ref _opcionesVM, value); }
        }

        public GameViewModel JuegoViewModel
        {
            get { return _juegoVM; }
            private set { OnPropertyChanged(ref _juegoVM, value); }
        }
        
        public LocalGameViewModel JuegoLocalViewModel
        {
            get { return _juegoLocalVM; }
            private set { OnPropertyChanged(ref _juegoLocalVM, value); }
        }

        public OpcionesUsuario Opciones
        {
            get { return _opciones; }
            private set { OnPropertyChanged(ref _opciones, value); }
        }
        #endregion

        // Constructor
        public MainViewModel()
        {
            InicioViewModel = new InicioViewModel();
            OpcionesViewModel = new OpcionesViewModel();
            CurrentView = InicioViewModel;

            Opciones = OpcionesViewModel.OpcionesUsuario;
        }

        #region METODOS

        /// <summary>
        /// Permite volver a la vista inicial
        /// </summary>
        public void VolverInicio()
        {
            CurrentView = InicioViewModel;
        }

        /// <summary>
        /// Muestra el menú de opciones
        /// </summary>
        public void MostrarOpciones()
        {
            CurrentView = OpcionesViewModel;
        }

        public void NuevoJuego()
        {
            JuegoViewModel = new GameViewModel(Opciones);
            CurrentView = JuegoViewModel;
        }

        public void NuevoJuegoLocal()
        {
            JuegoLocalViewModel = new LocalGameViewModel(Opciones);
            CurrentView = JuegoLocalViewModel;
        }

        #endregion
    }
}
