using MaterialDesignThemes.Wpf;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MVVM.Models
{
    public class OpcionesUsuario : ObservableObject
    {
        private string _nombreJugador;
        private PackIconKind _dibujoFicha;
        private ColorTheme _coloresFicha;

        public string NombreJugador
        {
            get { return _nombreJugador; }
            set { OnPropertyChanged(ref _nombreJugador, value); }
        }
        public PackIconKind DibujoFicha
        {
            get { return _dibujoFicha; }
            set { OnPropertyChanged(ref _dibujoFicha, value); }
        }
        public ColorTheme ColoresFicha
        {
            get { return _coloresFicha; }
            set { OnPropertyChanged(ref _coloresFicha, value); }
        }

        public OpcionesUsuario()
        {
            // Opciones por defecto
            NombreJugador = "Visitante";
            DibujoFicha = PackIconKind.Circle;
            ColoresFicha = new ColorTheme(new SolidColorBrush(Color.FromRgb(0, 105, 92)), new SolidColorBrush(Color.FromRgb(233, 30, 99)));
        }
    }
}
