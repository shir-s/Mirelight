using UnityEngine;
using UnityEngine.SceneManagement;

public class MirelightGameManager : MonoBehaviour
{
    public static MirelightGameManager Instance;

    [Header("UI Panels")]
    public GameObject losePanel;
    public GameObject winPanel;

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
        // בדיקה בטוחה אם הפאנלים עדיין קיימים
        if (((winPanel != null && winPanel.activeSelf) || (losePanel != null && losePanel.activeSelf))
            && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }

        // בדיקות בדיקה בלבד
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
        if (losePanel != null)
            losePanel.SetActive(true);
    }

    public void PlayerWon()
    {
        Debug.Log("Player won the game!");
        if (winPanel != null)
            winPanel.SetActive(true);
    }
}