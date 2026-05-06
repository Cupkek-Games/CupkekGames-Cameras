// CinemachineManager wraps com.unity.cinemachine impulse sources. The bridge
// only compiles when Cinemachine is installed (CINEMACHINE_INSTALLED define
// set by versionDefines). The rest of the Cameras package (Billboard,
// ViewportPositioner, CinemachineScreenShakeKinds string constants) builds
// independently.
#if CINEMACHINE_INSTALLED
using UnityEngine;
using CupkekGames.Services;
using Unity.Cinemachine;
using CupkekGames.KeyValueDatabases;

namespace CupkekGames.Cameras
{
    public class CinemachineManager : ServiceProvider
    {
        [SerializeField] private KeyValueDatabase<string, CinemachineImpulseSource> _screenShakes;

        public override void RegisterServices()
        {
            ServiceLocator.Register(new ServiceDescriptor(this));
        }

        public override void UnregisterServices()
        {
            ServiceLocator.Remove(this);
        }

        public void ShakeCamera(string kind, float intensity, float duration = 0.2f)
        {
            CinemachineImpulseSource source = _screenShakes.GetValue(kind);
            source.ImpulseDefinition.ImpulseDuration = duration;
            source.GenerateImpulse(intensity);
        }
    }
}
#endif
