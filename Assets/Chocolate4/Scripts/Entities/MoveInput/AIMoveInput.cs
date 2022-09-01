using UnityEngine;

namespace Chocolate4.MoveInput
{
    public class AIMoveInput : IMoveInput
    {
        public Vector3 Translation { get; set; }
        public Vector3? Rotation { get; set; }

        public void ReadMoveInput(float moveSpeed)
        {
            throw new System.NotImplementedException();
        }
    }
}