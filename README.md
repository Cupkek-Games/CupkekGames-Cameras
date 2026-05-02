# CupkekGames Cameras

Cinemachine wrappers for CupkekGames games. Drives Cinemachine impulse-based screen shake by typed key, registered as a `ServiceProvider` so consumers resolve it via `ServiceLocator.Get<CinemachineManager>()`.

## What's inside

**Runtime** (`CupkekGames.Cameras.asmdef`)

- `CinemachineManager` — `ServiceProvider` MonoBehaviour holding a `KeyValueDatabase<CinemachineScreenShakeType, CinemachineImpulseSource>`. Call `ShakeCamera(type, intensity, duration)` to fire a typed impulse.
- `CinemachineScreenShakeType` — enum key for the shake-type database. Add new entries per game.

## Dependencies

- `com.unity.cinemachine` (asmdef reference, not a UPM dep)
- `com.cupkekgames.services`, `com.cupkekgames.keyvaluedatabases`, `com.cupkekgames.fadeables`, `com.cupkekgames.addressableassets`, `com.cupkekgames.scenemanagement`, `com.cupkekgames.sequencer`, `com.cupkekgames.settings`, `com.cupkekgames.gamesave` (asmdef references — bring your own copy via the CupkekGames scoped registry)
