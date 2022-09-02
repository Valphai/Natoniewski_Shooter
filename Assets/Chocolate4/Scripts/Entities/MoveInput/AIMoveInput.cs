using UnityEngine;

namespace Chocolate4.Entities.MoveInput
{
    public class AIMoveInput : IMoveInput
    {
        public Vector3 Translation { get; set; }

        public void ReadMoveInput(float moveSpeed)
        {
            throw new System.NotImplementedException();
        }
    }
}