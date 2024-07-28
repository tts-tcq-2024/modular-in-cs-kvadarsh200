using System.Drawing;

namespace TelCo.ColorCoder
{
    public class ColorPair
    {
        public Color majorColor;
        public Color minorColor;

        public override string ToString()
        {
            return string.Format("MajorColor:{0}, MinorColor:{1}", majorColor.Name, minorColor.Name);
        }
    }
}
