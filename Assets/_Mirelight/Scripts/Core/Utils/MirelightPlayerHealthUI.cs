using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    [SerializeField] private MirelightPlayerHealth playerHealth;

    private void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            healthText.text = " HP: " + playerHealth.currentHealth.ToString();
        }
    }
}