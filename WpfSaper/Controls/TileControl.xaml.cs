using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void tileButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(RightClickEvent));
        }

        private void tileButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }        
    }
}
