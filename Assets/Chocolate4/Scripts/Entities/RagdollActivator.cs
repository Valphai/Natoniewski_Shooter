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
        private List<Vector3> originalPositions;
        private Animator anim;
        private Hp hp;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            hp = GetComponent<Hp>();
            GetRagdollParts();
        }
        private void OnEnable()
        {
            hp.OnKill += TurnOnRagdoll;
            ResetRagdoll();
        }
        private void OnDisable() => hp.OnKill -= TurnOnRagdoll;
        private void TurnOnRagdoll()
        {
            anim.enabled = false;
            // anim.avatar = null;

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
            originalPositions = new List<Vector3>();

            foreach (Collider c in colliders)
            {
                // mele weapons with trigger
                if (c.enabled == false) continue;

                if (c.gameObject != gameObject)
                {
                    originalPositions.Add(c.transform.position);
                    ragdollParts.Add(c);
                }
            }
        }
        private void ResetRagdoll()
        {
            for (int i = 0; i < ragdollParts.Count; i++)
            {
                Collider c = ragdollParts[i];

                if (originalPositions != null)
                {
                    // this seems to reset the ragdoll state somehow
                    c.transform.position = originalPositions[i];
                }
                c.attachedRigidbody.useGravity = false;
                c.attachedRigidbody.isKinematic = true;
                c.isTrigger = true;
            }
            anim.enabled = true;
        }
    }
}
