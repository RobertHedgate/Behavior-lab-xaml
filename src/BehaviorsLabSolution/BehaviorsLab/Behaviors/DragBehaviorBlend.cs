using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Interactivity;

namespace BehaviorsLab.Behaviors
{
    class DragBehaviorBlend : Behavior<UIElement>
    {        
        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;
            AssociatedObject.RenderTransform = _transform;

            AssociatedObject.MouseLeftButtonDown += (sender, e) =>
            {
                _mouseStartPosition = e.GetPosition(parent);
                AssociatedObject.CaptureMouse();
            };

            AssociatedObject.MouseLeftButtonUp += (sender, e) =>
            {
                AssociatedObject.ReleaseMouseCapture();
                _elementStartPosition.X = _transform.X;
                _elementStartPosition.Y = _transform.Y;

            };

            AssociatedObject.MouseMove += (sender, e) =>
            {
                var mousePos = e.GetPosition(parent);
                var diff = (mousePos - _mouseStartPosition);
                if (!AssociatedObject.IsMouseCaptured) return;
                _transform.X = _elementStartPosition.X + diff.X;
                _transform.Y = _elementStartPosition.Y + diff.Y;
            };
        }

        private readonly TranslateTransform _transform = new TranslateTransform();
        private Point _elementStartPosition;
        private Point _mouseStartPosition;
    }
}
