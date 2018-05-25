namespace WpfSaper
{
    public class GameConfig
    {
        public GameConfig()
        {
        }

        public int HorizontalTilesCount { get; set; }

        public int VerticalTilesCount { get; set; }

        public int BombsCount { get; set; }

        public void Setup(int horizontalTilesCount, int verticalTilesCount, int bombsCount)
        {
            HorizontalTilesCount = horizontalTilesCount;
            VerticalTilesCount = verticalTilesCount;
            BombsCount = bombsCount;
        }
    }
}
