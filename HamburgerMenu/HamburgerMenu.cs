using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace HamburgerMenu
{
    public class HamburgerMenu : ContentControl
    {
        public new ObservableCollection<HamburgerMenuItem> Content
        {
            get { return (ObservableCollection<HamburgerMenuItem>)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(ObservableCollection<HamburgerMenuItem>), typeof(HamburgerMenu),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        private static Task _openMenuTask;
        private static CancellationTokenSource _cancellationTokenSource;

        static HamburgerMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenu), new FrameworkPropertyMetadata(typeof(HamburgerMenu)));            
        }

        public override void BeginInit()
        {
            Content = new ObservableCollection<HamburgerMenuItem>();
            MouseEnter += HamburgerMenu_MouseEnter;
            MouseLeave += HamburgerMenu_MouseLeave;
            base.BeginInit();
        }
        private void HamburgerMenu_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!AutomaticOpenClose) return;
            IsOpen = false;
        }
        private void HamburgerMenu_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!AutomaticOpenClose) return;
            IsOpen = true;
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(HamburgerMenu),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, IsOpenPropertyChangedCallback));

        private static void IsOpenPropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var menu = (HamburgerMenu)depObj;
            if (menu == null || e.OldValue == e.NewValue)
                return;

            if ((bool)e.NewValue)
            {
                if (_openMenuTask != null)
                    _cancellationTokenSource.Cancel();

                _cancellationTokenSource = new CancellationTokenSource();

                _openMenuTask = Task.Delay(TimeSpan.FromSeconds(menu.SlideOpenDelay), _cancellationTokenSource.Token);

                _openMenuTask.ContinueWith(task =>
                {
                    var openMenuAnimation = new DoubleAnimation(menu.CloseWidth, menu.OpenWidth,
                        new Duration(TimeSpan.FromSeconds(menu.AnimationDuration)),
                        FillBehavior.HoldEnd);

                    menu.BeginAnimation(WidthProperty, openMenuAnimation);
                }, _cancellationTokenSource.Token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                if (_openMenuTask != null && !_openMenuTask.IsCompleted)
                    _cancellationTokenSource.Cancel();

                var closeMenuAnimation = new DoubleAnimation(menu.ActualWidth, menu.CloseWidth,
                                            new Duration(TimeSpan.FromSeconds(menu.AnimationDuration)),
                                            FillBehavior.HoldEnd);

                menu.BeginAnimation(WidthProperty, closeMenuAnimation);
            }
        }

        public Brush MenuIconColor
        {
            get { return (Brush)GetValue(MenuIconColorProperty); }
            set { SetValue(MenuIconColorProperty, value); }
        }

        public static readonly DependencyProperty MenuIconColorProperty =
            DependencyProperty.Register("MenuIconColor", typeof(Brush), typeof(HamburgerMenu), new PropertyMetadata(Brushes.White));

        public Brush SelectionBackgroundColor
        {
            get { return (Brush)GetValue(SelectionBackgroundColorProperty); }
            set { SetValue(SelectionBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty SelectionBackgroundColorProperty =
            DependencyProperty.Register("SelectionBackgroundColor", typeof(Brush), typeof(HamburgerMenu), new PropertyMetadata(Brushes.Red));

        public Brush SelectionIndicatorColor
        {
            get { return (Brush)GetValue(SelectionIndicatorColorProperty); }
            set { SetValue(SelectionIndicatorColorProperty, value); }
        }

        public static readonly DependencyProperty SelectionIndicatorColorProperty =
            DependencyProperty.Register("SelectionIndicatorColor", typeof(Brush), typeof(HamburgerMenu), new PropertyMetadata(Brushes.Red));

        public Brush MenuItemForeground
        {
            get { return (Brush)GetValue(MenuItemForegroundProperty); }
            set { SetValue(MenuItemForegroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemForegroundProperty =
            DependencyProperty.Register("MenuItemForeground", typeof(Brush), typeof(HamburgerMenu), new PropertyMetadata(Brushes.Transparent));


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set
            {
                SetValue(SelectedIndexProperty, value);
                if (value < Content.Count)
                    Content[value].IsSelected = true;
            }
        }

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(HamburgerMenu), new PropertyMetadata(0));

        public static readonly DependencyProperty CloseWidthProperty = DependencyProperty.Register(
            "CloseWidth", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(45d));

        [TypeConverter(typeof(LengthConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public double CloseWidth
        {
            get { return (double)GetValue(CloseWidthProperty); }
            set { SetValue(CloseWidthProperty, value); }
        }

        public static readonly DependencyProperty SelectionIndicatorWidthProperty = DependencyProperty.Register(
            "SelectionIndicatorWidth", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(2d));

        [TypeConverter(typeof(LengthConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public double SelectionIndicatorWidth
        {
            get { return (double)GetValue(SelectionIndicatorWidthProperty); }
            set { SetValue(SelectionIndicatorWidthProperty, value); }
        }


        public static readonly DependencyProperty OpenWidthProperty = DependencyProperty.Register(
            "OpenWidth", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(200d));

        [TypeConverter(typeof(LengthConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public double OpenWidth
        {
            get { return (double)GetValue(OpenWidthProperty); }
            set { SetValue(OpenWidthProperty, value); }
        }

        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
            "AnimationDuration", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(0.1d));

        public double AnimationDuration
        {
            get { return (double)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        public static readonly DependencyProperty SlideOpenDelayProperty = DependencyProperty.Register(
            "SlideOpenDelay", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(0.7d));

        public double SlideOpenDelay
        {
            get { return (double)GetValue(SlideOpenDelayProperty); }
            set { SetValue(SlideOpenDelayProperty, value); }
        }

        public static readonly DependencyProperty IconsSizeProperty = DependencyProperty.Register(
            "IconsSize", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(28d));

        [TypeConverter(typeof(LengthConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public double IconsSize
        {
            get { return (double)GetValue(IconsSizeProperty); }
            set { SetValue(IconsSizeProperty, value); }
        }

        public static readonly DependencyProperty IconsMarginProperty = DependencyProperty.Register(
            "IconsMargin", typeof(Thickness), typeof(HamburgerMenu), new PropertyMetadata(new Thickness(6, 0, 0, 0)));

        public Thickness IconsMargin
        {
            get { return (Thickness)GetValue(IconsMarginProperty); }
            set { SetValue(IconsMarginProperty, value); }
        }

        public ImageSource MenuIcon
        {
            get { return (ImageSource)GetValue(MenuIconProperty); }
            set { SetValue(MenuIconProperty, value); }
        }

        public bool AutomaticOpenClose { get; set; }

        public static readonly DependencyProperty MenuIconProperty =
            DependencyProperty.Register("MenuIcon", typeof(ImageSource), typeof(HamburgerMenu), new PropertyMetadata(
                new BitmapImage(new Uri("pack://application:,,,/HamburgerMenu;component/Assets/Menu.png", UriKind.Absolute))));

        private ICommand _clickCommand;

        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() =>
                {
                    if (AutomaticOpenClose) return;
                    IsOpen = !IsOpen;
                }, 
                true));
            }
        }
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
