using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{
    public class OrtographicDrag: MonoBehaviour, IDragHandler
    {
        private Camera _mainCamera;
        public float DistanceToCamera;

        public void OnDrag(PointerEventData eventData)
        {
            var screenPosition = new Vector3(eventData.position.x, eventData.position.y, DistanceToCamera);

            var worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
            transform.position = worldPosition;
        }
        // Use this for initialization
        private void Awake()
        {
            _mainCamera = Camera.main; //Cache the camera, micro optimization.
        }
    }
}