using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSaper.model
{
    class Minefield : INotifyPropertyChanged
    {
        private readonly List<List<Tile>> tiles;

        public event PropertyChangedEventHandler PropertyChanged;

        public Minefield(List<List<Tile>> tiles)
        {
            this.tiles = tiles;
        }

        public List<List<Tile>> Tiles
        {
            get
            {
                return tiles;
            }
        }

        //public bool IsCleared { get; set; }

        public int BombsInMinefiled
        {
            get
            {
                return this.tiles.SelectMany(x => x.Select(y => y.HasBomb)).Count(z => z == true);
            }
        }
        

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }        
    }
}
