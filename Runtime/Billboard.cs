using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CupkekGames.Cameras
{
    /// <summary>
    /// Rotates the GameObject to face a target camera every FixedUpdate.
    /// Falls back to Camera.main if no target is set. In the editor, also
    /// previews against the scene view camera while not playing so designers
    /// can verify orientation without entering play mode.
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        [Tooltip("Camera to face. Leave empty to use Camera.main.")]
        [SerializeField] private Camera _targetCamera;

        private void Awake()
        {
            if (_targetCamera == null)
                _targetCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            UpdateRotation();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!Application.isPlaying)
                UpdateRotation();
        }

        private void Update()
        {
            if (!Application.isPlaying)
                UpdateRotation();
        }
#endif

        private void UpdateRotation()
        {
            Camera cam = GetActiveCamera();
            if (cam != null)
                transform.rotation = cam.transform.rotation;
        }

        private Camera GetActiveCamera()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (_targetCamera != null) return _targetCamera;
                if (SceneView.lastActiveSceneView != null
                    && SceneView.lastActiveSceneView.camera != null)
                    return SceneView.lastActiveSceneView.camera;
            }
#endif
            return _targetCamera != null ? _targetCamera : Camera.main;
        }

        public void SetTargetCamera(Camera camera) => _targetCamera = camera;
    }
}
