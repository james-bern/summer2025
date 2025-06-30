using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class PrintCheck : MonoBehaviour
{
    SerialPort sp;
    int frame;
    // Start is called before the first frame update
    void Start()
    {
        sp = new SerialPort("COM10", 115200);
        sp.Open();
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // frame++;
        // if (frame % 60 == 0) { print("60th frame!"); }
        if (sp.BytesToRead != 0) 
        { 
            float curr = float.Parse(sp.ReadLine()); 
            print("" + curr);
        }
    }
}
