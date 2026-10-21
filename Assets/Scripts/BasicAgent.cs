using UnityEngine;
using UnityEngine.AI;

public class BasicAgent : MonoBehaviour
{
    public Transform playerTarget;
    public NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTarget.position);
    }
}
