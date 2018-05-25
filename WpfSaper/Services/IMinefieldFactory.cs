using System;
using WpfSaper.Model;

namespace WpfSaper.Services
{
    interface IMinefieldFactory
    {
        Minefield CreateNew(int horizontalTilesCount, int verticalTilesCount, int bombsCount);

        Minefield CreateNew(GameConfig config);
    }
}
