using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BehaviorsLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> StringList { get; set; }

        public MainWindow()
        {
            StringList = new List<string> { "0", "1", "2", "3", "4", "5", "6" };

            InitializeComponent();
            DataContext = this;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            List.SelectedIndex = List.Items.Count - 1;
        }
    }
}
