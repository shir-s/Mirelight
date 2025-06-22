using UnityEngine;

public class MirelightPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Attack Settings")]
    public KeyCode attackKey = KeyCode.Z;
    public KeyCode chargedAttackKey = KeyCode.X;
    // public float chargedAttackHoldTime = 1.2f;
    // public float chargedAttackHoldTime = 1.2f;

    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public KeyCode jumpKey = KeyCode.Space;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("References")]
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("States")]
    public bool isSleeping = false;
    public bool isHurt = false;
    public bool isDead = false;
    private bool isAttacking = false;

    private Vector2 moveInput;
    // private float chargedAttackTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isSleeping || isDead || isAttacking) return;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(moveInput.x));

        if (moveInput.x != 0)
            spriteRenderer.flipX = moveInput.x < 0;

        // Regular Attack
        if (Input.GetKeyDown(attackKey))
        {
            animator.SetTrigger("Attack");
            StartCoroutine(AttackLock(0.5f));
        }

        // Charged Attack
        if (Input.GetKeyDown(chargedAttackKey))
        {
            animator.SetTrigger("ChargedAttack");
            StartCoroutine(AttackLock(1.0f)); // Adjust duration if needed
        }

        
        
        // Jump
        if (Input.GetKeyDown(jumpKey) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        // Optional: update grounded state in animator
        animator.SetBool("IsGrounded", IsGrounded());
    }

    private void FixedUpdate()
    {
        if (isSleeping || isDead || isAttacking)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private System.Collections.IEnumerator AttackLock(float duration)
    {
        isAttacking = true;
        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }

    public void EnterSleep()
    {
        isSleeping = true;
        animator.SetBool("IsSleeping", true);
        rb.linearVelocity = Vector2.zero;
    }

    public void ExitSleep()
    {
        isSleeping = false;
        animator.SetBool("IsSleeping", false);
    }

    public void TakeDamage()
    {
        if (isDead) return;

        isHurt = true;
        animator.SetTrigger("Hurt");
        StartCoroutine(HurtRecovery());
    }

    private System.Collections.IEnumerator HurtRecovery()
    {
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");
        rb.linearVelocity = Vector2.zero;
        this.enabled = false;
    }
}
