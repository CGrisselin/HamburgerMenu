using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HamburgerMenu
{
    public class HamburgerMenuItem : ListBoxItem
    {
        static HamburgerMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenuItem), new FrameworkPropertyMetadata(typeof(HamburgerMenuItem)));
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(HamburgerMenuItem), new PropertyMetadata(String.Empty));


        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(HamburgerMenuItem), new PropertyMetadata(null));

        public Brush SelectionIndicatorColor
        {
            get { return (Brush)GetValue(SelectionIndicatorColorProperty); }
            set { SetValue(SelectionIndicatorColorProperty, value); }
        }

        public static readonly DependencyProperty SelectionIndicatorColorProperty =
            DependencyProperty.Register("SelectionIndicatorColor", typeof(Brush), typeof(HamburgerMenuItem), new PropertyMetadata(Brushes.Blue));

        public Brush SelectionBackgroundColor
        {
            get { return (Brush)GetValue(SelectionBackgroundColorProperty); }
            set { SetValue(SelectionBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty SelectionBackgroundColorProperty =
            DependencyProperty.Register("SelectionBackgroundColor", typeof(Brush), typeof(HamburgerMenuItem), new PropertyMetadata(Brushes.CornflowerBlue));


        public ICommand SelectionCommand
        {
            get { return (ICommand)GetValue(SelectionCommandProperty); }
            set { SetValue(SelectionCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectionCommandProperty =
            DependencyProperty.Register("SelectionCommand", typeof(ICommand), typeof(HamburgerMenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectionCommandParameterProperty = DependencyProperty.Register(
            "SelectionCommandParameter", typeof(object), typeof(HamburgerMenuItem), new PropertyMetadata(default(object)));

        public object SelectionCommandParameter
        {
            get { return GetValue(SelectionCommandParameterProperty); }
            set { SetValue(SelectionCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty IsSelectableProperty = DependencyProperty.Register(
            "IsSelectable", typeof(bool), typeof(HamburgerMenuItem), new PropertyMetadata(true));

        public bool IsSelectable
        {
            get { return (bool)GetValue(IsSelectableProperty); }
            set { SetValue(IsSelectableProperty, value); }
        }
    }
}
