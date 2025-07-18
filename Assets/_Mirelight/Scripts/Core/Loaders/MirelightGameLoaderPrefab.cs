using UnityEngine;
using UnityEngine.SceneManagement;

namespace BossLevel.Core
{
    public class MirelightGameLoaderPrefab : BossLevelBaseMono
    {
        // Names of prefabs in the Resources/Prefabs folder
        private readonly string[] essentialPrefabNames = {
            "GameManager",
            "SoundManager",
            "PlayerForestSpirit", // או כל דמות ראשית שצריכה להיות מוכנה מראש
            "Canvas" // אם צריך UI מראש
        };

        private void Awake()
        {
            LoadEssentialPrefabs();
            LoadNextScene();
        }

        private void LoadEssentialPrefabs()
        {
            foreach (string prefabName in essentialPrefabNames)
            {
                GameObject prefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");
                if (prefab == null)
                {
                    Debug.LogError($" Could not find prefab '{prefabName}' in Resources/Prefabs/");
                    continue;
                }

                GameObject instance = Instantiate(prefab);
                DontDestroyOnLoad(instance);
                Debug.Log($"✅ Loaded and preserved prefab: {prefabName}");
            }
        }

        private void LoadNextScene()
        {
            // למשל לטעון תמיד את GameLevel
            SceneManager.LoadScene("GameLevel");
        }
    }
}