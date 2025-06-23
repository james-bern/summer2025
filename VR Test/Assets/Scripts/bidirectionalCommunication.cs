using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net.Security;
using System.Runtime.InteropServices;
using System;
using System.Linq;

public class bidirectionalCommunication : MonoBehaviour
{
    SerialPort sp;
    int frame;
    bool motionToggle;

    void Start()
    {
        sp = new SerialPort("/dev/tty.usbserial-110", 115200);
        sp.Open();
        frame = 0;
    }

    void Update()
    {
        //READING: change size of object w/ knob
        if (sp.BytesToRead != 0)
        {
            string serialData = sp.ReadLine();
            if (serialData[0] == 's')
            {
                print(serialData);
                motionToggle = bool.Parse(serialData.Substring(1));
            }
            else
            {
                float currT = float.Parse(serialData);
                print(currT);
                if (transform.localScale.x <= 2.5f && transform.localScale.x >= 0.25f)
                {
                    float interScale = lerp(currT, 0.25f, 2.5f);
                    transform.localScale = new Vector3(interScale, interScale, interScale);
                }
            }
        }

        //WRITING: rotate object and send to arduino
        if (frame++ % 8 == 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (motionToggle) { move('y', 1); }
                else { rotate('x', 1); }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (motionToggle) { move('y', -1); }
                else { rotate('x', -1); }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (motionToggle) { move('x', 1); }
                else { rotate('y', -1); }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (motionToggle) { move('x', -1); }
                else { rotate('y', 1); }
            }
        }
    }

    void rotate(char axis, int direction)
    /*rotates object and sends information to arduino. axis is defined 
    by x, y, or z, direction by -1 or 1 (backward or forward)*/
    {
        float x = 0f, y = 0f, z = 0f;
        if (axis == 'x') { x = 1f * direction; }
        else if (axis == 'y') { y = 1f * direction; }
        else if (axis == 'z') { z = 1f * direction; }
        transform.Rotate(x, y, z);
        float unitRotation = 0f;

        if (axis == 'x') { unitRotation = inverseLerp(transform.rotation.x, -1f, 1f); }
        else if (axis == 'y') { unitRotation = inverseLerp(transform.rotation.y, -1f, 1f); }
        else if (axis == 'z') { unitRotation = inverseLerp(transform.rotation.z, -1f, 1f); }
        sp.WriteLine(string.Concat(axis, unitRotation));
    }

    void move(char axis, int direction)
    {
        float x = 0f, y = 0f, z = 0f;
        if (axis == 'x') { x = 0.1f * direction; }
        else if (axis == 'y') { y = 0.1f * direction; }
        else if (axis == 'z') { z = 0.1f * direction; }
        transform.Translate(x, y, z);
    }

    float inverseLerp(float currRotation, float lower, float upper)
    {
        return (currRotation - lower) / (upper - lower);
    }

    public float lerp(float t, float lower, float upper)
    {
        return t * (upper - lower) + lower;
    }

}
