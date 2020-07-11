using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MVVM.Service
{
    public class JugadorCliente
    {
        private SolidColorBrush _color;

        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanPlay { get; set; }
        public SolidColorBrush Color { get => _color; set => _color = value; }

        public JugadorCliente(int id, string name, bool canPlay, SolidColorBrush color)
        {
            Id = id;
            Name = name;
            CanPlay = canPlay;
            Color = color;
        }
    }
}
