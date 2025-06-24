using UnityEngine;
using System.IO.Ports;
using System.Collections; 
public class encoderController : MonoBehaviour
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
            if (Input.GetKey(KeyCode.R)) { print("R Pressed");  rotate('y', 1); }
            if (Input.GetKey(KeyCode.L)) { print("L Pressed");  rotate('y', -1); }
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
        sp.WriteLine(unitRotation.ToString());
    }

    float inverseLerp(float currRotation, float lower, float upper)
    {
        return (currRotation - lower) / (upper - lower);
    }
}
