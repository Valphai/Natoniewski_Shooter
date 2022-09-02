using UnityEngine;

namespace Chocolate4.Entities.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public int Damage;
        /// <summary>In seconds</summary>
        private float attackCooldown;
        /// <summary>In seconds</summary>
        [SerializeField] private float cooldownTime = 5f;
        [SerializeField] private ParticleSystem dmgParticles;
        public virtual void Attack()
        {
            ResetCooldown();
        }
        public bool IsOnCooldown()
        {
            if (attackCooldown <= 0)
            {
                return false;
            }
            attackCooldown -= 1;
            return true;
        }
        private void ResetCooldown() => attackCooldown = cooldownTime;
    }
}
