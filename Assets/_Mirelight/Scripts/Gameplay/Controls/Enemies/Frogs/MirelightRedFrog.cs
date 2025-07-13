using System.Collections;
using UnityEngine;

public class MirelightRedFrog : MonoBehaviour
{
    public float detectionRadius = 3f;
    public LayerMask playerLayer;
    private Animator animator;
    private bool triggered = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!triggered)
        {
            Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
            if (player != null)
            {
                triggered = true;
                StartCoroutine(ExplodeSequence(player.GetComponent<MirelightPlayerHealth>()));
            }
        }
    }

    private IEnumerator ExplodeSequence(MirelightPlayerHealth playerHealth)
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("Explode");
        yield return new WaitForSeconds(0.1f);

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}