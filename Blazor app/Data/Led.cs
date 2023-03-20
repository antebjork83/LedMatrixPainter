namespace LedMatrixPainter.WebApp.Data
{
    public class Led
    {
        private readonly string _initialPaintColor = "17de1b";
        private readonly string _baseColor = "000000";

        public Led(int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            HexColor = _baseColor;
            LedRgb = new Color(0, 0, 0);
        }

        public Led(int xPos, int yPos, string hexColor, Color ledRgb)
        {
            XPos = xPos;
            YPos = yPos;
            HexColor = hexColor;
            LedRgb = ledRgb;
        }

        public int XPos
        {
            get;
            private set;
        }

        public int YPos
        {
            get;
            private set;
        }

        public string HexColor
        {
            get;
            private set;
        }

        public Color LedRgb
        {
            get;
            private set;
        }

        public void ChangeColor(string hexColor)
        {
            if (HexColor == _initialPaintColor)
            {
                HexColor = _baseColor;
                LedRgb = Color.GenerateRgb(_baseColor);
            }
            else
            {
                HexColor = hexColor;
                LedRgb = Color.GenerateRgb(hexColor);
            }
        }
    }
}
