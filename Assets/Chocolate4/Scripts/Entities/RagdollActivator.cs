using System.Collections.Generic;
using Chocolate4.Entities.Stats;
using UnityEngine;

namespace Chocolate4.Entities
{
    [RequireComponent(typeof(Hp))]
    public class RagdollActivator : MonoBehaviour
    {
        [SerializeField, HideInInspector] 
        private List<Collider> ragdollParts;
        private Animator anim;
        private Hp hp;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            hp = GetComponent<Hp>();
            GetRagdollParts();
        }
        private void OnEnable() => hp.OnKill += TurnOnRagdoll;
        private void OnDisable() => hp.OnKill -= TurnOnRagdoll;
        private void TurnOnRagdoll()
        {
            anim.enabled = false;
            anim.avatar = null;

            foreach (Collider c in ragdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.isKinematic = false;
                c.attachedRigidbody.useGravity = true;
            }
        }
        private void GetRagdollParts()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider c in colliders)
            {
                // mele weapons with trigger
                if (c.enabled == false) continue;

                if (c.gameObject != gameObject)
                {
                    c.attachedRigidbody.useGravity = false;
                    c.attachedRigidbody.isKinematic = true;
                    c.isTrigger = true;
                    ragdollParts.Add(c);
                }
            }
        }
    }
}
