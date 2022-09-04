using UnityEngine;

namespace Chocolate4.Level
{
    public class FollowMouse : MonoBehaviour
    {
        private Camera cam;
        [SerializeField] private LayerMask groundLayer;
    
        private void Awake()
        {
            cam = Camera.main;
        }
        void Update()
        {
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
    
            if (Physics.Raycast(r, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
            {
                transform.position = hitInfo.point;
            }
        }
    }
}
