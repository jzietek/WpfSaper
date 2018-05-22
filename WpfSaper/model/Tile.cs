using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    OnStateChanged();
                    OnPropertyChanged("State");
                }
            }
        }

        public int Id { get; private set; }

        public bool HasBomb { get; private set; }

        public IEnumerable<Tile> Neighbours { get { return this.neighbours; }}
                
        public int BombsAround { get { return this.neighbours.Count(x => x.HasBomb); }}
        

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
                return;
            }
            if (State == TileState.Flagged)
            {
                State = TileState.Covered;
                return;
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
