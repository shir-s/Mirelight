using UnityEngine;
using UnityEngine.SceneManagement;

public class MirelightGameManager : MonoBehaviour
{
    public static MirelightGameManager Instance;

    [Header("UI Panels")]
    public GameObject losePanel;
    public GameObject winPanel;
    public bool IsInOpening = true;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        
        if (!IsInOpening && ((winPanel != null && winPanel.activeSelf) || (losePanel != null && losePanel.activeSelf))
                         && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
        
        // just for testing purposes, remove in production
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerLost();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerWon();
        }
    }

    public void RestartGame()
    {
        MirelightSoundManager.Instance.PlayBackgroundMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerLost()
    {
        Debug.Log("Player lost the game!");

        // Stop all other sounds (background, footsteps, etc.)
        MirelightSoundManager.Instance.StopAllSounds();

        // Play the lose screen sound
        MirelightSoundManager.Instance.PlayLoseSound();

        // Show the lose panel
        if (losePanel != null)
            losePanel.SetActive(true);
    }


    public void PlayerWon()
    {
        Debug.Log("Player won the game!");

        // Stop all other sounds (background, footsteps, etc.)
        MirelightSoundManager.Instance.StopAllSounds();

        // Play the win sound
        MirelightSoundManager.Instance.PlayWinSound();

        if (winPanel != null)
            winPanel.SetActive(true);
    }
    
    
    public void SetIsInOpeningFalse()
    {
        IsInOpening = false;
    }

}