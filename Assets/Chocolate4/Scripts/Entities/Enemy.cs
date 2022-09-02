using Chocolate4.Entities.AttackInput;
using Chocolate4.Entities.MoveInput;

namespace Chocolate4.Entities
{
    public class Enemy : Entity
    {
        public override void Initialize()
        {
            base.Initialize();
            AttackInput = AttackInput ?? new AIAttackInput(defaultWeapon);
            MoveInput = MoveInput ?? new AIMoveInput();
        }
    }
}