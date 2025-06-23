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
            if (Input.GetKey(KeyCode.F))
            {
                print("forwards");
                forward();

            }
            else if (Input.GetKey(KeyCode.B))
            {
                print("backwards");
                backward();
            }
        }
    }

    void forward()
    {
        
        transform.Rotate(1f, 0f, 0f);
        float tmp = inverseLerp(transform.rotation.x);
        sp.WriteLine(tmp.ToString());
    }

    void backward()
    {
        transform.Rotate(-1f, 0f, 0f);
        float tmp = inverseLerp(transform.rotation.x);
        sp.WriteLine(tmp.ToString());
    }

    void OnMessageArrived(string msg)
    {
        return;

    }

    float inverseLerp(float currRotation)
    {
        return (currRotation+ 1f) / 2f ;
    }

}
