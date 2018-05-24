using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WpfSaper.Model
{
    class Tile : INotifyPropertyChanged
    {
        public Tile(int id, bool hasBomb)
        {
            Id = id;
            HasBomb = hasBomb;
        }

        private readonly List<Tile> neighbours = new List<Tile>();

        private TileState state = TileState.Covered;

        private int bombsAround = -1;

        public event EventHandler<StateChangedEventArgs> StateChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public TileState State
        {
            get
            {
                return state;
            }
            private set
            {
                if (state != value)
                {
                    state = value;
                    OnPropertyChanged("State");
                    OnStateChanged();                    
                }
            }
        }

        public int Id { get; private set; }

        public bool HasBomb { get; private set; }

        public IEnumerable<Tile> Neighbours { get { return this.neighbours; }}
                
        public int BombsAround
        {
            get
            {
                if (bombsAround == -1)
                {
                    bombsAround = this.neighbours.Count(x => x.HasBomb);
                }
                return bombsAround;
            }
        }
        

        public void SetNeighbours(IEnumerable<Tile> neighbours)
        {
            this.neighbours.Clear();
            this.neighbours.AddRange(neighbours);
        }

        public void ToggleFlag()
        {
            if (State == TileState.Covered)
            {
                State = TileState.Flagged;
            }
            else if (State == TileState.Flagged)
            {
                State = TileState.Covered;
            }
        }

        public void UncoverTile()
        {
            if (State == TileState.Uncovered || State == TileState.Exploded)
            {
                return;
            }

            if (HasBomb)
            {
                State = TileState.Exploded;
            }
            else
            {
                State = TileState.Uncovered;                
            }
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(this.State));
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public enum TileState
        {
            Covered = 0,
            Flagged = 1,
            Uncovered = 2,            
            Exploded = 4
        };

        public class StateChangedEventArgs : EventArgs
        {
            public TileState CurrentState { get; private set; }

            public StateChangedEventArgs(TileState currentState)
            {
                this.CurrentState = currentState;
            }
        }
    }
}
