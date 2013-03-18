using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BehaviorsLab.Behaviors
{
    public class DragBehavior
    {
        public readonly TranslateTransform Transform = new TranslateTransform();
        private Point _elementStartPosition2;
        private Point _mouseStartPosition2;
        private static DragBehavior _instance = new DragBehavior();
        public static DragBehavior Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        public static bool GetDrag(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragProperty);
        }

        public static void SetDrag(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragProperty, value);
        }

        public static readonly DependencyProperty IsDragProperty =
          DependencyProperty.RegisterAttached("Drag",
          typeof(bool), typeof(DragBehavior),
          new PropertyMetadata(false, OnDragChanged));

        private static void OnDragChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // ignoring error checking
            var element = (UIElement)sender;
            var isDrag = (bool)(e.NewValue);

            Instance = new DragBehavior();
            ((UIElement)sender).RenderTransform = Instance.Transform;

            if (isDrag)
            {
                element.MouseLeftButtonDown += Instance.ElementOnMouseLeftButtonDown;
                element.MouseLeftButtonUp += Instance.ElementOnMouseLeftButtonUp;
                element.MouseMove += Instance.ElementOnMouseMove;
            }
            else
            {
                element.MouseLeftButtonDown -= Instance.ElementOnMouseLeftButtonDown;
                element.MouseLeftButtonUp -= Instance.ElementOnMouseLeftButtonUp;
                element.MouseMove -= Instance.ElementOnMouseMove;
            }
        }
        
        private void ElementOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var parent = Application.Current.MainWindow;
            _mouseStartPosition2 = mouseButtonEventArgs.GetPosition(parent);
            ((UIElement)sender).CaptureMouse();
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ((UIElement)sender).ReleaseMouseCapture();
            _elementStartPosition2.X = Transform.X;
            _elementStartPosition2.Y = Transform.Y;
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var parent = Application.Current.MainWindow;
            var mousePos = mouseEventArgs.GetPosition(parent);
            var diff = (mousePos - _mouseStartPosition2);
            if (!((UIElement)sender).IsMouseCaptured) return;
            Transform.X = _elementStartPosition2.X + diff.X;
            Transform.Y = _elementStartPosition2.Y + diff.Y;
        }        
    }
}
