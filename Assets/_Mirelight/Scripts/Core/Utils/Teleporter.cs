using UnityEngine;

public class MirelightTeleporter : MonoBehaviour
{
    public Transform teleportTarget;
    public string playerTag = "Player";
    private float cooldownTime = 0.2f;
    private float nextTeleportTime = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && Time.time >= nextTeleportTime)
        {
            other.transform.position = teleportTarget.position;
            nextTeleportTime = Time.time + cooldownTime;
            Debug.Log($"Teleported to: {teleportTarget.position}");
        }
    }
}
