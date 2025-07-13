using _Mirelight.Scripts.Gameplay.Controls.Enemies.Frogs;
using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightGreenFrog : BossLevelBaseMono
    {
        [SerializeField] private float jumpForce = 3f;
        [SerializeField] private float sideForce = 2f;
        private SpriteRenderer spriteRenderer;
        private Transform player;
        private Rigidbody2D rb;
        private bool grounded = false;

        private MirelightFrogHealth frogHealth;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindWithTag("Player").transform;
            spriteRenderer = GetComponent<SpriteRenderer>();
            frogHealth = GetComponent<MirelightFrogHealth>();
        }

        private void Update()
        {
            if (grounded && player != null)
            {
                Vector2 dir = (player.position - transform.position).normalized;

                float xForce = dir.x * sideForce;
                float yForce = jumpForce;

                rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
                grounded = false;

                spriteRenderer.flipX = dir.x < 0;
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
                collision.collider.GetComponent<MirelightPlayerHealth>()?.TakeDamage(1);
                frogHealth?.TakeDamage(1); // הצפרדע מתה
            }
            else if (collision.collider.CompareTag("Border"))
            {
                frogHealth?.TakeDamage(1);
            }
        }
    }
}