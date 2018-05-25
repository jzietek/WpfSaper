using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSaper.Model
{
    class Minefield
    {
        public event EventHandler<GameEndedEventArgs> GameEnded;
        public bool IsGameEnded;

        private readonly List<List<Tile>> tiles;

        private int bombsInMinefield = -1;
        private int tilesCovered;

        public Minefield(List<List<Tile>> tiles)
        {
            this.tiles = tiles;

            foreach(var tile in this.tiles.SelectMany(t => t))
            {
                tile.StateChanged += Tile_StateChanged;
                tilesCovered++;
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
                if (bombsInMinefield == -1)
                {
                    bombsInMinefield = this.tiles.SelectMany(x => x.Select(y => y.HasBomb)).Count(z => z == true);
                }
                return bombsInMinefield;
            }
        }

        private void Tile_StateChanged(object sender, Tile.StateChangedEventArgs e)
        {
            Tile tile = sender as Tile;
            if (tile != null && e != null)
            {
                System.Diagnostics.Debug.WriteLine($"{tile.Id}: {e.CurrentState}");
                             
                //Handle covered count
                if (e.CurrentState == Tile.TileState.Covered)
                {
                    tilesCovered++;
                }
                else
                {
                    tilesCovered--;
                }

                //Check end game criteria
                if (e.CurrentState == Tile.TileState.Exploded)
                {
                    OnGameEnd(false);
                    return;
                }
                if (tilesCovered <= 0)
                {
                    OnGameEnd(true);
                    return;
                }

                //Propagate uncovering
                if (tile.BombsAround == 0)
                {
                    foreach(var n in tile.Neighbours)
                    {
                        n.UncoverTile();
                    }
                }
            }
        }

        private void OnGameEnd(bool isWon)
        {
            this.IsGameEnded = true;
            GameEnded?.Invoke(this, new GameEndedEventArgs(isWon));            
        }

        public class GameEndedEventArgs : EventArgs
        {
            public GameEndedEventArgs(bool isWon)
            {
                IsWon = isWon;
            }

            public bool IsWon { get; private set; }
        }
    }
}
