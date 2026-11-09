using System;
using UnityEngine;

namespace CupkekGames.Cameras
{
    /// <summary>
    /// Scales the GameObject with its distance to a camera every LateUpdate, so a world-space
    /// panel stays readable near and far: the min scale at the min distance or closer, the max
    /// scale at the max distance or farther, linear between. The factor multiplies the
    /// GameObject's own scale at Awake. With parent-scale compensation on, the parent's world
    /// scale is divided out, so the panel's size does not follow what it hangs on.
    /// The camera is the serialized one, else Camera.main, resolved once in Awake.
    /// </summary>
    [DefaultExecutionOrder(CameraFollowers.ExecutionOrder)]
    public class DistanceScale : MonoBehaviour
    {
        [Tooltip("Camera to measure from. Leave empty to use Camera.main, resolved once in Awake.")]
        [SerializeField] private Camera _targetCamera;
        [Tooltip("At this distance or closer, the min scale.")]
        [Min(0f)] [SerializeField] private float _minDistance = 10f;
        [Tooltip("At this distance or farther, the max scale.")]
        [Min(0f)] [SerializeField] private float _maxDistance = 50f;
        [Min(0f)] [SerializeField] private float _minScale = 1f;
        [Min(0f)] [SerializeField] private float _maxScale = 1f;
        [Tooltip("Divide out the parent's world scale, so the size holds whatever the GameObject hangs on.")]
        [SerializeField] private bool _compensateParentScale = true;

        private Vector3 _baseScale;

        private void Awake()
        {
            _baseScale = transform.localScale;
            _targetCamera = CameraFollowers.Resolve(_targetCamera, this);
        }

        /// <summary>Sets the distances and scales from code (a game's tuning asset).</summary>
        public void Configure(float minDistance, float maxDistance, float minScale, float maxScale)
        {
            if (minDistance < 0f || maxDistance < minDistance)
            {
                throw new ArgumentException($"[DistanceScale] the distances run from {minDistance} to {maxDistance}: they need 0 <= min <= max.");
            }
            if (minScale < 0f || maxScale < 0f)
            {
                throw new ArgumentException($"[DistanceScale] the scales {minScale} and {maxScale} cannot be negative.");
            }

            _minDistance = minDistance;
            _maxDistance = maxDistance;
            _minScale = minScale;
            _maxScale = maxScale;
        }

        /// <summary>Measures from another camera from now on.</summary>
        public void SetTargetCamera(Camera camera)
        {
            _targetCamera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
        }

        private void LateUpdate()
        {
            float distance = Vector3.Distance(_targetCamera.transform.position, transform.position);
            float factor = Mathf.Lerp(_minScale, _maxScale, Mathf.InverseLerp(_minDistance, _maxDistance, distance));

            Vector3 scale = _baseScale * factor;
            if (_compensateParentScale && transform.parent != null)
            {
                Vector3 parent = transform.parent.lossyScale;
                scale = new Vector3(scale.x / parent.x, scale.y / parent.y, scale.z / parent.z);
            }
            transform.localScale = scale;
        }
    }
}
