using LedMatrixPainter.WebApp.Data;
using LedMatrixPainter.WebApp.WebSockets;
using Microsoft.AspNetCore.Components;

namespace LedMatrixPainter.WebApp.Components
{
    public partial class LedMatrixPainterComponent
    {
        private LedMatrix _ledMatrix;
        private string _currentColor;
        private bool _editMode;

        [Parameter]
        public int MatrixWidth
        {
            get; set;
        }

        [Parameter]
        public int MatrixHeight
        {
            get; set;
        }

        [Inject]
        private WebSocketClientService WebSocketClientService
        {
            get; set;
        }

        protected override void OnInitialized()
        {
            Init();
        }

        private void Init()
        {
            _currentColor = "17de1b";
            _ledMatrix = new LedMatrix(MatrixWidth, MatrixHeight);
        }

        private void UpdateLed(Led led)
        {
            led.ChangeColor(_currentColor);

            WebSocketClientService.SendCommand(new WebSocketMessage(led.XPos, led.YPos, led.LedRgb.Red, led.LedRgb.Green, led.LedRgb.Blue));
        }

        private void Clear()
        {
            Init();

            WebSocketClientService.SendCommand(new WebSocketMessage(true));
        }

        private void ChangeColor(ChangeEventArgs args)
        {
            _currentColor = args.Value.ToString().TrimStart('#');
        }

        private void Paint(Led led)
        {
            if (_editMode)
            {
                UpdateLed(led);
            }
        }

        private void SetPaintMode()
        {
            if (_editMode)
            {
                _editMode = false;
            }
            else
            {
                _editMode = true;
            }
        }
    }
}
