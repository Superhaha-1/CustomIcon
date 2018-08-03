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
            var brush = new DrawingBrush(Instance[iconKind] as Drawing);
            brush.Freeze();
            Brushes.Add(iconKind, brush);
            return brush;
        }
    }
}
