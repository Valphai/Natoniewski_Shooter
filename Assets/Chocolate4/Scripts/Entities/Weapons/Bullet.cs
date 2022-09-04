
using Chocolate4.Entities.Stats;
using Chocolate4.Helpers;
using UnityEngine;

namespace Chocolate4.Entities.Weapons
{
    [RequireComponent(typeof(TrailRenderer))]
    public class Bullet : MonoBehaviour
    {
        private Vector3 direction;
        private float speed;
        private int damage;
        private Rifle rifle;
        private TrailRenderer trailRenderer;
        private ParticleSystem dmgParticles;

        private void Awake()
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }
        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        public void Initialize(
            Transform spawnPoint, float speed, 
            int damage, Rifle rifle, ParticleSystem dmgParticles
        )
        {
            direction = spawnPoint.forward;
            transform.position = spawnPoint.position;
            this.speed = speed;
            this.damage = damage;
            this.rifle = rifle;
            this.dmgParticles = dmgParticles;
            trailRenderer.Clear();
        }
        public void ReturnToFactory() => rifle.ReturnToFactory(this);
        private void OnTriggerEnter(Collider other)
        {
            Hp targetHp = other.gameObject.GetComponent<Hp>();
            if (targetHp != null && other.gameObject.tag == "Enemy")
            {
                targetHp.Damage(damage);

                Vector3 contactPoint = transform.position;
                ParticleSystem instance = 
                    Instantiate(dmgParticles, contactPoint, Quaternion.identity);
                Destroy(instance.gameObject, 3f);
            }
        }
    }
}