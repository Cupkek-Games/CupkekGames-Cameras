using UnityEngine;

namespace CupkekGames.Cameras
{
    // What Billboard and DistanceScale share: when they run and which camera they follow.
    internal static class CameraFollowers
    {
        // After the camera rigs: Cinemachine's brain moves the camera in LateUpdate, and a
        // follower that ran before it would lag the camera by a frame.
        internal const int ExecutionOrder = 1000;

        // The serialized camera, else Camera.main, resolved once. With neither the follower
        // reports it and turns itself off: it would follow nothing.
        internal static Camera Resolve(Camera serialized, Behaviour follower)
        {
            if (serialized != null) return serialized;

            Camera main = Camera.main;
            if (main == null)
            {
                Debug.LogError($"[{follower.GetType().Name}] '{follower.name}' has no camera to follow: none is set and there is no Camera.main.", follower);
                follower.enabled = false;
            }
            return main;
        }
    }
}
