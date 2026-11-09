using System;
using UnityEngine;

namespace CupkekGames.Cameras
{
    /// <summary>
    /// Turns the GameObject to face a camera every LateUpdate, after the camera has moved, so a
    /// world-space panel never lags or jitters against a moving camera. The camera is the
    /// serialized one, else Camera.main, resolved once in Awake.
    /// </summary>
    [DefaultExecutionOrder(CameraFollowers.ExecutionOrder)]
    public class Billboard : MonoBehaviour
    {
        [Tooltip("Camera to face. Leave empty to use Camera.main, resolved once in Awake.")]
        [SerializeField] private Camera _targetCamera;

        private void Awake()
        {
            _targetCamera = CameraFollowers.Resolve(_targetCamera, this);
        }

        private void LateUpdate()
        {
            transform.rotation = _targetCamera.transform.rotation;
        }

        /// <summary>Faces another camera from now on.</summary>
        public void SetTargetCamera(Camera camera)
        {
            _targetCamera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
        }
    }
}
