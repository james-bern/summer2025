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
<<<<<<< HEAD
int yRotationScale = 90;

void setup()
{
  Serial.begin(115200);
  myservo.attach(SERVO_PIN);

=======

//bounds for the encoder knob
float maxEncoderPos = 50;
float minEncoderPos = -50;
int yRotationScale = 90;

void setup()
{
  Serial.begin(115200);
  myservo.attach(SERVO_PIN);
>>>>>>> 97a9d4abc4438f878579be3da2cde547a4ab57d7

  while (!Serial)
    delay(10);

<<<<<<< HEAD
  /*
=======
>>>>>>> 97a9d4abc4438f878579be3da2cde547a4ab57d7
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
<<<<<<< HEAD
  */
=======
>>>>>>> 97a9d4abc4438f878579be3da2cde547a4ab57d7

}
int frame;
bool readAnything;
float unityRotationY;

void loop()
{
<<<<<<< HEAD
  //int32_t new_position = ss.getEncoderPosition();
  readSerial();

  // update encoder position & LED after reading unity rotation
  if (readAnything && (frame % 30 == 0))
  {

    yRotationScale = round(unityRotationY * 180);
    myservo.write(yRotationScale);
    //myservo.write(sin(frame / 30.0) * 90 + 90);
=======

  //READING FROM UNITY TO MOVE SERVO
  readSerial();
  // update encoder position & LED after reading unity rotation
  if (readAnything && (frame % 30 == 0))
  {
    yRotationScale = round(lerp(0, 180, unityRotationY));
    myservo.write(yRotationScale);
     
  }

  //READING FROM ENCODER, WRITING TO SERVO AND UNITY
  float new_position = ss.getEncoderPosition();

  if (new_position < minEncoderPos) {
    ss.setEncoderPosition(minEncoderPos);
    new_position = minEncoderPos;
  } else if (new_position > maxEncoderPos) {
    ss.setEncoderPosition(maxEncoderPos);
    new_position = maxEncoderPos;
>>>>>>> 97a9d4abc4438f878579be3da2cde547a4ab57d7
  }
  //when knob is turned
  if (new_position != encoder_position) {

<<<<<<< HEAD

   frame++;
}


=======
    //write to unity to rotate
    float encoderChange = new_position - encoder_position;
    Serial.println(encoderChange / (maxEncoderPos - minEncoderPos));
    
    float tValue = inverseLerp(minEncoderPos, maxEncoderPos, new_position);

    //write to servo
    myservo.write(lerp(0, 180, tValue));
    
    encoder_position = new_position;
  }
  
   frame++;
}

>>>>>>> 97a9d4abc4438f878579be3da2cde547a4ab57d7
void readSerial()
{
  if (Serial.available())
  {
    String unityData = Serial.readStringUntil('\n');
<<<<<<< HEAD
    unityRotationY = unityData.toFloat();
=======
    unityRotationY = unityData.toFloat(); //float between 0 and 1
>>>>>>> 97a9d4abc4438f878579be3da2cde547a4ab57d7
    readAnything = true;
  }
}

float inverseLerp(float l, float u, int32_t pos)
{
  return ((float)pos - l) / (u - l);
}

float lerp(float l, float u, float tValue) {
  return (tValue * (u-l)) + l;
}
