using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSaper.Model
{
    class Minefield : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<GameEndedEventArgs> GameEnded;
        public bool IsGameEnded;

        private readonly List<List<Tile>> tiles;

        private int bombsInMinefield = -1;
        private int tilesCovered;
        private int tilesFlagged = 0;
        
        public Minefield(List<List<Tile>> tiles)
        {
            this.tiles = tiles;
            var allTiles = this.tiles.SelectMany(t => t).ToArray();
            tilesCovered = allTiles.Length;

            foreach (var tile in allTiles)
            {
                tile.StateChanged += Tile_StateChanged;
            }
        }       
        
        public int TilesFlagged
        {
            get { return tilesFlagged; }
            private set
            {
                if (tilesFlagged != value)
                {
                    tilesFlagged = value;
                    OnPropertyChanged();
                }
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
                    bombsInMinefield = tiles.SelectMany(x => x.Select(y => y.HasBomb)).Count(z => z == true);
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

                if (e.CurrentState == Tile.TileState.Flagged)
                {
                    TilesFlagged++;
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
            IsGameEnded = true;
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

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
