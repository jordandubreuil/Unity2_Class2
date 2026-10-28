using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthbar;
    public float playerHealth;
    public float currentHealth;

    [Header("Ragdoll Stuff")]
    public GameObject ragdoll;
    public GameObject playerModel;
    public PlayerController playerControls;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = playerHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthbar.fillAmount = currentHealth / playerHealth;
            if (currentHealth <= 0)
            {
                Debug.Log("Player is dead");
                ActivateRagdoll();
                GameManager.Instance._gameOver = true;
                GameManager.Instance.RestartGame();
            }
        }
    }

    void ActivateRagdoll()
    {
        playerControls.enabled = false;
        playerModel.SetActive(false);
        ragdoll.SetActive(true);
        //Restart Game
    }
   
}
