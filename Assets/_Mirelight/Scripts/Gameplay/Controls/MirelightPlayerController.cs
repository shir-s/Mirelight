using BossLevel.Gameplay.Controls;
using UnityEngine;

public class MirelightPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Attack Settings")]
    public KeyCode attackKey = KeyCode.Z;
    public KeyCode chargedAttackKey = KeyCode.X;

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
    private bool isAttacking = false;

    private Vector2 moveInput;
    private bool wasGrounded;

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    private float initialFirePointX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialFirePointX = _firePoint.localPosition.x;
    }

    private void Update()
    {
        if (isSleeping || isAttacking) return;

        HandleMovement();
        HandleAttack();
        HandleJump();
        HandleGroundState();

        _firePoint.localPosition = new Vector3(
            spriteRenderer.flipX ? -Mathf.Abs(initialFirePointX) : Mathf.Abs(initialFirePointX),
            _firePoint.localPosition.y);
    }

    private void HandleMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(moveInput.x));

        if (moveInput.x != 0)
            spriteRenderer.flipX = moveInput.x < 0;
    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(attackKey))
        {
            animator.SetTrigger("Attack");
            Shoot();
            StartCoroutine(AttackLock(0.5f));
        }

        if (Input.GetKeyDown(chargedAttackKey))
        {
            animator.SetTrigger("ChargedAttack");
            StartCoroutine(AttackLock(1.0f));
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }
    }

    private void HandleGroundState()
    {
        bool grounded = IsGrounded();
        animator.SetBool("IsGrounded", grounded);

        if (!wasGrounded && grounded)
        {
            animator.SetTrigger("Land");
        }

        wasGrounded = grounded;
    }

    private void FixedUpdate()
    {
        if (isSleeping || isAttacking)
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

    private Transform FindClosestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = _firePoint.position;

        foreach (var enemyObj in enemies)
        {
            float dist = Vector3.Distance(enemyObj.transform.position, currentPos);
            if (dist < minDist)
            {
                closest = enemyObj.transform;
                minDist = dist;
            }
        }

        return closest;
    }

    private void Shoot()
    {
        var projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        var projectileScript = projectile.GetComponent<MirelightProjectile>();

        if (projectileScript != null)
        {
            Transform closestEnemy = FindClosestEnemy();
            Vector2 dirToEnemy = closestEnemy != null
                ? (closestEnemy.position - _firePoint.position).normalized
                : (spriteRenderer.flipX ? Vector2.left : Vector2.right);

            projectileScript.Initialize(dirToEnemy, 10f);
        }
    }

    public void PlayHurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Die");
        rb.linearVelocity = Vector2.zero;
        this.enabled = false;
    }
}
