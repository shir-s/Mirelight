using _Mirelight.Scripts.Gameplay.Controls.Enemies.Frogs;
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
            
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var frog = other.GetComponent<MirelightFrogHealth>();
            if (frog != null)
            {
                frog.TakeDamage(1);
                return;
            }

            var boss = other.GetComponent<MirelightBossHealth>();
            if (boss != null && !boss.isDead)
            {
                boss.TakeDamage(1);
            }
        }


    }
}