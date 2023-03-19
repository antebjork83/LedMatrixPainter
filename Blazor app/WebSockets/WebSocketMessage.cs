namespace LedMatrixPainter.WebApp.WebSockets
{
    public class WebSocketMessage
    {
        public WebSocketMessage(bool clear)
        {
            Clear = clear;
        }

        public WebSocketMessage(int xpos, int ypos, int red, int green, int blue)
        {
            Xpos = xpos;
            Ypos = ypos;
            Red = red;
            Green = green;
            Blue = blue;
        }

        public bool Clear
        {
            get; set;
        }

        public int Xpos
        {
            get;
            private set;
        }

        public int Ypos
        {
            get;
            private set;
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
    }
}
