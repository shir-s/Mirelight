using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightEnemy : BossLevelBaseMono
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float changeDirectionTime = 2f;

        [Header("Frogs Settings")]
        [SerializeField] private GameObject[] frogPrefabs; // array for multiple frog types
        [SerializeField] private int frogsPerWave = 5;
        [SerializeField] private float spawnDelay = 0.8f;
        [SerializeField] private float timeBetweenWaves = 5f;
        [SerializeField] private float spawnRadius = 0.5f;

        private Vector2 moveDirection;
        private float timer;
        private bool spawnedFrogs = false;

        private void Start()
        {
            ChooseNewDirection();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= changeDirectionTime)
            {
                ChooseNewDirection();
                timer = 0f;
            }

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                moveDirection = new Vector2(-moveDirection.x, -moveDirection.y);
            }
            else if (collision.collider.CompareTag("Player"))
            {
                MirelightPlayerController player = collision.collider.GetComponent<MirelightPlayerController>();
                if (player != null)
                {
                    player.TakeDamage();
                }
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
            // always at least one of each type if we have them
            foreach (var frogPrefab in frogPrefabs)
            {
                SpawnSingleFrog(frogPrefab);
                yield return new WaitForSeconds(spawnDelay);
            }

            // fill the rest randomly
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
