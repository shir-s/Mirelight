using UnityEngine;

namespace _Mirelight.Scripts.Gameplay.Controls.Enemies.Frogs
{
    public class MirelightFrogHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 1;
        private int currentHealth;

        private Animator animator;
        private bool isDying = false;

        private void Awake()
        {
            currentHealth = maxHealth;
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(int damage)
        {
            if (isDying) return;

            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        // private void Die()
        // {
        //     isDying = true;
        //     animator.SetTrigger("Die");
        //     GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        //     GetComponent<Collider2D>().enabled = false;
        // }

        private void Die()
        {
            isDying = true;
            animator.SetTrigger("Die");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f; 
            GetComponent<Collider2D>().enabled = false;
        }
        
        public void OnDeathAnimationComplete()
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