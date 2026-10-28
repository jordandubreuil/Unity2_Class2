using UnityEngine;

public class WaypointEnemy : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;
    Vector3 targetPosition;
    [SerializeField]
    [Range(0, 100)]
    float moveSpeed;
    int waypointIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            waypointIndex++;
            if(waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }

        targetPosition = waypoints[waypointIndex].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(10.0f);
        }
    }
}
