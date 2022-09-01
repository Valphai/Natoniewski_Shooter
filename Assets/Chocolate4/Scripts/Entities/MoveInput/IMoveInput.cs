using UnityEngine;

namespace Chocolate4.MoveInput
{
    public interface IMoveInput
    {
        public Vector3 Translation { get; set; }
        public Vector3? Rotation { get; set; }
        public void ReadMoveInput(float speed);
    }
}
