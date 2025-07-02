using BossLevel.Core;
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightEnemy : BossLevelBaseMono
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float changeDirectionTime = 2f;
        [SerializeField] private GameObject frogPrefab;

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
            for (int i = 0; i < frogsPerWave; i++)
            {
                Vector2 offset = Random.insideUnitCircle * spawnRadius;
                Instantiate(frogPrefab, (Vector2)transform.position + offset, Quaternion.identity);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
