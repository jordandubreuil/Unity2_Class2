using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackTransform;
    public float attackRadius;
    public float attackDamage = 10;

    public void AttackPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackTransform.position, attackRadius);

        foreach(var player in hitColliders)
        {
            if (player.CompareTag("Player"))
            {
                Debug.Log("Hitting the Player");
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackTransform.position, attackRadius);
    }
}
