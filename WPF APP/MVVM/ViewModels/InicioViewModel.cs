using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels
{
    public class InicioViewModel : ObservableObject
    {

        #region COMANDOS
        public RelayCommand OpcionesCommand { get; private set; }
        public RelayCommand NuevoJuegoCommand { get; private set; }
        public RelayCommand NuevoLocalCommand { get; private set; }
        #endregion

        // Constructor
        public InicioViewModel()
        {
            OpcionesCommand = new RelayCommand(MostrarOpciones);
            NuevoJuegoCommand = new RelayCommand(NuevoJuego);
            NuevoLocalCommand = new RelayCommand(NuevoJuegoLocal);
        }

        #region METODOS

        /// <summary>
        /// Muestra el menú de opciones
        /// </summary>
        private void MostrarOpciones()
        {
            MainWindow.mainWindow.MostrarOpciones();
        }

        /// <summary>
        /// Inicializa un nuevo juego
        /// </summary>
        private void NuevoJuego()
        {
            MainWindow.mainWindow.NuevoJuego();
        }

        /// <summary>
        /// Inicia un nuevo juego local
        /// </summary>
        private void NuevoJuegoLocal()
        {
            MainWindow.mainWindow.NuevoJuegoLocal();
        }

        #endregion
    }
}
