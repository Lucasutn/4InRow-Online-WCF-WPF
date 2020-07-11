using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MVVM.Models
{
    /// <summary>
    /// Tema del usuario para el color de las fichas
    /// </summary>
    public class ColorTheme : ObservableObject
    {
        private SolidColorBrush _colorJ1;
        private SolidColorBrush _colorJ2;

        public SolidColorBrush ColorJ1 
        {
            get { return _colorJ1; }
            set { OnPropertyChanged(ref _colorJ1, value); } 
        }
        public SolidColorBrush ColorJ2
        {
            get { return _colorJ2; }
            set { OnPropertyChanged(ref _colorJ2, value); }
        }

        public ColorTheme(SolidColorBrush colorJ1, SolidColorBrush colorJ2)
        {
            ColorJ1 = colorJ1;
            ColorJ2 = colorJ2;
        }
    }
}
