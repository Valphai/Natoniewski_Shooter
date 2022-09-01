using UnityEngine;

namespace Chocolate4.MoveInput
{
    public class PlayerMoveInput : IMoveInput
    {
        public Vector3 Translation { get; set; }
        public Vector3? Rotation { get; set; }
        private readonly Camera cam;

        public PlayerMoveInput(Camera cam)
        {
            this.cam = cam;
        }
        public void ReadMoveInput(float speed)
        {
            float xDelta = Input.GetAxis("Horizontal");
            float zDelta = Input.GetAxis("Vertical");
            if (xDelta != 0f || zDelta != 0f)
            {
                Translation = AdjustPosition(xDelta, zDelta, speed);
            }
            Rotation = AdjustRotation();
        }
        private Vector3 AdjustPosition(
            float xDelta, float zDelta, float speed
        )
        {
            return new Vector3(xDelta, 0f, zDelta) * speed * Time.deltaTime;
        }
        private Vector3? AdjustRotation()
        {
            Ray r = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(r, out RaycastHit hitInfo))
            {
                return hitInfo.point;
            }
            return null;
        }
    }
}