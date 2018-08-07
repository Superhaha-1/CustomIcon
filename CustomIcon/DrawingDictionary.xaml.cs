using System.Windows.Media;
using System.Collections.Generic;

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
                    return PutInLeft(IconKind.Document, IconKind.Save);
            }
            return null;
        }


        public static Drawing PutInLeft(IconKind baseElement, IconKind modifier)
        {
            var baseDrawing = (Instance[baseElement] as DrawingGroup).Clone();
            var modifierDrawing = (Instance[modifier] as DrawingGroup).Clone();
            baseDrawing.Transform = new ScaleTransform(0.9, 0.9, 16, 16);
            modifierDrawing.Transform = new ScaleTransform(0.5, 0.5);
            return new DrawingGroup() { Children = { baseDrawing, modifierDrawing } };
        }
    }
}
