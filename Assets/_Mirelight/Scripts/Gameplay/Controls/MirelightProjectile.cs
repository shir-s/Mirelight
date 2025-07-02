using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightProjectile : BossLevelBaseMono
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float maxDistance = 30f;

        private Vector2 moveDirection;
        private Vector2 startPosition;

        public void Initialize(Vector2 dir, float spreadDegrees)
        {
            dir = Quaternion.Euler(0, 0, Random.Range(-spreadDegrees, spreadDegrees)) * dir;
            moveDirection = dir.normalized;
            startPosition = transform.position;
        }

        private void Update()
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);

            // Destroy by distance
            if (Vector2.Distance(transform.position, startPosition) > maxDistance)
            {
                Destroy(gameObject);
            }

            // Or destroy by leaving screen:
            // Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            // if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
            // {
            //     Destroy(gameObject);
            // }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemyHealth = other.GetComponent<MirelightEnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
                Destroy(gameObject);
            }
        }

    }
}