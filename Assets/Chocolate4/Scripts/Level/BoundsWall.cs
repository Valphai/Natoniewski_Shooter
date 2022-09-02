using UnityEngine;
using Chocolate4.Entities.Weapons;

namespace Chocolate4.Level
{
    public class BoundsWall : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Bullet b = other.gameObject.GetComponent<Bullet>();
            if (b != null)
            {
                b.ReturnToFactory();
            }
        }
    }
}