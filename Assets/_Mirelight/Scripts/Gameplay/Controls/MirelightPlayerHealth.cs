using System.Collections;
using BossLevel.Gameplay.Controls;
using UnityEngine;

public class MirelightPlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    // private int currentHealth;
    public int currentHealth; 
    
    
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
    
    //function that wait for 2 second and the call playerlost
    private IEnumerator WaitAndCallPlayerLost()
    {
        yield return new WaitForSeconds(2f);
        Object.FindFirstObjectByType<MirelightGameManager>().PlayerLost();
    }
}