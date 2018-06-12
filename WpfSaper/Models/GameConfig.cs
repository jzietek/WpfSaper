using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfSaper.Models
{
    public class GameConfig : INotifyPropertyChanged
    {
        public GameConfig()
        {
            SetMedium();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int horizontalTilesCount;
        private int verticalTilesCount;
        private int bombsCount;

        public int HorizontalTilesCount
        {
            get { return horizontalTilesCount; }
            set
            {
                if (horizontalTilesCount != value)
                {
                    horizontalTilesCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public int VerticalTilesCount {
            get
            {
                return verticalTilesCount;
            }
            set
            {
                if (verticalTilesCount != value)
                {
                    verticalTilesCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public int BombsCount {
            get
            {
                return bombsCount;
            }
            set
            {
                if (bombsCount != value)
                {
                    bombsCount = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public void SetCustom(int horizontalTilesCount, int verticalTilesCount, int bombsCount)
        {
            HorizontalTilesCount = horizontalTilesCount;
            VerticalTilesCount = verticalTilesCount;
            BombsCount = bombsCount;
        }

        public void SetEasy()
        {
            SetCustom(6, 6, 8);
        }

        public void SetMedium()
        {
            SetCustom(10, 10, 16);
        }

        public void SetHard()
        {
            SetCustom(16, 16, 60);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
