using System;
using UnityEngine;

namespace Chocolate4.Entities.Stats
{
    [System.Serializable]
    public abstract class Hp : MonoBehaviour
    {
        protected int current;
        protected int max;
        public event Action OnKill;

        public virtual void Initialize(int max)
        {
            current = max;
            this.max = max;
        }
        public virtual void Damage(int damage)
        {
            current -= damage;

            if (current < 0)
            {
                current = 0;
                OnKill?.Invoke();
                return;
            }
        }
    }
}
