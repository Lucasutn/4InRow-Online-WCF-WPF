using MaterialDesignThemes.Wpf;
using MVVM.Models;
using MVVM.Service;
using MVVM.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MVVM.Views;

namespace MVVM.ViewModels
{
    public class GameViewModel : ObservableObject
    {
        #region CAMPOS
        private OpcionesUsuario _opciones;
        #endregion

        #region PROPIEDADES
        public OpcionesUsuario Opciones
        {
            get { return _opciones; }
            set { OnPropertyChanged(ref _opciones, value); }
        }
        #endregion

        // Constructor
        public GameViewModel(OpcionesUsuario opciones)
        {
            Opciones = opciones;
        }

    }
}
