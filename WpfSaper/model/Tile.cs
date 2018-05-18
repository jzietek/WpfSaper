using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSaper.model
{
    class Tile
    {
        public enum TileState
        {
            Covered = 0,
            Uncovered = 1,
            Flagged = 2,
            Exploded = 4            
        };

        private readonly IList<Tile> neighbours = new List<Tile>();

        public TileState State { get; private set; }

        public bool HasBomb { get; set; }

        public IList<Tile> Neighbours { get { return this.neighbours; } }

        public int BombsAround
        {
            get
            {
                return this.neighbours.Count(x => x.HasBomb);
            }
        }
    }
}
