using UnityEngine;
using System.IO.Ports;
using System.Collections; 
public class encoderController : MonoBehaviour
{
    SerialPort sp;
    int frame;

    //clamping bounds for rotation
    float minAngle = -45f;
    float maxAngle = 45f;
    Vector3 currentRotation = new Vector3(0f, 0f, 0f);

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
            if (Input.GetKey(KeyCode.R))
            {
                //print("R Pressed");
                yRotate(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.L))
            {
                //print("L Pressed"); 
                yRotate(0, -1, 0);
            }
        }
    }

    void yRotate(float xDegrees, float yDegrees, float zDegrees)
    /*rotates object by degree input and sends y rotation to arduino.*/
    {
        currentRotation += new Vector3(xDegrees, yDegrees, zDegrees);

        //currentRotation.x = Mathf.Clamp(currentRotation.x, minAngle, maxAngle);
        currentRotation.y = Mathf.Clamp(currentRotation.y, minAngle, maxAngle);
        //currentRotation.z = Mathf.Clamp(currentRotation.z, minAngle, maxAngle);

        transform.localEulerAngles = currentRotation;

        // ONLY SENDS Y ROTATION
        float unitRotation = inverseLerp(currentRotation.y, minAngle, maxAngle);
        sp.WriteLine(unitRotation.ToString());
    }

    float inverseLerp(float currRotation, float lower, float upper)
    {
        return (currRotation - lower) / (upper - lower);
    }
}
