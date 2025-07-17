using System.Collections;
using TMPro;
using UnityEngine;

public class MirelightTypewriter : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string fullText;
    public float typingSpeed = 0.05f;
    public float delayBeforeStart = 5f;

    // private void Start()
    // {
    //     StartCoroutine(StartWithDelay());
    // }
    //
    // private IEnumerator StartWithDelay()
    // {
    //     yield return new WaitForSeconds(delayBeforeStart);
    //     StartTyping();
    // }

    public void StartTyping()
    {
        StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        dialogueText.text = "";

        // Play the typing sound once
        if (MirelightSoundManager.Instance != null)
            MirelightSoundManager.Instance.PlayTypewriter();

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}