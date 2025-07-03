using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

// NOTE (SPOOKY): "When Domain Reloading is disabled, entering Play Mode is faster, because Unity does not reset the scripting state each time. However, it is then up to you to ensure your scripting state resets when you enter Play Mode. To do this, you need to add code that resets your scripting state when Play Mode starts."

public class ArduinoSize : MonoBehaviour
{

    SerialPort sp;
    Vector3 currentSize;

    void Start()
    {
        sp = new SerialPort("COM10", 115200);
        sp.Open(); 
        currentSize = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void Update()
    {
        if (sp.BytesToRead != 0)
        {
            float currT = float.Parse(sp.ReadLine());
            Debug.Log("" + currT);
            if (transform.localScale.x <= 0.25f && transform.localScale.x >= 0.05f)
            {
                float interScale = interpolate(currT, 0.25f, 0.05f);
                currentSize = new Vector3(interScale, interScale, interScale);
            }
        }

    }

    void LateUpdate() { transform.localScale = currentSize; }

    public float interpolate(float t, float upper, float lower)
    {
        return t * (upper - lower) + lower;
    }


}