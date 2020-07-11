using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModels
{
    public class GameOverViewModel : ObservableObject
    {
        #region CAMPOS
        private string _mensaje;
        #endregion

        #region PROPIEDADES
        public string Mensaje
        {
            get { return _mensaje; }
            set { OnPropertyChanged(ref _mensaje, value); }
        }
        #endregion

        #region COMANDOS
        public ICommand AceptarCommand { get; private set; }
        #endregion

        // Constructor
        public GameOverViewModel(string mensaje)
        {
            Mensaje = mensaje;

            AceptarCommand = new RelayCommand(Aceptar);
        }

        #region METODOS

        public void Aceptar()
        {
            MainWindow.mainWindow.VolverInicio();
        }
        #endregion
    }
}
