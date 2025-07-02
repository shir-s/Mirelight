using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightEnemy : BossLevelBaseMono
    {
        public float moveSpeed = 2f;
        private Vector2 moveDirection;
        private float changeDirectionTime = 2f;
        private float timer;

        private void Start()
        {
            ChooseNewDirection();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            // החלפת כיוון רנדומלית אחרי זמן
            if (timer >= changeDirectionTime)
            {
                ChooseNewDirection();
                timer = 0f;
            }

            // תזוזה
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                // נהפוך כיוון בכיוון ה-X וה-Y
                moveDirection = new Vector2(-moveDirection.x, -moveDirection.y);
            }
        }

        private void ChooseNewDirection()
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            moveDirection = new Vector2(x, y).normalized;
        }
    }
}