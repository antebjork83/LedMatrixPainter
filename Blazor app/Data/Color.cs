using System.Drawing;

namespace LedMatrixPainter.WebApp.Data
{
    public class Color
    {
        public Color(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public int Red
        {
            get;
            private set;
        }

        public int Green
        {
            get;
            private set;
        }

        public int Blue
        {
            get;
            private set;
        }

        public static Color GenerateRgb(string hexColor)
        {
            System.Drawing.Color color = ColorTranslator.FromHtml($"#{hexColor}");
            int r = Convert.ToInt16(color.R);
            int g = Convert.ToInt16(color.G);
            int b = Convert.ToInt16(color.B);

            return new Color(r, g, b);
        }
    }
}
