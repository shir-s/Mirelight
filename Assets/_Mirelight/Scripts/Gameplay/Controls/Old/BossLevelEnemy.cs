using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class BossLevelEnemy: BossLevelBaseMono 
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

            // החלפת כיוון רנדומלי לפי זמן
            if (timer >= changeDirectionTime)
            {
                ChooseNewDirection();
                timer = 0f;
            }

            // תנועה
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // בדיקת גבולות מסך
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

            // אם פוגע בגבול – הופך כיוון
            if (viewPos.x <= 0f || viewPos.x >= 1f)
                moveDirection.x *= -1;
            if (viewPos.y <= 0f || viewPos.y >= 1f)
                moveDirection.y *= -1;

            // שמירה על גבולות המסך
            viewPos.x = Mathf.Clamp01(viewPos.x);
            viewPos.y = Mathf.Clamp01(viewPos.y);
            transform.position = Camera.main.ViewportToWorldPoint(viewPos);
        }

        private void ChooseNewDirection()
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            moveDirection = new Vector2(x, y).normalized;
        }
    }
}
