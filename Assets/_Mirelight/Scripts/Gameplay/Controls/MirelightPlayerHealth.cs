using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightPlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 5;
        private int currentHealth;

        private MirelightPlayerController playerController;

        private void Awake()
        {
            currentHealth = maxHealth;
            playerController = GetComponent<MirelightPlayerController>();
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log("Player took damage. Current health: " + currentHealth);

            if (playerController != null)
            {
                playerController.TakeDamage();
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Player died!");
            playerController?.Die();
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            Debug.Log("Player healed. Current health: " + currentHealth);
        }

        public int GetHealth()
        {
            return currentHealth;
        }
    }
}