using System;
using UnityEngine;

namespace Chocolate4.Entities.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public int Damage;
        /// <summary>In seconds</summary>
        private float remainingTime;
        /// <summary>In seconds</summary>
        [SerializeField] private float cooldownDuration;
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
            if (remainingTime <= 0)
            {
                return false;
            }
            remainingTime -= Time.deltaTime;
            return true;
        }
        /// <summary>AI specified attack range</summary>
        public virtual bool IsInRange(float distance) => distance <= attackRange;
        private void ResetCooldown() => remainingTime = cooldownDuration;
    }
}
