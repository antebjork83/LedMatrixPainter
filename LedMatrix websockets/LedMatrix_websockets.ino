#include <ESP32-HUB75-MatrixPanel-I2S-DMA.h>
#include <WiFi.h>
#include <WebSocketsServer.h>
#include <ArduinoJson.h>

/*--------------------- MATRIX GPIO CONFIG  -------------------------*/
#define R1_PIN 25
#define G1_PIN 26
#define B1_PIN 27
#define R2_PIN 14
#define G2_PIN 12
#define B2_PIN 13
#define A_PIN 23
#define B_PIN 22
#define C_PIN 5
#define D_PIN 17
#define E_PIN 32
#define LAT_PIN 4
#define OE_PIN 15
#define CLK_PIN 16

/*--------------------- MATRIX PANEL CONFIG -------------------------*/
#define PANEL_RES_X 64     // Number of pixels wide of each INDIVIDUAL panel module. 
#define PANEL_RES_Y 64     // Number of pixels tall of each INDIVIDUAL panel module.
#define PANEL_CHAIN 1      // Total number of panels chained one to another

// Set matrix size
#define MATRIX_HEIGHT 64
#define MATRIX_WIDTH 64

//Custom pin mapping for all pins
HUB75_I2S_CFG::i2s_pins _pins={25, 26, 27, 14, 12, 13, 23, 22, 5, 17, 32, 4, 15, 16};

HUB75_I2S_CFG mxconfig(
  MATRIX_HEIGHT,          // width
  MATRIX_WIDTH,           // height
  PANEL_CHAIN,            // chain length
  _pins,                  // pin mapping
  HUB75_I2S_CFG::FM6126A  // driver chip
);

MatrixPanel_I2S_DMA *dma_display = nullptr;

// Init wifi
char ssid[] = "[your ssid]";
char pass[] = "[your password]";

WiFiServer server(20032);

WebSocketsServer webSocket = WebSocketsServer(81);

StaticJsonDocument<200> doc;

void setup()
{
 /************** SERIAL **************/
  Serial.begin(115200);
  delay(250);

  Serial.println("Starting Display...");

  dma_display = new MatrixPanel_I2S_DMA(mxconfig);

  dma_display->begin();
  dma_display->setBrightness8(90); //0-255

  IPAddress ip = InitServer();

  dma_display->setTextColor(0x0400);
  dma_display->println(ip);
  delay(5000);
  dma_display->clearScreen(); 

  webSocket.begin();
  webSocket.onEvent(webSocketEvent);
}

void loop()
{
  webSocket.loop();
}

// Custom methods
void clearScreen()
{
  dma_display->clearScreen();
}

void setPixel(int16_t xPos, int16_t yPos, uint8_t red, uint8_t green, uint8_t blue)
{
  dma_display->fillRect(xPos, yPos, 1, 1, red, green, blue);
}

IPAddress InitServer()
{
  Serial.print("Attempting to connect to Network named: ");
  Serial.println(ssid); // print the network name (SSID);

  WiFi.begin(ssid, pass);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(1000);
  }
  Serial.println("");
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);

  server.begin(); // start the server

  return ip;
}

void webSocketEvent(uint8_t num, WStype_t type, uint8_t * payload, size_t length)
{
  int xPos;
  int yPos;
  uint8_t r;
  uint8_t g;
  uint8_t b;

  switch(type)
  {
    case WStype_DISCONNECTED:
      Serial.printf("[%u] Disconnected!\n", num);
      break;
    case WStype_CONNECTED:
      {
        IPAddress ip = webSocket.remoteIP(num);
        Serial.printf("[%u] Connected from %d.%d.%d.%d url: %s\n", num, ip[0], ip[1], ip[2], ip[3], payload);

        // send message to client
        webSocket.sendTXT(num, "Connected");
      }
      break;
    case WStype_TEXT:
        Serial.printf("[%u] get Text: %s\n", num, payload);

        // Deserialize the JSON document
        DeserializationError error = deserializeJson(doc, payload);

        // Test if parsing succeeds.
        if (error) {
          Serial.print(F("deserializeJson() failed: "));
          Serial.println(error.f_str());
          return;
        }

        int16_t xPos = doc["xpos"];
        int16_t yPos = doc["ypos"];
        uint8_t red = doc["red"];
        uint8_t green = doc["green"];
        uint8_t blue = doc["blue"];
        bool clear = doc["clear"];
        
        if (clear)
        {
          clearScreen();
        }
        else
        {
          setPixel(xPos, yPos, red, green, blue);
        }

      break;
  }
}