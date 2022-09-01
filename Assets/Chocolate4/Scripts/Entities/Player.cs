using System;
using UnityEngine;

namespace Chocolate4.Entities
{
    public class Player : Entity
    {
        public static event Action<Entity> OnPlayerJoin;

        public override void Awake()
        {
            base.Awake();
            Initialize();
        }
        private void Start()
        {
            OnPlayerJoin?.Invoke(this);
        }
        public override void Initialize()
        {
            base.Initialize();
            // nice blue player color
            mpb.SetColor(
                colorID, new Color(0.1157037f, 0.4686949f, 0.9674544f, 1f)
            );
            mr.SetPropertyBlock(mpb);
        }
    }
}