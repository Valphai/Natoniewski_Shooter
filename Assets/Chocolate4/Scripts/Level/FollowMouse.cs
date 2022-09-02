using UnityEngine;

namespace Chocolate4.Level
{
    public class FollowMouse : MonoBehaviour
    {
        private Camera cam;
    
        private void Awake()
        {
            cam = Camera.main;
        }
        void Update()
        {
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
    
            if (Physics.Raycast(r, out RaycastHit hitInfo))
            {
                Vector3 p = new Vector3(
                    hitInfo.point.x, 1f, hitInfo.point.z
                );
                transform.position = p;
            }
        }
    }
}
