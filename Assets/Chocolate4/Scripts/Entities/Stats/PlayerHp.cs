using System;

namespace Chocolate4.Entities.Stats
{
    public class PlayerHp : Hp
    {
        public static event Action<float> OnPlayerDamaged;

        public override void Initialize(int max)
        {
            base.Initialize(max);
            OnPlayerDamaged?.Invoke((float)current/max);
        }
        public override void Damage(int damage)
        {
            base.Damage(damage);
            OnPlayerDamaged?.Invoke((float)current/max);
        }
    }
}
