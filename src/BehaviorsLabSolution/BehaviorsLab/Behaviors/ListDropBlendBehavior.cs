using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Interactivity;

namespace BehaviorsLab.Behaviors
{
    class ListDropBlendBehavior : Behavior<ListBox>
    {        
        protected override void OnAttached()
        {
            AssociatedObject.AllowDrop = true;
            AssociatedObject.Drop += AssociatedObjectOnDrop;
        }

        private void AssociatedObjectOnDrop(object sender, DragEventArgs dragEventArgs)
        {
            var dropTarget = sender as ListBox;
            if ((dropTarget != null) && (dragEventArgs.Data.GetDataPresent("Custom")))
            {
                dropTarget.Items.Add(dragEventArgs.Data.GetData("Custom"));
            }
        }
    }
}
