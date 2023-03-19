using System.Net.WebSockets;
using System.Text.Json;
using Websocket.Client;

namespace LedMatrixPainter.WebApp.WebSockets
{
    public class WebSocketClientService
    {
        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);

        public bool ConnectionIsAlive
        {
            get
            {
                return Client != null && Client.IsStarted;
            }
        }

        private IWebsocketClient Client
        {
            get; set;
        }

        public void InitializeClient()
        {
            var url = new Uri("ws://192.168.197.188:81");

            using (Client = new WebsocketClient(url, WebsocketClientFactory()))
            {
                Client.ReconnectTimeout = TimeSpan.FromMinutes(5);
                Client.ErrorReconnectTimeout = TimeSpan.FromSeconds(10);

                Client.Start().Wait();

                ExitEvent.WaitOne();
            }
        }

        public void SendCommand(WebSocketMessage command)
        {
            Client.Send(JsonSerializer.Serialize<WebSocketMessage>(command, GetOptions()));
        }

        private static Func<ClientWebSocket> WebsocketClientFactory()
        {
            var factory = new Func<ClientWebSocket>(() =>
            {
                var client = new ClientWebSocket
                {
                    Options =
                    {
                        KeepAliveInterval = TimeSpan.FromSeconds(5)
                    }
                };

                return client;
            });

            return factory;
        }

        private static JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}
