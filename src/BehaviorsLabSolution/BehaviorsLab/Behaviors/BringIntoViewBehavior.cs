using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BehaviorsLab.Behaviors
{
    public static class BringIntoViewBehavior
    {
        public static bool GetIsBringIntoView(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsBringIntoViewProperty);
        }

        public static void SetIsBringIntoView(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBringIntoViewProperty, value);
        }

        public static readonly DependencyProperty IsBringIntoViewProperty =
          DependencyProperty.RegisterAttached("IsBringIntoView",
          typeof(bool), typeof(BringIntoViewBehavior),
          new PropertyMetadata(false, OnIsBringIntoViewChanged));

        private static void OnIsBringIntoViewChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // ignoring error checking
            var listBox = (ListBox)sender;
            var isDigitOnly = (bool)(e.NewValue);

            if (isDigitOnly)
                listBox.SelectionChanged += BringIntoView;
            else
                listBox.SelectionChanged -= BringIntoView;
        }

        private static void BringIntoView(object sender, RoutedEventArgs e)
        {
            var list = sender as ListBox;
            if (list != null)
                list.ScrollIntoView(list.SelectedItem);
        }       
    }
}
