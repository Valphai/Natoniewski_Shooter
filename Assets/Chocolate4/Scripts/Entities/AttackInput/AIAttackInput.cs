using Chocolate4.Entities.Weapons;

namespace Chocolate4.Entities.AttackInput
{
    public class AIAttackInput : IAttackInput
    {
        public Weapon Weapon { get; set; }

        public AIAttackInput(Weapon weapon)
        {
            Weapon = weapon;
        }
        public void ReadAttackInput()
        {
        }
    }
}