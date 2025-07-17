using UnityEngine;
using UnityEngine.Events;

public class MirelightOpeningController : MonoBehaviour
{
    public UnityEvent onOpeningComplete; 
    public GameObject openingPanel;

    private bool hasStarted = false;

    private void Update()
    {
        if (!hasStarted && Input.GetKeyDown(KeyCode.Return))
        {
            hasStarted = true;
            openingPanel.SetActive(false);
            onOpeningComplete?.Invoke(); 
        }
    }
}