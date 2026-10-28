using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicAgent : MonoBehaviour
{
    public Transform playerTarget;
    public NavMeshAgent agent;
    public Animator anim;
    public float attackDuration;
    private bool isAttacking;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Check to see if we are in attack range
        if (!isAttacking && Vector3.Distance(transform.position, playerTarget.position) > 3)
        {
            agent.SetDestination(playerTarget.position);
        }
        if (!isAttacking && Vector3.Distance(transform.position,playerTarget.position) < 3 && GameManager.Instance._gameOver == false)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
        
        anim.SetFloat("Running", agent.velocity.magnitude);
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }
}
