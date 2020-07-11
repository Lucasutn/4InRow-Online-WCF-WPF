using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVM.Service;

namespace MVVM.ViewModels
{
    public class BuscandoJugadorViewModel
    {
        public ICommand CancelarCommand { get; private set; }

        public BuscandoJugadorViewModel()
        {
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private void Cancelar()
        {
            MainWindow.mainWindow.VolverInicio();


           // new JuegoDataService().EliminarJuego();
           
        }
    }
}
