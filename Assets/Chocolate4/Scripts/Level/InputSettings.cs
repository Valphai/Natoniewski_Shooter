using UnityEngine;

namespace Chocolate4.Level
{
    [CreateAssetMenu(fileName = "InputSettings", menuName = "Natoniewski_Shooter/InputSettings")]
    public class InputSettings : ScriptableObject
    {
        [Tooltip("Use this key to drag the world")]
        public KeyCode WorldDrag = KeyCode.Mouse2;
    }
}
