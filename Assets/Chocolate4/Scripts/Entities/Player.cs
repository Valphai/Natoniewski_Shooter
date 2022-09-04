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

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Initialize();
        }
        private void Start()
        {
            OnPlayerJoin?.Invoke(this);
        }
        public override void Initialize()
        {
            base.Initialize();
            AttackInput = AttackInput ?? new PlayerAttackInput(input, defaultWeapon);
            MoveInput = MoveInput ?? new PlayerMoveInput();
            animController = new AnimationController(anim, AttackInput);
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
        protected override void ApplyMovement()
        {
            // offset input angle by camera rotation
            Quaternion angle = CameraMovement.Instance.CurrentRotation();
            Vector3 direction = angle * MoveInput.Translation;
            rb.velocity = direction;

            transform.forward = direction == Vector3.zero ? 
                transform.forward : direction;
        }
    }
}