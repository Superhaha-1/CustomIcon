using System.Windows;
using System.Windows.Media;

namespace CustomIcon
{
    public sealed class Icon : FrameworkElement
    {
        static Icon()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(Icon), new FrameworkPropertyMetadata(typeof(Icon)));
        }

        public Icon()
        {
            _drawingVisual = new DrawingVisual();
            AddVisualChild(_drawingVisual);
        }

        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
                "Kind",
                typeof(IconKind),
                typeof(Icon),
                new PropertyMetadata(IconKind.Default, KindPropertyChangedCallback));

        public IconKind Kind
        {
            get
            {
                return (IconKind)GetValue(KindProperty);
            }

            set
            {
                SetValue(KindProperty, value);
            }
        }

        private static void KindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
                using (var drawing = (dependencyObject as Icon)._drawingVisual.RenderOpen())
                {
                    drawing.DrawRectangle(DrawingDictionary.GetBrush((IconKind)e.NewValue), null, new Rect(0, 0, 16, 16));
                }
        }

        private readonly DrawingVisual _drawingVisual;

        protected override Visual GetVisualChild(int index)
        {
            return _drawingVisual;
        }

        protected override int VisualChildrenCount => 1;
    }
}
