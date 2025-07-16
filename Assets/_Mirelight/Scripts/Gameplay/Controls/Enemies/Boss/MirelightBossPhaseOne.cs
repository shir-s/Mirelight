using UnityEngine;

namespace BossLevel.Gameplay.Controls.Boss
{
    public class MirelightBossPhaseOne : IMirelightBossPhase
    {
        private Transform bossTransform;
        private float moveSpeed;
        private float changeDirectionTime;

        private Vector2 moveDirection;
        private float timer;

        public MirelightBossPhaseOne(
            Transform bossTransform,
            float moveSpeed,
            float changeDirectionTime)
        {
            this.bossTransform = bossTransform;
            this.moveSpeed = moveSpeed;
            this.changeDirectionTime = changeDirectionTime;

            ChooseNewDirection();
        }

        public void Tick(float deltaTime)
        {
            timer += deltaTime;

            if (timer >= changeDirectionTime)
            {
                ChooseNewDirection();
                timer = 0f;
            }

            bossTransform.Translate(moveDirection * moveSpeed * deltaTime);
        }

        private void ChooseNewDirection()
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            moveDirection = new Vector2(x, y).normalized;
        }
    }
}