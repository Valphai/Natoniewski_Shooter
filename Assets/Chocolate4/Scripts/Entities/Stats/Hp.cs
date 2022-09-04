using System;
using UnityEngine;

namespace Chocolate4.Entities.Stats
{
    [System.Serializable]
    public class Hp : MonoBehaviour
    {
        private int current;
        public event Action OnKill;

        public void Initialize(int max) => current = max;
        public void Damage(int damage)
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
