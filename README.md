# LedMatrixPainter (esp32, Blazor web app)

A simple project that lets you paint on to a Led Matrix via a web gui using websockets.

Heavily based on https://github.com/witnessmenow/ESP8266-Led-Matrix-Web-Draw

![webapp](https://user-images.githubusercontent.com/53916475/226210430-d6956529-d5ce-4b80-81ca-00fdfa3b452d.png)
A simple Blazor web app

![led_matrix](https://user-images.githubusercontent.com/53916475/226210446-06747ced-1ae4-44cd-a4b6-991503c04f54.jpg)
64x64 Led Matrix panel

# Hardware
AZDelivery ESP32 NodeMCU

Waveshare RGB LED Matrix panel P3-64x64

# Get it working
1) In the Blazor app change the url "ws://192.168.197:80" to your esp 32 IP and port.

WebSocketaclientService.cs

var url = new Uri("ws://192.168.197.188:81");

2) In LedMatrix_websockets.ino change to your ssid and password.

char ssid[] = "[your ssid]";

char pass[] = "[your password]";
