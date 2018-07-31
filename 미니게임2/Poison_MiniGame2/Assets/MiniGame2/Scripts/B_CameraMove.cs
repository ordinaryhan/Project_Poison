using UnityEngine;

public class B_CameraMove : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.125f;
    float x, y;

    private void LateUpdate()
    {
        x = target.position.x;
        y = target.position.y;
        if (y <= 0)
        {
            y = 0;
        }
        else if (y >= 11.5f)
        {
            y = 11.5f;
        }
        if (x <= -1)
        {
            x = -1;
        }
        else if (x >= 1)
        {
            x = 1;
        }
        transform.position = new Vector3(x, y, -25);
    }
}
