using UnityEngine;

namespace Chocolate4.Entities.MoveInput
{
    public class PlayerMoveInput : IMoveInput
    {
        public Vector3 Translation { get; set; }
        public void ReadMoveInput(float speed)
        {
            float xDelta = Input.GetAxis("Horizontal");
            float zDelta = Input.GetAxis("Vertical");
            Translation = AdjustPosition(xDelta, zDelta, speed);

        }
        private Vector3 AdjustPosition(
            float xDelta, float zDelta, float speed
        )
        {
            return new Vector3(xDelta, 0f, zDelta).normalized * speed;
        }
    }
}