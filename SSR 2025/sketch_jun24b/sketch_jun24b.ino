#include "Adafruit_seesaw.h"
#include <seesaw_neopixel.h>

#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#include <Servo.h>

// Potentiometer Info
#define SS_SWITCH 24
#define SS_NEOPIX 6
#define SEESAW_ADDR 0x36

#define SERVO_PIN 9

Adafruit_seesaw ss;
seesaw_NeoPixel sspixel = seesaw_NeoPixel(1, SS_NEOPIX, NEO_GRB + NEO_KHZ800);

Servo myservo;

int32_t encoder_position;
int yRotationScale = 90;

void setup()
{
  Serial.begin(115200);
  myservo.attach(SERVO_PIN);


  while (!Serial)
    delay(10);

  /*
    if (!ss.begin(SEESAW_ADDR) || !sspixel.begin(SEESAW_ADDR))
    {
      Serial.println("Couldn't find seesaw on default address");
      while (1)
        delay(10);
    }


    uint32_t version = ((ss.getVersion() >> 16) & 0xFFFF);
    if (version != 4991)
    {
      // Serial.print("Wrong firmware loaded? ");
      // Serial.println(version);
      while (1)
        delay(10);
    }

    pinMode(LED_BUILTIN, OUTPUT);
    // use a pin for the built in encoder switch
    ss.pinMode(SS_SWITCH, INPUT_PULLUP);
    // get starting position
    encoder_position = ss.getEncoderPosition();

    // Serial.println("Turning on interrupts");
    delay(10);
    ss.setGPIOInterrupts((uint32_t)1 << SS_SWITCH, 1);
    ss.enableEncoderInterrupt();
  */

}
int frame;
bool readAnything;
float unityRotationY;

void loop()
{
  //int32_t new_position = ss.getEncoderPosition();
  readSerial();

  // update encoder position & LED after reading unity rotation
  if (readAnything && (frame % 30 == 0))
  {

    yRotationScale = round(unityRotationY * 180);
    myservo.write(yRotationScale);
    //myservo.write(sin(frame / 30.0) * 90 + 90);
  }


   frame++;
}


void readSerial()
{
  if (Serial.available())
  {
    String unityData = Serial.readStringUntil('\n');
    unityRotationY = unityData.toFloat();
    readAnything = true;
  }
}

float inverseLerp(float l, float u, int32_t pos)
{
  return ((float)pos - l) / (u - l);
}
