using UnityEngine;

public class ball : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            if (transform.localScale.x <= 5f)
            {
                transform.localScale += new Vector3(0.04f, 0.04f, 0.04f);
            }
        }

        if (Input.GetKey(KeyCode.N))
        {
            if (transform.localScale.x > 0.25f)
            {
                transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            }
        }
    }
}
