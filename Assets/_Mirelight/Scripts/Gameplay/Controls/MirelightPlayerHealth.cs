using UnityEngine;

public class MirelightPlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    // private int currentHealth;
    [SerializeField] private int currentHealth; 
    
    
    private MirelightPlayerController playerController;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<MirelightPlayerController>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        playerController.PlayHurtAnimation();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        playerController.PlayDeathAnimation();
    }

}