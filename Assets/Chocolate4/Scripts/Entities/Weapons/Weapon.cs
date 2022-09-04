using System;
using UnityEngine;

namespace Chocolate4.Entities.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public int Damage;
        /// <summary>In seconds</summary>
        private float attackCooldown;
        /// <summary>In seconds</summary>
        [SerializeField] private float cooldownTime = 15f;
        [SerializeField] private float attackRange;
        [SerializeField] protected ParticleSystem dmgParticles;
        
        protected abstract void AttackLogic();
        public virtual void Attack()
        {
            ResetCooldown();
            AttackLogic();
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
        /// <summary>AI specified attack range</summary>
        public virtual bool IsInRange(float distance) => distance <= attackRange;
        private void ResetCooldown() => attackCooldown = cooldownTime;
    }
}
