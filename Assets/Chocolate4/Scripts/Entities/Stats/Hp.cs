using System;
using UnityEngine;

namespace Chocolate4.Entities.Stats
{
    public class Hp : MonoBehaviour
    {
        private int max;
        private int current;
        public event Action OnKill;

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
