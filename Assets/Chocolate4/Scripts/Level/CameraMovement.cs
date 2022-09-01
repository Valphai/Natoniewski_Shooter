using UnityEngine;

namespace Chocolate4.Level
{
    [System.Serializable]
    public class CameraMovement : MonoBehaviour
    {
        [HideInInspector, SerializeField] 
        private bool canRotate = false;
        [HideInInspector, SerializeField] 
        private bool canMoveByDragMap = false;
        [HideInInspector, SerializeField] 
        private bool canZoom = false;
        [HideInInspector, SerializeField] 
        private bool canTwistZoom = false;
        [HideInInspector, SerializeField] 
        private LayerMask mouseWheelDraggable;
        [HideInInspector, SerializeField] 
        private float minZoom = -25f, maxZoom = -5f;
        [HideInInspector, SerializeField] 
        private float twistMinZoom = 50f, twistMaxZoom = 40f;
        [HideInInspector, SerializeField] 
        private float moveSpeedMinZoom = 25f, moveSpeedMaxZoom = 15f;
        [HideInInspector, SerializeField] 
        private float rotationSpeed = 50f;
        [HideInInspector, SerializeField] 
        private float screenEdgePan = 25f;
        [HideInInspector, SerializeField] 
        private bool canMoveByEdge;
        [HideInInspector, SerializeField] 
        private Transform lockedTransform;
        [HideInInspector, SerializeField, Tooltip("Can you lock the camera view on a character?")]
        private bool canLockCharacter;
        [SerializeField, Tooltip("Can you move the camera?")]
        private bool canMoveCamera;
        private float zoom = .6f;
        private Transform swivel, stick;
        private Camera cam;
        private float rotationAngle;
        public const float _YRot = 50f;
        private Vector3 dragStartPos;
        private Vector3 dragCurrentPos;
        [SerializeField] private InputSettings input;

        private void OnEnable()
        {
            cam = cam ?? Camera.main;
            swivel = transform.GetChild(0);
            stick = swivel.GetChild(0);
            
            // CharacterSelections.OnLockTransform += LockTransform;
        }
        private void OnDisable()
        {
            // CharacterSelections.OnLockTransform -= LockTransform;
        }
        private void Update()
        {
            if (canZoom)
            {
                float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
                if (zoomDelta != 0f)
                {
                    AdjustZoom(zoomDelta);
                }
            }

            if (canRotate)
            {
                float rotationDelta = Input.GetAxis("Rotation");
                if (rotationDelta != 0f)
                {
                    AdjustRotation(rotationDelta);
                }
            }

            if (canMoveCamera)
            {
                float xDelta = Input.GetAxis("Horizontal");
                float zDelta = Input.GetAxis("Vertical");
                if (xDelta != 0f || zDelta != 0f)
                {
                    AdjustPosition(xDelta, zDelta);
                }
            }
            if (canMoveByEdge)
            {
                AdjustPosition();
            }
            if (canMoveByDragMap)
            {
                AdjustPositionMouseWheel();
            }
            
            if (canLockCharacter)
            {
                LockPositionOn();
            }
        }

        private void AdjustPositionMouseWheel()
        {
            if (Input.GetKeyDown(input.WorldDrag))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mouseWheelDraggable))
                {
                    dragStartPos = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                }
            }

            if (Input.GetKey(input.WorldDrag))
            {
                Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out RaycastHit hitInfo2, Mathf.Infinity, mouseWheelDraggable))
                {
                    dragCurrentPos = new Vector3(hitInfo2.point.x, 0, hitInfo2.point.z);

                    Vector3 position = transform.localPosition;
                    position += dragStartPos - dragCurrentPos;

                    transform.localPosition = position;
                }
            }
        }
        private void LockPositionOn()
        {
            if (lockedTransform != null)
            {
                Vector3 position = lockedTransform.transform.localPosition;
                transform.localPosition = position;
            }
        }
        private void AdjustZoom (float delta) 
        {
            zoom = Mathf.Clamp01(zoom + delta);

            float distance = Mathf.Lerp(minZoom, maxZoom, zoom);
            stick.localPosition = new Vector3(0f, 0f, distance);

            if (canTwistZoom)
            {
                float angle = Mathf.Lerp(twistMinZoom, twistMaxZoom, zoom);
                swivel.localRotation = Quaternion.Euler(angle, _YRot, 0f);
            }
        }

        private void AdjustRotation(float delta)
        {
            rotationAngle += delta * rotationSpeed * Time.deltaTime;
            if (rotationAngle < 0f)
            {
                rotationAngle += 360f;
            }
            else if (rotationAngle >= 360f)
            {
                rotationAngle -= 360f;
            }
            transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
        }

        private void AdjustPosition(float xDelta, float zDelta)
        {
            lockedTransform = null;

            Quaternion angle = Quaternion.AngleAxis(_YRot, Vector3.up);
            Vector3 direction = 
                transform.localRotation * angle * new Vector3(xDelta, 0f, zDelta).normalized;
            float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));

            float distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoom) * 
                damping * Time.deltaTime;

            Vector3 position = transform.localPosition;
            position += direction * distance;

            transform.localPosition = position;
        }

        private void AdjustPosition()
        {
            Vector2 mouse = Input.mousePosition;
            float xDelta = 0;
            float zDelta = 0;
            if (mouse.x > Screen.width - screenEdgePan)
            {
                xDelta = 1;
            }
            else if (mouse.x < screenEdgePan)
            {
                xDelta = -1;
            }

            if (mouse.y > Screen.height - screenEdgePan)
            {
                zDelta = 1;
            }
            else if (mouse.y < screenEdgePan)
            {
                zDelta = -1;
            }

            if (xDelta != 0 || zDelta != 0) 
                AdjustPosition(xDelta, zDelta);
        }
    }
}