using Chocolate4.Entities.AttackInput;
using Chocolate4.Entities.MoveInput;
using Chocolate4.Entities.Weapons;
using Chocolate4.Entities.Stats;
using UnityEngine;
using System;

namespace Chocolate4.Entities
{
    [RequireComponent(typeof(Hp))]
    public abstract class Entity : MonoBehaviour
    {
        public EntitySettings Settings;
        public bool IsInitialized { get; private set; }
        [HideInInspector] public Hp Hp;
        [HideInInspector] public IAttackInput AttackInput;
        [HideInInspector] public IMoveInput MoveInput;
        protected AnimationController animController;
        protected Animator anim;
        protected MaterialPropertyBlock mpb;
        protected SkinnedMeshRenderer mr;
        protected readonly int colorID = Shader.PropertyToID("_BaseColor");
        [SerializeField] protected Weapon defaultWeapon;
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

        protected abstract void ApplyMovement();
        public virtual void OnValidate()
        {
            Awake();
        }
        public virtual void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            mr = GetComponentInChildren<SkinnedMeshRenderer>();
            Hp = GetComponent<Hp>();
            
            if (!mr.HasPropertyBlock())
            {
                SetBoxesColor();
            }
        }
        public virtual void OnEnable() => Hp.OnKill += Kill;
        public virtual void OnDisable()
        {
            animController.Disable();
            Hp.OnKill -= Kill;
        }
        public virtual void Initialize()
        {
            animController = new AnimationController(anim, AttackInput);
            Hp.Initialize(Settings.MaxHp);
            IsInitialized = true;
        }
        public virtual void UpdateEntity()
        {
            MoveInput.ReadMoveInput(MoveSpeed);
            AttackInput.ReadAttackInput();

            ApplyMovement();
        }
        public virtual void SetBoxesColor()
        {
            mpb = new MaterialPropertyBlock();
            mpb.SetColor(
                colorID, UnityEngine.Random.ColorHSV(0f, 1f, .1f, .4f, 0f, 1f)
            );
            mr.SetPropertyBlock(mpb, 0);
        }
        protected virtual void Kill() => OnKilled?.Invoke(this);
    }
}
