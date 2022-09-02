using UnityEngine;

namespace Chocolate4.Entities
{
    public class AnimationController
    {
        public void UpdateAnimation(
            Animator anim, Vector3 velocity, float maxSpeed
        )
        {
            float velocityNormalized = velocity.magnitude / maxSpeed;
            anim.SetFloat("speed", velocityNormalized); 
        }
    }
}
