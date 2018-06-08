using System;
using WpfSaper.Models;

namespace WpfSaper.Services
{
    interface IMinefieldFactory
    {
        Minefield CreateNew(int horizontalTilesCount, int verticalTilesCount, int bombsCount);

        Minefield CreateNew(GameConfig config);
    }
}
