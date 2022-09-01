using UnityEngine;

namespace Chocolate4.Entities
{
    [CreateAssetMenu(fileName = "EntitySettings", menuName = "Natoniewski_Shooter/EntitySettings")]
    public class EntitySettings : ScriptableObject
    {
        public float MoveSpeed;
        public bool IsAI;
    }
}