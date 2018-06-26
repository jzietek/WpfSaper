using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfSaper.Models;

namespace WpfSaper.ViewModels
{
    public class GameConfigViewModel : ViewModelBase
    {
        public GameConfigViewModel()
        {
            GameConfig = new GameConfig();
        }

        private bool isEasySelected;
        private bool isMediumSelected = true;
        private bool isHardSelected;
        private bool isCustomSelected;
        private bool isExpanded;

        private GameConfig gameConfig;

        public bool IsCustomSelected
        {
            get
            {
                return isCustomSelected;
            }
            set
            {
                if (isCustomSelected != value)
                {
                    isCustomSelected = value;
                    if (isCustomSelected)
                    {
                        IsExpanded = true;
                    }
                    OnPropertyChanged();
                }
            }
        }

        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEasySelected
        {
            get
            {
                return isEasySelected;
            }
            set
            {
                if (isEasySelected != value)
                {
                    isEasySelected = value;
                    if (isEasySelected)
                    {
                        GameConfig.SetEasy();
                    }
                    OnPropertyChanged();
                }
            }
        }

        public bool IsMediumSelected
        {
            get
            {
                return isMediumSelected;
            }
            set
            {
                if (isMediumSelected != value)
                {
                    isMediumSelected = value;
                    if (isMediumSelected)
                    {
                        GameConfig.SetMedium();
                    }
                    OnPropertyChanged();
                }
            }
        }

        public bool IsHardSelected
        {
            get
            {
                return isHardSelected;
            }
            set
            {
                if (isHardSelected != value)
                {
                    isHardSelected = value;
                    if (isHardSelected)
                    {
                        System.Diagnostics.Debug.WriteLine("IsHard = true");
                        GameConfig.SetHard();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("IsHard = false");
                    }
                    OnPropertyChanged();
                }
            }
        }

        public GameConfig GameConfig
        {
            get
            {
                return gameConfig;
            }
            private set
            {
                if (gameConfig != value)
                {
                    gameConfig = value;
                    OnPropertyChanged();
                }
            }
        }                
    }
}
