using UnityEngine;

namespace Chocolate4.Entities.Stats
{
    public class AIHp : Hp
    {
        private MaterialPropertyBlock mpb;
        [SerializeField] private MeshRenderer hpQuad;
        private readonly int hpID = Shader.PropertyToID("_Hp");

        public override void Initialize(int max)
        {
            base.Initialize(max);
            if (hpQuad != null)
            {
                mpb = new MaterialPropertyBlock();
                mpb.SetFloat(hpID, (float)current / max);
                hpQuad.SetPropertyBlock(mpb);
            }
        }
        public override void Damage(int damage)
        {
            base.Damage(damage);
            
            if (hpQuad != null)
            {
                mpb.SetFloat(hpID, (float)current / max);
                hpQuad.SetPropertyBlock(mpb);
            }
        }
    }
}
