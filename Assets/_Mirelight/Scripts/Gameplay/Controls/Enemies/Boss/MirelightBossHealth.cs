using BossLevel.Gameplay.Controls;
using UnityEngine;

public class MirelightBossHealth : MonoBehaviour, IMirelightDamageable
{
    public int maxHealth = 10;
    [SerializeField] private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private static readonly Color HurtFlashColor = new Color(0.34f, 0f, 0.04f);
    private Color originalColor;
    public bool isDead = false;
    [SerializeField] private MirelightBossController bossController;
    private Collider2D col;

    private void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        col = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        StartCoroutine(FlashHurtColor(1f));

        bossController?.OnHealthChanged(currentHealth, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Boss died!");

        // מנטרל את ה-Controller
        if (bossController != null)
            bossController.enabled = false;

        // מנטרל את הקוליידר כדי שפרויקטילים לא ימשיכו לפגוע בו
        if (col != null)
            col.enabled = false;

        // שולח הודעה לנצחון
        MirelightGameManager.Instance?.PlayerWon();

        // הורס את עצמו
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator FlashHurtColor(float duration)
    {
        spriteRenderer.color = HurtFlashColor;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }

    public bool IsDead()
    {
        return isDead;
    }
}