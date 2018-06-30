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
            get { return isCustomSelected;}
            set
            {
                if (value) UnsetAll();

                Set(ref isCustomSelected, value);
                if (isCustomSelected)
                {
                    IsExpanded = true;
                }
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set { Set(ref isExpanded, value); }
        }

        public bool IsEasySelected
        {
            get { return isEasySelected; }
            set
            {
                if (value) UnsetAll();

                Set(ref isEasySelected, value);
                if (isEasySelected)
                    GameConfig.SetEasy();
            }
        }

        public bool IsMediumSelected
        {
            get { return isMediumSelected; }
            set
            {
                if (value) UnsetAll();

                Set(ref isMediumSelected, value);
                if (isMediumSelected)
                    GameConfig.SetMedium();
            }
        }

        private void UnsetAll()
        {
            IsEasySelected = false;
            IsMediumSelected = false;
            IsHardSelected = false;
            IsCustomSelected = false;
        }

        public bool IsHardSelected
        {
            get { return isHardSelected; }
            set
            {
                if (value) UnsetAll();

                Set(ref isHardSelected, value);
                if (isHardSelected)
                {
                    GameConfig.SetHard();                    
                }
            }
        }

        public GameConfig GameConfig
        {
            get { return gameConfig; }
            private set { Set(ref gameConfig, value); }
        }                
    }
}
