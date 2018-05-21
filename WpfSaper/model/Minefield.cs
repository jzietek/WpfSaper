using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSaper.Model
{
    class Minefield : INotifyPropertyChanged
    {
        private readonly List<List<Tile>> tiles;

        public event PropertyChangedEventHandler PropertyChanged;

        public Minefield(List<List<Tile>> tiles)
        {
            this.tiles = tiles;

            foreach(var tile in this.tiles.SelectMany(t => t))
            {
                tile.StateChanged += Tile_StateChanged;
            }
        }

        private void Tile_StateChanged(object sender, Tile.StateChangedEventArgs e)
        {
            Tile tile = sender as Tile;
            if (tile != null && e != null)
            {
                System.Diagnostics.Debug.WriteLine($"{tile.Id}: {e.CurrentState}");
                HandleTileStateChange(tile, e.CurrentState);
            }
        }        

        public List<List<Tile>> Tiles
        {
            get
            {
                return tiles;
            }
        }        

        public int BombsInMinefiled
        {
            get
            {
                return this.tiles.SelectMany(x => x.Select(y => y.HasBomb)).Count(z => z == true);
            }
        }
        
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void HandleTileStateChange(Tile tile, Tile.TileState currentState)
        {
            //TODO
        }
    }
}
