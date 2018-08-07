using System.Windows.Media;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics.Contracts;

namespace CustomIcon
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DrawingDictionary
    {
        private static DrawingDictionary Instance { get; } = new DrawingDictionary();

        private DrawingDictionary()
        {
            InitializeComponent();
            var m = this[IconKind.Save];
        }

        private static Dictionary<IconKind, Brush> Brushes { get; }=new Dictionary<IconKind, Brush>();

        internal static Brush GetBrush(IconKind iconKind)
        {
            if (Brushes.ContainsKey(iconKind))
                return Brushes[iconKind];
            Drawing drawing = null;
            if (Instance.Contains(iconKind))
            {
                drawing = Instance[iconKind] as Drawing;
            }
            else
            {
                drawing = GetDrawing(iconKind);
            }
            var brush = new DrawingBrush(drawing);
            brush.Freeze();
            Brushes.Add(iconKind, brush);
            return brush;
        }

        private static Drawing GetDrawing(IconKind iconKind)
        {
            switch (iconKind)
            {
                case IconKind.SaveDocument:
                    return Combine(IconKind.Document, IconKind.Save);
            }
            return null;
        }

        private static Drawing Combine(IconKind middle, IconKind leftTop = IconKind.Default, IconKind rightTop = IconKind.Default, IconKind rightBottom = IconKind.Default, IconKind leftBottom = IconKind.Default)
        {
            double middleHeight = 1;
            double middleWidth = 1;
            Point middleCenter = new Point(8, 8);
            if ((leftTop | rightTop) != IconKind.Default)
            {
                middleHeight -= 0.1;
                middleCenter.Y += 8;
            }
            if((leftBottom | rightBottom) != IconKind.Default)
            {
                middleHeight -= 0.1;
                middleCenter.Y -= 8;
            }
            if ((rightTop | rightBottom) != IconKind.Default)
            {
                middleWidth -= 0.1;
                middleCenter.X -= 8;
            }
            if ((leftTop | leftBottom) != IconKind.Default)
            {
                middleWidth -= 0.1;
                middleCenter.X += 8;
            }
            Contract.Assert(middleWidth != 1 || middleHeight != 1);
            DrawingGroup drawingGroup = new DrawingGroup();
            var baseDrawing = (Instance[middle] as DrawingGroup).Clone();
            baseDrawing.Transform = new ScaleTransform(middleWidth, middleHeight, middleCenter.X, middleCenter.Y);
            drawingGroup.Children.Add(baseDrawing);
            DrawingGroup modifier = null;
            if (leftTop != IconKind.Default)
            {
                modifier = (Instance[leftTop] as DrawingGroup).Clone();
                modifier.Transform = new ScaleTransform(0.5, 0.5, 0, 0);
                drawingGroup.Children.Add(modifier);
            }
            if (rightTop != IconKind.Default)
            {
                modifier = (Instance[rightTop] as DrawingGroup).Clone();
                modifier.Transform = new ScaleTransform(0.5, 0.5, 16, 0);
                drawingGroup.Children.Add(modifier);
            }
            if (rightBottom != IconKind.Default)
            {
                modifier = (Instance[rightBottom] as DrawingGroup).Clone();
                modifier.Transform = new ScaleTransform(0.5, 0.5, 16, 16);
                drawingGroup.Children.Add(modifier);
            }
            if (leftBottom != IconKind.Default)
            {
                modifier = (Instance[leftBottom] as DrawingGroup).Clone();
                modifier.Transform = new ScaleTransform(0.5, 0.5, 0, 16);
                drawingGroup.Children.Add(modifier);
            }
            drawingGroup.Freeze();
            return drawingGroup;
        }
    }
}
