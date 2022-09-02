using UnityEngine;
using Chocolate4.Helpers;

namespace Chocolate4.Entities.Weapons
{
    public class Rifle : Weapon
    {
        private Factory<Bullet> bulletFactory;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private Transform spawnPoint;

        private void Awake()
        {
            bulletFactory = new Factory<Bullet>(bulletPrefab);
        }
        public override void Attack()
        {
            base.Attack();
            Bullet b = bulletFactory.Get();
            b.Initialize(
                spawnPoint, bulletSpeed, Damage, this
            );
        }
        public void ReturnToFactory(Bullet b) => bulletFactory.Return(b);
    }
}
