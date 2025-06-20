using UnityEngine;
using System.Collections;
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
        sp = new SerialPort("/dev/tty.usbserial-1120", 115200);
        sp.Open();

        frame = 0;
    }



    void Update()
    {
        if ((frame++ % 8 == 0) && Input.GetKey(KeyCode.F))
        {
            transform.Rotate(1f, 0f, 0f);
            float tmp = inverseLerp(transform.rotation.x);
            print(tmp);
            // sp.Write(tmp.ToString());
            byte[] buffer = BitConverter.GetBytes(tmp);
            print(buffer.Length);

            sp.Write(buffer, 0, buffer.Length);
            for (int i = 0;  i < 4; ++i) print(buffer[i]);
        }
    }

    float inverseLerp(float currRotation)
    {
        return (currRotation + 1f) / 2f;
    }

}
