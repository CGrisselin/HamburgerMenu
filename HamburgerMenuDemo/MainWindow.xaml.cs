using System;
using System.Windows;
using System.Windows.Input;

namespace HamburgerMenuDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ICommand
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show("Selection Binding: " + parameter);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 800 && HamMenu.IsOpen)
                HamMenu.IsOpen = false;
            if (e.NewSize.Width > 800 && !HamMenu.IsOpen)
                HamMenu.IsOpen = true;
        }
    }
}
