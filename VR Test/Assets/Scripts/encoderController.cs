using UnityEngine;

public class encoderController : MonoBehaviour
{

    float currPos;
    void Start()
    {
        currPos = transform.rotation.y;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R)) { transform.Rotate(0f, 0.25f, 0f); }
        if (Input.GetKey(KeyCode.L)) { transform.Rotate(0f, -0.25f, 0f); }
    }
}
