namespace LedMatrixPainter.WebApp.WebSockets
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketClientService _webSocketClient;

        public WebSocketMiddleware(RequestDelegate next,
            WebSocketClientService webSocketClient)
        {
            _next = next;
            _webSocketClient = webSocketClient;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!_webSocketClient.ConnectionIsAlive)
            {
                _webSocketClient.InitializeClient();
            }

            await _next(context);
        }
    }

    public static partial class MiddlewareExtensions
    {
        public static IApplicationBuilder UseWebsocketClient(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketMiddleware>();
        }
    }
}
