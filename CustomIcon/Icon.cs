using System.Windows;
using System.Windows.Media;

namespace CustomIcon
{
    public sealed class Icon : FrameworkElement
    {
        public Icon()
        {
            _drawingVisual = new DrawingVisual();
            AddVisualChild(_drawingVisual);
        }

        #region Kind

        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(
            "Kind",
            typeof(IconKind),
            typeof(Icon),
            new PropertyMetadata(IconKind.Default,
                KindPropertyChangedCallback));

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

        #endregion

        #region SizeDefinition

        public static readonly DependencyProperty SizeDefinitionProperty = DependencyProperty.Register(
                "SizeDefinition",
                typeof(int),
                typeof(Icon),
                new PropertyMetadata(16, SizeDefinitionPropertyChangedCallback));

        public int SizeDefinition
        {
            get
            {
                return (int)GetValue(SizeDefinitionProperty);
            }

            set
            {
                SetValue(SizeDefinitionProperty, value);
            }
        }

        private static void SizeDefinitionPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                double scale = (int)e.NewValue / (double)(int)e.OldValue;
                (dependencyObject as Icon)._drawingVisual.Transform = new ScaleTransform(scale, scale);
                    }
        }

        #endregion

        private readonly DrawingVisual _drawingVisual;

        protected override Visual GetVisualChild(int index)
        {
            return _drawingVisual;
        }

        protected override int VisualChildrenCount => 1;
    }
}
