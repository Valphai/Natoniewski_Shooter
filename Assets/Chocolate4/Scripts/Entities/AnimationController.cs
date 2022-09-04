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
        }
        private void OnEnable() => attackInput.OnAttack += TriggerAttack;
        private void OnDisable() => attackInput.OnAttack -= TriggerAttack;
        private void TriggerAttack() => anim.SetTrigger("attack");
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
    }
}
