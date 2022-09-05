using UnityEngine;

namespace Chocolate4.UI
{
    public class Crosshair : MonoBehaviour
    {
        private void OnEnable() => Cursor.visible = false;
        private void OnDisable() => Cursor.visible = true;
        private void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}