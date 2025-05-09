using UnityEngine;

public class LockY : MonoBehaviour
{
    float fixedY;

    void Start()
    {
        fixedY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
    }
}