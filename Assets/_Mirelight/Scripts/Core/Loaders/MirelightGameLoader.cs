using System;
using BossLevel.Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BossLevel.Core
{
    public class MirelightGameLoader: BossLevelBaseMono 
    {
        private void Awake()
        {
            //load the game
            new MirelightCoreManager();
            LoadNextScene();
        }
        
        private void LoadNextScene()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log($"Current scene index: {currentIndex}");
            int nextIndex = currentIndex + 1;
            Debug.Log($"Trying to load next scene index: {nextIndex}");
            SceneManager.LoadScene(1); // במקום current + 1

        }


    }
}
