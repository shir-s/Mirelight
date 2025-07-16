using BossLevel.Gameplay.Controls;
using UnityEngine;

public class MirelightBossHealth : MonoBehaviour
{
    public int maxHealth = 10;
    [SerializeField] private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private static readonly Color HurtFlashColor = new Color(0.34f, 0f, 0.04f);
    private Color originalColor;
    private bool isDead = false;
    [SerializeField] private MirelightBossController bossController;

    private void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        StartCoroutine(FlashHurtColor(1f));

        bossController?.OnHealthChanged(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator FlashHurtColor(float duration)
    {
        spriteRenderer.color = HurtFlashColor;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }
    
}