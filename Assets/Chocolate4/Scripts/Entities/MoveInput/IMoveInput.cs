using UnityEngine;

namespace Chocolate4.Entities.MoveInput
{
    public interface IMoveInput
    {
        public Vector3 Translation { get; set; }
        public void ReadMoveInput(float speed);
    }
}
