using Chocolate4.AttackInput;
using Chocolate4.Level;
using Chocolate4.MoveInput;
using Chocolate4.SaveLoad;
using UnityEngine;

namespace Chocolate4.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Entity : SaveableMonoBehaviour
    {
        public EntitySettings Settings;
        [HideInInspector] public Hp Hp;
        [SerializeField, HideInInspector] 
        private IAttackInput attackInput;
        [SerializeField, HideInInspector] 
        private IMoveInput moveInput;
        [SerializeField, HideInInspector] 
        private AnimationController animController;
        private Animator anim;
        private Rigidbody rb;
        protected MaterialPropertyBlock mpb;
        protected int colorID = Shader.PropertyToID("_Color");
        protected MeshRenderer mr;
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

        public virtual void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
        }
        public virtual void Initialize()
        {
            attackInput = Settings.IsAI ? 
                new AIAttackInput() : new PlayerAttackInput();
            
            moveInput = Settings.IsAI ? 
                new AIMoveInput() : new PlayerMoveInput(Camera.main);

            animController = new AnimationController();

            // randomize material color for fun!
            mpb.SetColor(
                colorID, Random.ColorHSV(0f, 1f, .1f, .4f, 0f, 1f)
            );
            mr.SetPropertyBlock(mpb);
        }
        public void UpdateEntity()
        {
            // animController.UpdateAnimation(anim, rb.velocity);
            moveInput.ReadMoveInput(MoveSpeed);

            Vector3? rotation = moveInput.Rotation;
            if (rotation != null)
            {
                transform.LookAt((Vector3)rotation, Vector3.up);
            }
        }
        public void FixedUpdateEntity()
        {
            Quaternion angle = Quaternion.AngleAxis(CameraMovement._YRot, Vector3.up);
            Vector3 direction = transform.position + angle * moveInput.Translation;
            rb.MovePosition(direction);
        }
    }
}
