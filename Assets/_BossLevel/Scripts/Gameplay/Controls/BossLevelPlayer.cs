using System;
using BossLevel.Core;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace BossLevel.Gameplay.Controls
{
    public class BossLevelPlayer: BossLevelBaseMono 
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private GameObject _projectilePrefab; // הפריפאב של הירייה
        [SerializeField] private Transform _firePoint; // נקודת הירי
        [SerializeField] private float _projectileSpeed = 10f;
        
        public void Update()
        {
            CheckInput();
        }
        
        private void CheckInput()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            
            var movement = new Vector3(horizontal,vertical, 0).normalized;
            
            transform.position += movement * Time.deltaTime * _speed;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        
        private void Shoot()
        {
            var projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
            var rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.up * _projectileSpeed;
            }
        }
    }
}
