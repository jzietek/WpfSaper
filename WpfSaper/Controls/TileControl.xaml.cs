using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfSaper.Commands;

namespace WpfSaper.Controls
{
    /// <summary>
    /// Interaction logic for TileControl.xaml
    /// </summary>
    public partial class TileControl : UserControl
    {
        public TileControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(TileControl), new FrameworkPropertyMetadata(string.Empty));

        public String Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TileStateProperty = DependencyProperty.Register("TileState", typeof(String), typeof(TileControl), new FrameworkPropertyMetadata("Covered"));

        public String TileState
        {
            get { return GetValue(TileStateProperty).ToString(); }
            set { SetValue(TileStateProperty, value); }
        }

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TileControl));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public static readonly RoutedEvent RightClickEvent = EventManager.RegisterRoutedEvent("RightClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TileControl));

        public event RoutedEventHandler RightClick
        {
            add { AddHandler(RightClickEvent, value); }
            remove { RemoveHandler(RightClickEvent, value); }
        }

        public static readonly RoutedEvent LeftAndRightClickEvent = EventManager.RegisterRoutedEvent("LeftAndRightClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TileControl));

        public event RoutedEventHandler LeftAndRightClick
        {
            add { AddHandler(LeftAndRightClickEvent, value); }
            remove { RemoveHandler(LeftAndRightClickEvent, value); }
        }

        private ICommand handleTileClickCommand;

        public ICommand HandleTileClickCommand
        {
            get
            {
                if (handleTileClickCommand == null)
                {
                    handleTileClickCommand = new RelayCommand(_ => { HandleTileClicked(); });
                }
                return handleTileClickCommand;
            }
        }

        private void HandleTileClicked()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && Mouse.RightButton == MouseButtonState.Pressed)
            {
                RaiseEvent(new RoutedEventArgs(LeftAndRightClickEvent));
            }
            else if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                RaiseEvent(new RoutedEventArgs(ClickEvent));
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                RaiseEvent(new RoutedEventArgs(RightClickEvent));
            }
        }        
    }
}
