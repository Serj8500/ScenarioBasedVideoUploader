using System.Drawing;
using System.Windows.Forms;

namespace ScenarioBasedVideoUploader
{
    public static class LayoutFix
    {
        private static Size _buttonSize = new Size( 117, 36 );

        public static int HeightGap1 => 12;

        public static int GetHeight( Control control, int aboveGap, int bellowGap )
        {
            return control.Size.Height + aboveGap + bellowGap;
        }
        
        public static Control PutBellowTarget( this Control source, Control target, int gapAbove )
        {
            //target - относительно чего позиционируется source
            
            source.Location = new Point( source.Location.X, target.Location.X + target.Size.Height + gapAbove );

            return source;
        }
        
        public static Control SetHeightRelativeToParent( this Control control, Control parent, int heightGap )
        {
            //parent - относительно чего устанавливается высота source
            control.Height = parent.Height - heightGap;

            return control;
        }
    }
}