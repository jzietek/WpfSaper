using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSaper.Model;

namespace WpfSaper.Services.Impl
{
    class MinefieldFactory : IMinefieldFactory
    {
        private readonly IBooleansGenerator booleansGenerator;

        public MinefieldFactory(IBooleansGenerator booleansGenerator)
        {
            this.booleansGenerator = booleansGenerator;
        }

        public Minefield CreateNew(GameConfig config)
        {
            return this.CreateNew(config.HorizontalTilesCount, config.VerticalTilesCount, config.BombsCount);
        }

        public Minefield CreateNew(int horizontalTilesCount, int verticalTilesCount, int bombsCount)
        {
            bool[] bombIndices = this.booleansGenerator.GenerateBooleans(horizontalTilesCount * verticalTilesCount, bombsCount);
            var minefield = AllocateTiles(horizontalTilesCount, verticalTilesCount, bombIndices);
            AssignNeighbours(minefield);
            return minefield;
        }        

        private Minefield AllocateTiles(int horizontalTilesCount, int verticalTilesCount, bool[] bomdIndices)
        {
            int counter = 0;
            var tiles = new List<List<Tile>>();
            for (int i = 0; i < verticalTilesCount; i++)
            {
                var tilesRow = new List<Tile>();
                for (int j = 0; j < horizontalTilesCount; j++)
                {
                    var tile = new Tile(counter, bomdIndices[counter]);
                    tilesRow.Add(tile);
                    counter++;
                }
                tiles.Add(tilesRow);
            }
            return new Minefield(tiles);
        }

        private void AssignNeighbours(Minefield minefield)
        {
            int rowsCount = minefield.Tiles.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                int columnsCount = minefield.Tiles[i].Count;
                for (int j = 0; j < columnsCount; j++)
                {
                    List<Tile> neighbours = new List<Tile>();

                    if (j > 0)
                        neighbours.Add(minefield.Tiles[i][j - 1]);
                    if (j < columnsCount - 1)
                        neighbours.Add(minefield.Tiles[i][j + 1]);
                    if (i > 0)
                        neighbours.Add(minefield.Tiles[i - 1][j]);
                    if (i < rowsCount - 1)
                        neighbours.Add(minefield.Tiles[i + 1][j]);

                    if (i > 0 && j > 0)
                        neighbours.Add(minefield.Tiles[i - 1][j - 1]);
                    if (i > 0 && j < columnsCount - 1)
                        neighbours.Add(minefield.Tiles[i - 1][j + 1]);
                    if (i < rowsCount - 1 && j < columnsCount - 1)
                        neighbours.Add(minefield.Tiles[i + 1][j + 1]);
                    if (i < rowsCount - 1 && j > 0)
                        neighbours.Add(minefield.Tiles[i + 1][j - 1]);

                    minefield.Tiles[i][j].SetNeighbours(neighbours);
                }
            }
        }        
    }
}
