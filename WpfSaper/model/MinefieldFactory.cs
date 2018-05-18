using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSaper.model
{
    class MinefieldFactory
    {
        public static Minefield CreateNew(int horizontalTilesCount, int verticalTilesCount, int bombsCount)
        {
            var tiles = new List<List<Tile>>();
            for (int i = 0; i < verticalTilesCount; i++)
            {
                var tilesRow = new List<Tile>();
                for (int j = 0; j < horizontalTilesCount; j++)
                {
                    var tile = new Tile();
                    tilesRow.Add(tile);
                }
                tiles.Add(tilesRow);
            }
            return new Minefield(tiles);
        }        
    }
}
