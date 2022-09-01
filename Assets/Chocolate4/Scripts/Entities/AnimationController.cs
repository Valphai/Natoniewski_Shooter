using UnityEngine;

namespace Chocolate4.Entities
{
    public class AnimationController
    {
        public void UpdateAnimation(
            Animator anim, float velocity, float maxSpeed
        )
        {
            float velocityNormalized = velocity / maxSpeed;
            anim.SetFloat("speed", velocityNormalized); 
        }
    }
}
