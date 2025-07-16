using BossLevel.Gameplay.Controls.Boss;
using Pathfinding; 
using UnityEngine;

namespace BossLevel.Gameplay.Controls
{
    public class MirelightBossController : MonoBehaviour
    {
        [Header("Phase Settings")]
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float changeDirectionTime = 2f;
        [SerializeField] private GameObject[] frogPrefabs;
        [SerializeField] private int frogsPerWave = 5;
        [SerializeField] private float spawnDelay = 0.8f;
        [SerializeField] private float timeBetweenWaves = 5f;
        [SerializeField] private float spawnRadius = 0.5f;

        [Header("Phase Three Settings")]
        [SerializeField] private float aStarSpeed = 3f;
        [SerializeField] private float nextWaypointDistance = 0.2f;
        [SerializeField] private Transform playerTarget;
        [SerializeField] private Seeker seeker;

        private IMirelightBossPhase currentPhase;

        private void Start()
        {
            currentPhase = new MirelightBossPhaseOne(
                transform,
                moveSpeed,
                changeDirectionTime
            );
        }

        private void Update()
        {
            currentPhase?.Tick(Time.deltaTime);
        }

        public void OnHealthChanged(int currentHealth, int maxHealth)
        {
            float percent = (float)currentHealth / maxHealth;

            if (percent <= 0.7f && !(currentPhase is MirelightBossPhaseTwo))
            {
                currentPhase = new MirelightBossPhaseTwo(
                    transform,
                    moveSpeed,
                    changeDirectionTime,
                    this,
                    frogPrefabs,
                    frogsPerWave,
                    spawnDelay,
                    timeBetweenWaves,
                    spawnRadius
                );
            }
            else if (percent <= 0.3f && !(currentPhase is MirelightBossPhaseThree))
            {
                currentPhase = new MirelightBossPhaseThree(
                    transform,
                    aStarSpeed,
                    nextWaypointDistance,
                    playerTarget,
                    seeker
                );
            }
        }
    }
}
