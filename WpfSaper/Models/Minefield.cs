﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfSaper.Models
{
    class Minefield : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<GameEndedEventArgs> GameEnded;
        public bool IsGameEnded;
        private int bombsInMinefield = -1;
        private int tilesCovered;
        private int tilesFlagged = 0;

        public Minefield(List<List<Tile>> tiles)
        {
            Tiles = tiles;
            var allTiles = Tiles.SelectMany(t => t).ToArray();
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

        public List<List<Tile>> Tiles { get; }

        public int BombsInMinefiled
        {
            get
            {
                if (bombsInMinefield == -1)
                {
                    bombsInMinefield = Tiles.SelectMany(x => x.Select(y => y.HasBomb)).Count(z => z);
                }
                return bombsInMinefield;
            }
        }

        internal List<List<Tile>> Tiles1 => Tiles;

        private void Tile_StateChanged(object sender, Tile.StateChangedEventArgs e)
        {
            if (sender is Tile tile && e != null)
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

                //Propagate uncovering for covered ones
                if (tile.State == Tile.TileState.Uncovered && tile.BombsAround == 0)
                {
                    Debug.WriteLine($"Propagating uncovering from tile Id: {tile.Id}");

                    var coveredNeighbours = tile.Neighbours.Where(n => n.State == Tile.TileState.Covered);
                    foreach (var n in coveredNeighbours)
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
