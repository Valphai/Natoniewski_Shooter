
using System;
using Chocolate4.Entities.Stats;
using UnityEngine;

namespace Chocolate4.Entities.Weapons
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 direction;
        private float speed;
        private int damage;
        private Rifle rifle;
        
        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        public void Initialize(
            Transform spawnPoint, float speed, 
            int damage, Rifle rifle
        )
        {
            direction = spawnPoint.forward;
            transform.position = spawnPoint.position;
            this.speed = speed;
            this.damage = damage;
            this.rifle = rifle;
        }
        public void ReturnToFactory() => rifle.ReturnToFactory(this);
        private void OnTriggerEnter(Collider other)
        {
            Hp targetHp = other.gameObject.GetComponent<Hp>();
            if (targetHp != null && other.gameObject.tag == "Enemy")
            {
                targetHp.Damage(damage);
            }
        }

    }
}