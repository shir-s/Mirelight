using UnityEngine;

namespace _Mirelight.Scripts.Gameplay.Controls.Enemies.Frogs
{
    public class MirelightFrogHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 1;
        private int currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Projectile"))
            {
                TakeDamage(1);
                Destroy(collision.gameObject);
            }
        }
    }
}
