using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WpfSaper.Models
{
    class Tile : INotifyPropertyChanged
    {
        public Tile(int id, bool hasBomb)
        {
            Id = id;
            HasBomb = hasBomb;
        }

        private readonly List<Tile> _neighbours = new List<Tile>();

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
                    var oldState = state;
                    state = value;
                    OnPropertyChanged("State");
                    OnStateChanged(oldState, state);                    
                }
            }
        }

        public int Id { get; private set; }

        public bool HasBomb { get; private set; }

        public IEnumerable<Tile> Neighbours { get { return _neighbours; }}
                
        public int BombsAround
        {
            get
            {
                if (bombsAround == -1)
                {
                    bombsAround = _neighbours.Count(x => x.HasBomb);
                }
                return bombsAround;
            }
        }
        

        public void SetNeighbours(IEnumerable<Tile> neighbours)
        {
            _neighbours.Clear();
            _neighbours.AddRange(neighbours);
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

        private void OnStateChanged(TileState previousState, TileState currentState)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(previousState, currentState));
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
            public TileState PreviousState { get; private set; }

            public StateChangedEventArgs(TileState previousState, TileState currentState)
            {
                PreviousState = previousState;
                CurrentState = currentState;
            }
        }
    }
}
