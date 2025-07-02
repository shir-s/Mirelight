using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightEnemyHealth : BossLevelBaseMono
    {
        public int maxHealth = 10;
        [SerializeField] private int currentHealth;


        private SpriteRenderer spriteRenderer;
        private static readonly Color HurtFlashColor = new Color(0.34f, 0f, 0.04f);
        private Color originalColor;

        private bool triggeredSpawn = false;

        private void Awake()
        {
            currentHealth = maxHealth;
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            StartCoroutine(FlashHurtColor(1f));

            // אם ירד מתחת לסף, נגרום לו לזמן את הצפרדעים
            if (currentHealth <= 6 && !triggeredSpawn)
            {
                triggeredSpawn = true;
                GetComponent<MirelightEnemy>()?.SpawnFrogs();
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private System.Collections.IEnumerator FlashHurtColor(float duration)
        {
            spriteRenderer.color = HurtFlashColor;
            yield return new WaitForSeconds(duration);
            spriteRenderer.color = originalColor;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}