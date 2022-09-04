using System;
using Chocolate4.Entities.Weapons;

namespace Chocolate4.Entities.AttackInput
{
    public interface IAttackInput
    {
        public Weapon Weapon { get; set; }
        public void ReadAttackInput();
        public event Action OnAttack;
    }
}
