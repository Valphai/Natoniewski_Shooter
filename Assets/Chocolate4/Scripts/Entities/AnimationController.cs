using Chocolate4.Entities.AttackInput;
using UnityEngine;

namespace Chocolate4.Entities
{
    [System.Serializable]
    public class AnimationController
    {
        private Animator anim;
        private IAttackInput attackInput;

        public AnimationController(
            Animator anim, IAttackInput attackInput
        )
        {
            this.anim = anim;
            this.attackInput = attackInput;
            Enable();
        }
        private void Enable() => attackInput.OnAttack += TriggerAttack;
        public void Disable() => attackInput.OnAttack -= TriggerAttack;
        public void DanceNow()
        {
            anim.SetLayerWeight(1, 0f);
            anim.SetBool("game won", true);
        }
        public void UpdateAnimation(
            Vector3 velocity, float maxSpeed
        )
        {
            if (anim.enabled == true)
            {
                float velocityNormalized = velocity.magnitude / maxSpeed;
                anim.SetFloat("speed", velocityNormalized); 
            }
        }
        private void TriggerAttack() => anim.SetTrigger("attack");
    }
}
