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
        private ListBox _dragSource;
        private object _dragData;
        private Point _dragStart;

        public List<string> StringList { get; set; }

        public MainWindow()
        {
            StringList = new List<string> {"0", "1", "2", "3", "4", "5", "6"};

            InitializeComponent();
            DataContext = this;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            List.SelectedIndex = List.Items.Count - 1;
        }

        int IndexUnderDragCursor
        {
            get
            {
                var index = -1;
                for (var i = 0; i < List.Items.Count; ++i)
                {
                    var item = List.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                    if (item != null && item.IsMouseOver)
                    {
                        index = i;
                        break;
                    }
                }
                return index;
            }
        }

        private void List_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStart = e.GetPosition(null);
            _dragSource = sender as ListBox;
            if (_dragSource == null) return;
            var i = IndexUnderDragCursor;
            _dragData = i != -1 ? _dragSource.Items.GetItemAt(i) : null;
        }

        private void List_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragData == null) return;

            var currentPosition = e.GetPosition(null);
            var difference = _dragStart - currentPosition;

            if ((MouseButtonState.Pressed == e.LeftButton) &&
                ((Math.Abs(difference.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                 (Math.Abs(difference.Y) > SystemParameters.MinimumVerticalDragDistance)))
            {
                var data = new DataObject("Custom", _dragData);
                DragDrop.DoDragDrop(_dragSource, data, DragDropEffects.Copy);
        
                _dragData = null;
            }
        }

        private void List2_OnDrop(object sender, DragEventArgs e)
        {
            var dropTarget = sender as ListBox;
            if ((dropTarget != null) && (e.Data.GetDataPresent("Custom")))
            {
                dropTarget.Items.Add(e.Data.GetData("Custom"));
            }
        }

        private void List_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragData = null;
        }
    }
}
