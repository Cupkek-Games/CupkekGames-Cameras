using System;
using UnityEngine;
using CupkekGames.Services;
using Unity.Cinemachine;
using CupkekGames.KeyValueDatabases;

namespace CupkekGames.Cameras
{
    public class CinemachineManager : ServiceProvider
    {
        [SerializeField] private KeyValueDatabase<string, CinemachineImpulseSource> _screenShakes = new();

        private int _mutes;

        /// <summary>True while any <see cref="Mute"/> scope is open: <see cref="ShakeCamera"/> does nothing.</summary>
        public bool IsMuted => _mutes > 0;

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
            if (_mutes > 0) return;
            CinemachineImpulseSource source = _screenShakes.GetValue(kind);
            source.ImpulseDefinition.ImpulseDuration = duration;
            source.GenerateImpulse(intensity);
        }

        /// <summary>
        /// Every shake off until the returned scope is disposed (a capture's still shot). Scopes nest:
        /// the shakes come back when the last one closes.
        /// </summary>
        public IDisposable Mute()
        {
            _mutes++;
            return new MuteScope(this);
        }

        /// <summary>
        /// Sets the impulse source a shake kind fires, for rigs built in code (an editor sandbox, a test)
        /// instead of authored in the inspector.
        /// </summary>
        public void SetShake(string kind, CinemachineImpulseSource source)
        {
            if (string.IsNullOrEmpty(kind)) throw new ArgumentException("[CinemachineManager] a shake needs a kind.", nameof(kind));
            if (source == null) throw new ArgumentNullException(nameof(source));
            _screenShakes.SetValue(kind, source);
        }

        private sealed class MuteScope : IDisposable
        {
            private CinemachineManager _manager;

            public MuteScope(CinemachineManager manager) => _manager = manager;

            public void Dispose()
            {
                if (_manager == null) return;
                _manager._mutes--;
                _manager = null;
            }
        }
    }
}
