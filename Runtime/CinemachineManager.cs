using UnityEngine;
using CupkekGames.AddressableAssets;
using CupkekGames.SceneManagement;
using CupkekGames.Sequencer;
using CupkekGames.Services;
using CupkekGames.Settings;
using CupkekGames.GameSave;
using Unity.Cinemachine;
using CupkekGames.KeyValueDatabases;

namespace CupkekGames.Cameras
{
    public class CinemachineManager : ServiceProvider
    {
        [SerializeField] private KeyValueDatabase<CinemachineScreenShakeType, CinemachineImpulseSource> _screenShakes;
        public override void RegisterServices()
        {
            ServiceLocator.Register(new ServiceDescriptor(this));
        }

        public override void UnregisterServices()
        {
            ServiceLocator.Remove(this);
        }

        public void ShakeCamera(CinemachineScreenShakeType type, float intensity, float duration = 0.2f)
        {
            _screenShakes.GetValue(type).ImpulseDefinition.ImpulseDuration = duration;
            _screenShakes.GetValue(type).GenerateImpulse(intensity);
        }
    }
}