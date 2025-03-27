using System;
using BossLevel.Core.Managers;
using UnityEngine.SceneManagement;

namespace BossLevel.Core
{
    public class BossLevelGameLoader: BossLevelBaseMono 
    {
        private void Awake()
        {
            //load the game
            new BossLevelCoreManager();
            LoadNextScene();
        }

        // private void Start()
        // {
        //      
        // }
        
        private void LoadNextScene()
        {
            //load the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
