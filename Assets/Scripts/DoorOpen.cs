using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Transform target;
    public float smoothTime;
    public float yVelocity;
    public bool doorOpen = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen)
        {
            float newY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
