# CupkekGames Cameras

Cinemachine wrappers for CupkekGames games. Drives Cinemachine impulse-based screen shake by typed key, registered as a `ServiceProvider` so consumers resolve it via `ServiceLocator.Get<CinemachineManager>()`.

## What's inside

**Runtime** (`CupkekGames.Cameras.asmdef`)

- `CinemachineManager` — `ServiceProvider` MonoBehaviour holding a `KeyValueDatabase<string, CinemachineImpulseSource>`. Call `ShakeCamera(kind, intensity, duration)` to fire a kind-keyed impulse.
- `CinemachineScreenShakeKinds` — reserved string-kind constants (`Default`, `Critical`, `Rumble`). Games are free to add their own keys to the database.

## Dependencies

- `com.unity.cinemachine` (asmdef reference, not a UPM dep)
- `com.cupkekgames.services`, `com.cupkekgames.keyvaluedatabases`, `com.cupkekgames.fadeables`, `com.cupkekgames.addressableassets`, `com.cupkekgames.scenemanagement`, `com.cupkekgames.sequencer`, `com.cupkekgames.settings`, `com.cupkekgames.gamesave` (asmdef references — bring your own copy via the CupkekGames scoped registry)
