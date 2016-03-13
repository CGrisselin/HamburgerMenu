using System;
using System.Windows;
using System.Windows.Input;

namespace HamburgerMenuDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICommand
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
    }
}
