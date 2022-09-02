using Chocolate4.Level;
using Chocolate4.Entities.Weapons;
using UnityEngine;

namespace Chocolate4.Entities.AttackInput
{
    public class PlayerAttackInput : IAttackInput
    {
        public Weapon Weapon { get; set; }
        private InputSettings input;

        public PlayerAttackInput(InputSettings input, Weapon weapon)
        {
            this.input = input; 
            Weapon = weapon;
        }
        public void ReadAttackInput()
        {
            if (!Weapon.IsOnCooldown())
            {
                if (Input.GetKey(input.ShootKey))
                {
                    Weapon.Attack();
                }
            }
        }
    }
}