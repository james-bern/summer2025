using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net.Security;
using System.Runtime.InteropServices;
using System;
using System.Linq;

public class ArduinoLED : MonoBehaviour
{
    SerialPort sp;
    int frame;

    void Start()
    {
        sp = new SerialPort("/dev/tty.usbserial-110", 115200);
        sp.Open();
        frame = 0;
    }

    void Update()
    {
        if (frame++ % 8 == 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                print("forwards");
                rotateForward();
            }
            if (Input.GetKey(KeyCode.S))
            {
                print("backwards");
                rotateBackward();
            }

            if (Input.GetKey(KeyCode.D))
            {
                print("clockwise");
                rotateYClockwise();
            }
            if (Input.GetKey(KeyCode.A))
            {
                print("counter");
                rotateYCounter();
            }
        }
    }

    void rotateForward()
    {
        transform.Rotate(1f, 0f, 0f);
        float tmp = inverseLerp(transform.rotation.x);
        sp.WriteLine("x" + tmp.ToString());
    }

    void rotateBackward()
    {
        transform.Rotate(-1f, 0f, 0f);
        float tmp = inverseLerp(transform.rotation.x);
        sp.WriteLine("x" + tmp.ToString());
    }

    void rotateYClockwise()
    {
        transform.Rotate(0f, 1f, 0f);
        float tmp = inverseLerp(transform.rotation.y);
        sp.WriteLine("y" + tmp.ToString());
    }

    void rotateYCounter()
    {
        transform.Rotate(0f, -1f, 0f);
        float tmp = inverseLerp(transform.rotation.y);
        sp.WriteLine("y" + tmp.ToString());
    }

    float inverseLerp(float currRotation)
    {
        return (currRotation + 1f) / 2f;
    }

}
