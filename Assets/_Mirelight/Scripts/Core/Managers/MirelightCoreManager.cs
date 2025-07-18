using BossLevel.Gameplay.Controls;
using UnityEngine;

namespace BossLevel.Core.Managers
{
    public class MirelightCoreManager : BossLevelBaseMono
    {
        private void Awake()
        {
            // Load and place player
            GameObject player = LoadAndPlace("CoreObjects/PlayerForestSpirit", new Vector3(-45f, 14f, 0));

            if (player != null)
            {
                // Connect to camera
                var cameraFollow = Camera.main.GetComponent<CameraFollows>();
                if (cameraFollow != null)
                {
                    cameraFollow.player = player.transform;
                }
                
                // Connect to health UI
                var healthUI = FindObjectOfType<PlayerHealthUI>();
                if (healthUI != null)
                {
                    var playerHealth = player.GetComponent<MirelightPlayerHealth>();
                    if (playerHealth != null)
                    {
                        healthUI.SetPlayerHealth(playerHealth);
                    }
                }
            }

            // Load and place enemy
            GameObject enemy = LoadAndPlace("CoreObjects/Enemy", new Vector3(-80f, -40f, 0));
            if (enemy != null && player != null)
            {
                var bossController = enemy.GetComponent<MirelightBossController>();
                if (bossController != null)
                {
                    bossController.SetPlayerTarget(player.transform);
                }
            }

            // Load frogs
            LoadAndPlace("CoreObjects/RedFroggy", new Vector3(-19f, 12f, 0));
            LoadAndPlace("CoreObjects/RedFroggy", new Vector3(-30f, -7f, 0));
        }

        private GameObject LoadAndPlace(string resourcePath, Vector3 position)
        {
            GameObject prefab = Resources.Load<GameObject>(resourcePath);
            if (prefab == null)
            {
                Debug.LogError($"[MirelightCoreManager] Failed to load prefab at path: {resourcePath}");
                return null;
            }

            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.position = position;
            return obj;
        }
    }
}