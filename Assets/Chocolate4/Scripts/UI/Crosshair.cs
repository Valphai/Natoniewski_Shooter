using UnityEngine;

namespace Chocolate4.UI
{
    public class Crosshair : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = false;
        }
        private void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}