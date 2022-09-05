using System;
using Chocolate4.Entities.AttackInput;
using Chocolate4.Entities.MoveInput;
using UnityEngine;
using UnityEngine.AI;

namespace Chocolate4.Entities
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Entity
    {
        public float ChaseRange;
        public Player Player;
        private NavMeshAgent agent;
        
        public override void Awake()
        {
            base.Awake();
            agent = GetComponent<NavMeshAgent>();
        }
        public override void Initialize()
        {
            ResetEntity();
            AttackInput = AttackInput ?? new AIAttackInput(
                                        Player.transform, transform,
                                        defaultWeapon
                                    );
            MoveInput = MoveInput ?? new AIMoveInput(
                                        Player.transform, 
                                        transform, ChaseRange
                                    );
            base.Initialize();
        }
        public override void UpdateEntity()
        {
            base.UpdateEntity();
            animController.UpdateAnimation(
                agent.velocity, agent.speed
            );
        }
        protected override void ApplyMovement()
        {
            agent.destination = MoveInput.Translation;
        }
        protected override void Kill()
        {
            base.Kill();
            agent.enabled = false;
        } 
        private void ResetEntity()
        {
            agent.enabled = true;
            AttackInput = null;
            MoveInput = null;
        }
        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawWireDisc(
            transform.position + Vector3.up, 
            Vector3.up, 
            ChaseRange
        );
        }
        #endif
    }
}