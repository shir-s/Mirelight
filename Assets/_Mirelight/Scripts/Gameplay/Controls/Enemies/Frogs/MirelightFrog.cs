using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightFrog : BossLevelBaseMono
    {
        // public float jumpForce = 5f;
        [SerializeField] private float jumpForce = 5f;
        private Rigidbody2D rb;
        private Transform player;
        private bool grounded;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            if (grounded && player != null)
            {
                Vector2 dir = (player.position - transform.position).normalized;
                rb.AddForce(new Vector2(dir.x, 1) * jumpForce, ForceMode2D.Impulse);
                grounded = false;
            }
            
        }
        
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                grounded = true;
            }
            else if (collision.collider.CompareTag("Player"))
            {
                collision.collider.GetComponent<MirelightPlayerController>()?.TakeDamage();
                Destroy(gameObject);
            }
            else if (collision.collider.CompareTag("Border"))
            {
                Destroy(gameObject);
            }
        }


        public void TakeDamage(int damage)
        {
            Destroy(gameObject); // תמיד מת במכה אחת
        }
    }

    internal class MirelightPlayerHealth
    {
    }
}