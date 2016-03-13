using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HamburgerMenu
{
    public class HamburgerMenu : ContentControl
    {
        private static Task _openMenuTask;
        private static CancellationTokenSource _cancellationTokenSource;

        public new List<Control> Content
        {
            get { return (List<Control>)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(List<Control>), typeof(HamburgerMenu),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        static HamburgerMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenu), new FrameworkPropertyMetadata(typeof(HamburgerMenu)));
        }

        public override void BeginInit()
        {
            Content = new List<Control>();
            base.BeginInit();
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

        public static readonly DependencyProperty CloseWidthProperty = DependencyProperty.Register(
            "CloseWidth", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(45d));

        [TypeConverter(typeof(LengthConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public double CloseWidth
        {
            get { return (double)GetValue(CloseWidthProperty); }
            set { SetValue(CloseWidthProperty, value); }
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

    }
}
