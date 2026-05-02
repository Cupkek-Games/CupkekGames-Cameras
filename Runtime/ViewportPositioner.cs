using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CupkekGames.Cameras
{
    /// <summary>
    /// Editor-time helper that positions a GameObject at viewport (X, Y) at a
    /// chosen distance from a camera, and rescales by distance so the object
    /// appears the same on-screen size at any distance from the reference
    /// point. Useful for placing world-space UI / props / decals against a
    /// designed shot without manually nudging in 3D.
    ///
    /// Body is gated to UNITY_EDITOR — runs in the inspector / scene view
    /// only, never at runtime. The class itself compiles in player builds so
    /// existing component references on prefabs / scenes are preserved; the
    /// component is inert in builds.
    /// </summary>
    public class ViewportPositioner : MonoBehaviour
    {
#if UNITY_EDITOR
        [Tooltip("Camera the viewport coordinates are projected against. Falls back to the scene view camera, then Camera.main.")]
        [SerializeField] private Camera _targetCamera;

        [Tooltip("Screen X coordinate (0-1, normalized viewport coordinates).")]
        [Range(0f, 1f)]
        [SerializeField] private float _screenX = 0.5f;

        [Tooltip("Screen Y coordinate (0-1, normalized viewport coordinates).")]
        [Range(0f, 1f)]
        [SerializeField] private float _screenY = 0.5f;

        [Tooltip("Distance from camera along the viewport ray.")]
        [SerializeField] private float _distanceFromCamera = 5f;

        [Tooltip("Reference distance for size calculation. Elements further from this distance scale up to compensate; closer elements scale down.")]
        [SerializeField] private float _referenceDistance = 5f;

        [Tooltip("Base scale at the reference distance.")]
        [SerializeField] private Vector3 _baseScale = Vector3.one;

        private void OnValidate()
        {
            if (!Application.isPlaying)
                UpdatePosition();
        }

        private void Update()
        {
            if (!Application.isPlaying)
                UpdatePosition();
        }

        private void UpdatePosition()
        {
            Camera cam = GetActiveCamera();
            if (cam == null) return;

            Ray ray = cam.ViewportPointToRay(new Vector3(_screenX, _screenY, 0));
            transform.position = ray.origin + ray.direction * _distanceFromCamera;

            float distance = Mathf.Max(_distanceFromCamera, 0.01f);
            float scaleFactor = distance / _referenceDistance;
            Vector3 desired = _baseScale * scaleFactor;
            Vector3 parentScale = transform.parent != null ? transform.parent.lossyScale : Vector3.one;
            transform.localScale = new Vector3(
                desired.x / parentScale.x,
                desired.y / parentScale.y,
                desired.z / parentScale.z);
        }

        private Camera GetActiveCamera()
        {
            if (_targetCamera != null) return _targetCamera;
            if (SceneView.lastActiveSceneView != null && SceneView.lastActiveSceneView.camera != null)
                return SceneView.lastActiveSceneView.camera;
            return Camera.main;
        }
#endif
    }
}
