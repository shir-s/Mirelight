using BossLevel.Gameplay.Controls;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IMirelightDamageable damageable = other.GetComponent<IMirelightDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(1); 
        }
    }
}