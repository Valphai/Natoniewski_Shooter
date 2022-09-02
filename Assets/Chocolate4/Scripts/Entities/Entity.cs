using Chocolate4.Entities.AttackInput;
using Chocolate4.Entities.MoveInput;
using Chocolate4.SaveLoad;
using Chocolate4.Entities.Weapons;
using Chocolate4.Entities.Stats;
using UnityEngine;
using System;

namespace Chocolate4.Entities
{
    [RequireComponent(typeof(Rigidbody), typeof(Hp))]
    public abstract class Entity : SaveableMonoBehaviour
    {
        public EntitySettings Settings;
        [HideInInspector] public Hp Hp;
        [HideInInspector] public IAttackInput AttackInput;
        [HideInInspector] public IMoveInput MoveInput;
        [SerializeField, HideInInspector] 
        private AnimationController animController;
        private Animator anim;
        [SerializeField] protected Weapon defaultWeapon;
        protected Rigidbody rb;
        protected MaterialPropertyBlock mpb;
        protected readonly int colorID = Shader.PropertyToID("_BaseColor");
        protected SkinnedMeshRenderer mr;
        private float _moveSpeed;
        public float MoveSpeed
        {
            get 
            {
                if (_moveSpeed == 0f) 
                    return Settings.MoveSpeed;
                return _moveSpeed; 
            }
            set { _moveSpeed = value; }
        }
        public static event Action<Entity> OnKilled;

        private void OnValidate()
        {
            anim = GetComponentInChildren<Animator>();
            mr = GetComponentInChildren<SkinnedMeshRenderer>();
            rb = GetComponent<Rigidbody>();
            Hp = GetComponent<Hp>();
            SetBoxesColor();
        }
        private void OnEnable() => Hp.OnKill += Kill;
        private void OnDisable() => Hp.OnKill -= Kill;
        public virtual void Initialize()
        {
            animController = new AnimationController();
        }
        public virtual void UpdateEntity()
        {
            animController.UpdateAnimation(
                anim, rb.velocity, MoveSpeed
            );
            MoveInput.ReadMoveInput(MoveSpeed);
            AttackInput.ReadAttackInput();
        }
        public virtual void SetBoxesColor()
        {
            mpb = new MaterialPropertyBlock();
            mpb.SetColor(
                colorID, UnityEngine.Random.ColorHSV(0f, 1f, .1f, .4f, 0f, 1f)
            );
            mr.SetPropertyBlock(mpb, 0);
        }
        private void Kill() => OnKilled?.Invoke(this);
    }
}
