using System.Collections;
using System.Collections.Generic;
using BossLevel.Gameplay.Controls;
using UnityEngine;
using DG.Tweening;

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
    
    [SerializeField] private GameObject soulFairyPrefab;

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

        if (bossController != null)
            bossController.enabled = false;

        if (col != null)
            col.enabled = false;

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (soulFairyPrefab != null)
        {
            Vector3 fairyPosition = transform.position + new Vector3(0f, 0f, 1f); 
            GameObject soul = Instantiate(soulFairyPrefab, fairyPosition, Quaternion.identity);
            StartCoroutine(SoulRoutine(soul));
        }

        else
        {
            Destroy(gameObject);
            MirelightGameManager.Instance?.PlayerWon();
        }
    }
    
    IEnumerator SoulRoutine(GameObject soul)
    {
        // 1. Find the SoulPath inside the prefab
        Transform pathRoot = soul.transform.Find("SoulPath");

        if (pathRoot == null)
        {
            Debug.LogError("SoulPath not found inside soul prefab!");
            yield break;
        }

        // 2. Collect all child positions as path points
        List<Vector3> path = new List<Vector3>();
        foreach (Transform point in pathRoot)
        {
            path.Add(point.position);
        }

        // 3. Animate movement along path
        Tween moveTween = soul.transform
            .DOPath(path.ToArray(), 8f, PathType.CatmullRom)
            .SetEase(Ease.InOutSine)
            .OnWaypointChange(index =>
            {
                if (index == 3) // When reaching Point4 (index starts at 0)
                {
                    SpriteRenderer sr = soul.GetComponentInChildren<SpriteRenderer>();
                    if (sr != null) sr.flipX = true;
                }
            });

        // 4. Breathing scale animation
        Tween scaleTween = soul.transform
            .DOScale(1.1f, 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        // 5. Wait for path movement to finish
        yield return moveTween.WaitForCompletion();

        // 6. Cleanup
        scaleTween.Kill(); // Stop the infinite loop
        Destroy(soul);
        
        // 6.5 Wait before win
        yield return new WaitForSeconds(2f);
        
        // 7. WIN!
        FindObjectOfType<MirelightGameManager>().PlayerWon();
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