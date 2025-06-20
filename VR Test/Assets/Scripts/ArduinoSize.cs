using UnityEngine;
using System.Collections;
using System.IO.Ports;

// NOTE (SPOOKY): "When Domain Reloading is disabled, entering Play Mode is faster, because Unity does not reset the scripting state each time. However, it is then up to you to ensure your scripting state resets when you enter Play Mode. To do this, you need to add code that resets your scripting state when Play Mode starts."

public class ArduinoSize : MonoBehaviour
{

    SerialPort sp;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void TODO_Reset()
    {
    }

    void Start()
    {
        sp = new SerialPort("/dev/tty.usbserial-1120", 9600);
        sp.Open();

    }

    void Update()
    {
        if (sp.BytesToRead != 0)
        {
            float currT = float.Parse(sp.ReadLine());
            if (transform.localScale.x <= 5.0f & transform.localScale.x >= 0.25f)
            {
                float interScale = interpolate(currT, 5.0f, 0.25f);
                transform.localScale = new Vector3(interScale, interScale, interScale);
            }
        }

    }

    public float interpolate(float t, float upper, float lower)
    {
        return t * (upper - lower) + lower;
    }


}