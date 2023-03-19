namespace LedMatrixPainter.WebApp.Data
{
    public class LedMatrix
    {
        private readonly int _matrixWidth;
        private readonly int _matrixHeight;

        public LedMatrix(int matrixWidth, int matrixHeight)
        {
            _matrixWidth = matrixWidth;
            _matrixHeight = matrixHeight;

            InitLeds();
        }

        public List<Led> Leds
        {
            get;
            private set;
        }

        private void InitLeds()
        {
            Leds = new List<Led>();

            for (int y = 0; y <= _matrixHeight-1; y++)
            {
                for (int x = 0; x <= _matrixWidth-1; x++)
                {
                    Leds.Add(new Led(x, y));
                }
            }
        }
    }
}
