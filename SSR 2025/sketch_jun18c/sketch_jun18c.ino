/*
 * This example shows how to read from a seesaw encoder module.
 * The available encoder API is:
 *      int32_t getEncoderPosition();
        int32_t getEncoderDelta();
        void enableEncoderInterrupt();
        void disableEncoderInterrupt();
        void setEncoderPosition(int32_t pos);
 */
#include "Adafruit_seesaw.h"
#include <seesaw_neopixel.h>
#include <SoftwareSerial.h>

#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#define SCREEN_WIDTH 128 // OLED display width, in pixels
#define SCREEN_HEIGHT 64 //OLED display height

#define SS_SWITCH        24
#define SS_NEOPIX        6

#define SEESAW_ADDR          0x36

#define OLED_RESET     -1 // Reset pin # (or -1 if sharing Arduino reset pin)
#define SCREEN_ADDRESS 0x3D ///< See datasheet for Address; 0x3D for 128x64, 0x3C for 128x32
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

Adafruit_seesaw ss;
seesaw_NeoPixel sspixel = seesaw_NeoPixel(1, SS_NEOPIX, NEO_GRB + NEO_KHZ800);

int32_t encoder_position;

// SoftwareSerial screenSerial(10, 11);

void setup() {
  Serial.begin(115200);
  // screenSerial.begin(9600);
  
  while (!Serial) delay(10);

  if(!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
    Serial.println(F("SSD1306 allocation failed"));
    for(;;); // Don't proceed, loop forever
  }
      display.display();



  //Serial.println("Displayed Display");
  // delay(2000);
  // display.clearDisplay();

  //display.drawPixel(64, 32, SSD1306_WHITE);
  // display.display();
  // Serial.println("Looking for seesaw!");
  
  if (! ss.begin(SEESAW_ADDR) || ! sspixel.begin(SEESAW_ADDR)) {
    Serial.println("Couldn't find seesaw on default address");
    while(1) delay(10);
  }
  // Serial.println("seesaw started");

  uint32_t version = ((ss.getVersion() >> 16) & 0xFFFF);
  if (version  != 4991){
    //Serial.print("Wrong firmware loaded? ");
    //Serial.println(version);
    while(1) delay(10);
  }
  // Serial.println("Found Product 4991");

  pinMode(LED_BUILTIN, OUTPUT);
  // use a pin for the built in encoder switch
  ss.pinMode(SS_SWITCH, INPUT_PULLUP);

  // get starting position
  encoder_position = ss.getEncoderPosition();

  // Serial.println("Turning on interrupts");
  delay(10);
  ss.setGPIOInterrupts((uint32_t)1 << SS_SWITCH, 1);
  ss.enableEncoderInterrupt();

  // delay(4000);
  delay(100);
  Serial.flush();
}

int xScale;
int frame; 
bool readAnything;
void loop() {


//  int32_t new_position = ss.getEncoderPosition();
//  // did we move arounde?
//  if (new_position > 50) { ss.setEncoderPosition(50); }
//  if (new_position < -50) { ss.setEncoderPosition(-50); }
//  if (encoder_position != new_position) {
//    Serial.println(clampedInverseLerp(-50.0, 50.0, new_position)); // FORNOW (clamp is unnecessary)
//    encoder_position = new_position;      // and save for next round
//  }

//  if (Serial.available() > 0) { 
//    float unitScale = Serial.parseFloat();
//    int xScale = round(unitScale*128);
//    display.clearDisplay();
//    display.drawPixel(xScale, 32, SSD1306_WHITE);
//    display.display();
//    delay(500);
//  }

  if (Serial.available() >= 4) {
    float unitScale;    
    Serial.readBytes((byte *) &unitScale, 4);
    xScale = round(unitScale*128);
    readAnything = true;
  }

  if (readAnything && (frame++ % 30 == 0)) {
    display.clearDisplay();
    display.drawPixel(xScale, 32, SSD1306_WHITE);
    display.display();
  }
  
  // delay(10);
}


float clampedInverseLerp(float l, float u, int32_t pos) {
  float t = ((float)pos-l) / (u-l);
  return min(1.0, max(0.0, t));
}
