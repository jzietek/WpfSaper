using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSaper.Model
{
    class Minefield
    {
        private readonly List<List<Tile>> tiles;        

        public Minefield(List<List<Tile>> tiles)
        {
            this.tiles = tiles;

            foreach(var tile in this.tiles.SelectMany(t => t))
            {
                tile.StateChanged += Tile_StateChanged;
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

        private void Tile_StateChanged(object sender, Tile.StateChangedEventArgs e)
        {
            Tile tile = sender as Tile;
            if (tile != null && e != null)
            {
                System.Diagnostics.Debug.WriteLine($"{tile.Id}: {e.CurrentState}");
            }
        }
    }
}
