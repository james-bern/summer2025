using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

// NOTE (SPOOKY): "When Domain Reloading is disabled, entering Play Mode is faster, because Unity does not reset the scripting state each time. However, it is then up to you to ensure your scripting state resets when you enter Play Mode. To do this, you need to add code that resets your scripting state when Play Mode starts."

public class ServoRotation : MonoBehaviour
{

    SerialPort sp;
    int frame;
    float currentRotate;

    void Start()
    {
        sp = new SerialPort("COM10", 115200);
        sp.Open();
        frame = 0;
        currentRotate = transform.rotation.y; 
    }

    void Update()
    {
        float newRotation = transform.rotation.y;
        if (frame++ % 8 == 0)
        {
            if (newRotation != currentRotate)
            {
                float write = inverseLerp(newRotation, -1f, 1f);
                print("" + write);
                sp.WriteLine(string.Concat(write));
            }
        }

        currentRotate = newRotation;

    }



    public float inverseLerp(float currRotation, float lower, float upper)
    {
        return (currRotation - lower) / (upper - lower);
    }



}