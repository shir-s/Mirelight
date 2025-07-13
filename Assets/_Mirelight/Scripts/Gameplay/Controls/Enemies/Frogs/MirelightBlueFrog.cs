using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightBlueFrog : BossLevelBaseMono
    {
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float sideForce = 1.5f;

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private bool grounded;
        private int initialDirection;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            initialDirection = Random.value < 0.5f ? -1 : 1;

            spriteRenderer.flipX = initialDirection < 0;
        }

        private void Update()
        {
            if (grounded)
            {
                float xForce = initialDirection * sideForce;
                float yForce = jumpForce;

                rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
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
            Destroy(gameObject);
        }
    }
}