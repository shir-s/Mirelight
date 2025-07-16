using System.Collections;
using UnityEngine;

namespace BossLevel.Gameplay.Controls.Boss
{
    public class MirelightBossPhaseTwo : IMirelightBossPhase
    {
        private Transform bossTransform;
        private float moveSpeed;
        private float changeDirectionTime;

        private Vector2 moveDirection;
        private float timer;

        private MonoBehaviour coroutineRunner;
        private GameObject[] frogPrefabs;
        private int frogsPerWave;
        private float spawnDelay;
        private float timeBetweenWaves;
        private float spawnRadius;

        private bool hasSpawnedFrogs = false;

        public MirelightBossPhaseTwo(
            Transform bossTransform,
            float moveSpeed,
            float changeDirectionTime,
            MonoBehaviour coroutineRunner,
            GameObject[] frogPrefabs,
            int frogsPerWave,
            float spawnDelay,
            float timeBetweenWaves,
            float spawnRadius)
        {
            this.bossTransform = bossTransform;
            this.moveSpeed = moveSpeed;
            this.changeDirectionTime = changeDirectionTime;
            this.coroutineRunner = coroutineRunner;
            this.frogPrefabs = frogPrefabs;
            this.frogsPerWave = frogsPerWave;
            this.spawnDelay = spawnDelay;
            this.timeBetweenWaves = timeBetweenWaves;
            this.spawnRadius = spawnRadius;

            ChooseNewDirection();
        }

        public void Tick(float deltaTime)
        {
            timer += deltaTime;

            if (timer >= changeDirectionTime)
            {
                ChooseNewDirection();
                timer = 0f;
            }

            bossTransform.Translate(moveDirection * moveSpeed * deltaTime);

            if (!hasSpawnedFrogs)
            {
                hasSpawnedFrogs = true;
                coroutineRunner.StartCoroutine(SpawnFrogsRoutine());
            }
        }

        private void ChooseNewDirection()
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            moveDirection = new Vector2(x, y).normalized;
        }

        private IEnumerator SpawnFrogsRoutine()
        {
            yield return SpawnWave();
            yield return new WaitForSeconds(timeBetweenWaves);
            yield return SpawnWave();
        }

        private IEnumerator SpawnWave()
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
            Object.Instantiate(frogPrefab, (Vector2)bossTransform.position + offset, Quaternion.identity);
        }
    }
}
