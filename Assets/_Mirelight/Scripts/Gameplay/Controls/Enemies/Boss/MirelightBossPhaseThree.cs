using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

namespace BossLevel.Gameplay.Controls.Boss
{
    public class MirelightBossPhaseThree : IMirelightBossPhase
    {
        private Transform bossTransform;
        private float speed;
        private float nextWaypointDistance;

        private Transform target;
        private Seeker seeker;
        private Path path;
        private int currentWaypoint = 0;

        private float pathUpdateTimer = 0f;
        private float pathUpdateInterval = 0.5f;

        public MirelightBossPhaseThree(
            Transform bossTransform,
            float speed,
            float nextWaypointDistance,
            Transform target,
            Seeker seeker)
        {
            this.bossTransform = bossTransform;
            this.speed = speed;
            this.nextWaypointDistance = nextWaypointDistance;
            this.target = target;
            this.seeker = seeker;

            UpdatePath();
        }

        public void Tick(float deltaTime)
        {
            pathUpdateTimer += deltaTime;
            if (pathUpdateTimer >= pathUpdateInterval)
            {
                UpdatePath();
                pathUpdateTimer = 0f;
            }

            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
                return;

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)bossTransform.position).normalized;
            bossTransform.Translate(direction * speed * deltaTime);

            float distance = Vector2.Distance(bossTransform.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }

        private void UpdatePath()
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(bossTransform.position, target.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }
    }
}
