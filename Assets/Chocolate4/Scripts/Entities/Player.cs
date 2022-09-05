using System;
using Chocolate4.Entities.AttackInput;
using Chocolate4.Entities.MoveInput;
using Chocolate4.Level;
using UnityEngine;

namespace Chocolate4.Entities
{
    public class Player : Entity
    {
        [SerializeField] private InputSettings input;
        private Rigidbody rb;
        public static event Action<Entity> OnPlayerJoin;
        public static event Action OnPlayerKilled;

        public override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            Initialize();
            OnPlayerJoin?.Invoke(this);
        }
        public override void Initialize()
        {
            AttackInput = AttackInput ?? new PlayerAttackInput(input, defaultWeapon);
            MoveInput = MoveInput ?? new PlayerMoveInput();
            base.Initialize();
        }
        public override void UpdateEntity()
        {
            base.UpdateEntity();
            animController.UpdateAnimation(
                rb.velocity, MoveSpeed
            );
        }
        public override void SetBoxesColor()
        {
            // nice blue player color
            mpb = new MaterialPropertyBlock();
            mpb.SetColor(
                colorID, new Color(0.1157037f, 0.4686949f, 0.9674544f, 1f)
            );
            mr.SetPropertyBlock(mpb, 0);
        }
        public void GameWon()
        {
            animController.DanceNow();
        }
        protected override void ApplyMovement()
        {
            // offset input angle by camera rotation
            Quaternion angle = CameraMovement.Instance.CurrentRotation();
            Vector3 direction = angle * MoveInput.Translation;
            rb.velocity = direction;

            transform.forward = direction == Vector3.zero ? 
                transform.forward : direction;
        }
        protected override void Kill()
        {
            base.Kill();
            OnPlayerKilled?.Invoke();
        }
    }
}