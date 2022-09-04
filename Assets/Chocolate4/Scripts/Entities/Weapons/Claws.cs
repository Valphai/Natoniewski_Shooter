using System.Collections;
using Chocolate4.Entities.Stats;
using UnityEngine;

namespace Chocolate4.Entities.Weapons
{
    public class Claws : Weapon
    {
        private const float _animationDuration = 15f;
        private BoxCollider boxCollider;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        protected override void AttackLogic()
        {
            StartCoroutine(
                AttackCo()
            );
        }
        private IEnumerator AttackCo()
        {
            boxCollider.enabled = true;
            yield return new WaitForSeconds(_animationDuration);
            boxCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            Hp targetHp = other.gameObject.GetComponent<Hp>();
            if (targetHp != null && other.gameObject.tag == "Player")
            {
                targetHp.Damage(Damage);
            }
        }
    }
}
