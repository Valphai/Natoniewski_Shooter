using System;
using Chocolate4.Entities.Weapons;
using UnityEngine;

namespace Chocolate4.Entities.AttackInput
{
    public class AIAttackInput : IAttackInput
    {
        public Weapon Weapon { get; set; }
        private Transform playerTr, thisTr;
        public event Action OnAttack;

        public AIAttackInput(
            Transform playerTr, Transform thisTr,
            Weapon weapon
        )
        {
            this.playerTr = playerTr;
            this.thisTr = thisTr;
            Weapon = weapon;
        }
        public void ReadAttackInput()
        {
            if (!Weapon.IsOnCooldown())
            {
                float distanceToPlayer = 
                    Vector3.Distance(playerTr.position, thisTr.position);
                if (Weapon.IsInRange(distanceToPlayer))
                {
                    Weapon.Attack();
                    OnAttack?.Invoke();
                }
            }
        }
    }
}