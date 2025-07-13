using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightEnemy : BossLevelBaseMono
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float changeDirectionTime = 2f;

        [Header("Jump Attack Settings")]
        [SerializeField] private float jumpAttackInterval = 6f; // כל כמה זמן ינסה לקפוץ על השחקן
        [SerializeField] private float jumpAttackForce = 8f;     // עוצמת הקפיצה

        [Header("Frogs Settings")]
        [SerializeField] private GameObject[] frogPrefabs;
        [SerializeField] private int frogsPerWave = 5;
        [SerializeField] private float spawnDelay = 0.8f;
        [SerializeField] private float timeBetweenWaves = 5f;
        [SerializeField] private float spawnRadius = 0.5f;

        private Vector2 moveDirection;
        private float timer;
        private float jumpAttackTimer; // NEW

        private bool spawnedFrogs = false;
        private Transform player;      // NEW
        private Rigidbody2D rb;         // NEW

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            ChooseNewDirection();
            jumpAttackTimer = jumpAttackInterval; // NEW
        }

        private void Update()
        {
            timer += Time.deltaTime;
            jumpAttackTimer += Time.deltaTime; // NEW

            if (jumpAttackTimer >= jumpAttackInterval && player != null)
            {
                JumpAttack();
                jumpAttackTimer = 0f;
            }
            else if (timer >= changeDirectionTime)
            {
                ChooseNewDirection();
                timer = 0f;
            }

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        private void JumpAttack()
        {
            if (player == null || rb == null) return;

            Vector2 directionToPlayer = (player.position - transform.position).normalized;

            // נגרום לו "לקפוץ" לשחקן
            rb.AddForce(new Vector2(directionToPlayer.x, 1).normalized * jumpAttackForce, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                moveDirection = new Vector2(-moveDirection.x, -moveDirection.y);
            }
            else if (collision.collider.CompareTag("Player"))
            {
                // MirelightPlayerController player = collision.collider.GetComponent<MirelightPlayerController>();
                // if (player != null)
                // {
                //     player.TakeDamage();
                // }
                collision.collider.GetComponent<MirelightPlayerHealth>()?.TakeDamage(1);
            }
        }

        private void ChooseNewDirection()
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            moveDirection = new Vector2(x, y).normalized;
        }

        public void SpawnFrogs()
        {
            if (!spawnedFrogs)
            {
                spawnedFrogs = true;
                StartCoroutine(SpawnFrogsRoutine());
            }
        }

        private System.Collections.IEnumerator SpawnFrogsRoutine()
        {
            yield return SpawnWave();
            yield return new WaitForSeconds(timeBetweenWaves);
            yield return SpawnWave();
        }

        private System.Collections.IEnumerator SpawnWave()
        {
            foreach (var frogPrefab in frogPrefabs)
            {
                SpawnSingleFrog(frogPrefab);
                yield return new WaitForSeconds(spawnDelay);
            }

            for (int i = frogPrefabs.Length; i < frogsPerWave; i++)
            {
                var randomFrogPrefab = frogPrefabs[Random.Range(0, frogPrefabs.Length)];
                SpawnSingleFrog(randomFrogPrefab);
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        private void SpawnSingleFrog(GameObject frogPrefab)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Instantiate(frogPrefab, (Vector2)transform.position + offset, Quaternion.identity);
        }
    }
}
