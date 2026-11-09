# CupkekGames Cameras

Cinemachine wrappers for CupkekGames games. Drives Cinemachine impulse-based screen shake by typed key, registered as a `ServiceProvider` so consumers resolve it via `ServiceLocator.Get<CinemachineManager>()`.

## What's inside

**Runtime** (`CupkekGames.Cameras.asmdef`)

- `CinemachineManager` — `ServiceProvider` MonoBehaviour holding a `KeyValueDatabase<string, CinemachineImpulseSource>`. Call `ShakeCamera(kind, intensity, duration)` to fire a kind-keyed impulse. `Mute()` returns a scope that turns every shake off until disposed (scopes nest; for captures). `SetShake(kind, source)` sets a kind's impulse source from code, for rigs built without the inspector (an editor sandbox, a test).
- `CinemachineScreenShakeKinds` — reserved string-kind constants (`Default`, `Critical`, `Rumble`). Games are free to add their own keys to the database.
- `Billboard` — turns a world-space panel to face the camera every LateUpdate, after Cinemachine has moved it (no jitter against a moving camera). The camera is the serialized one, else `Camera.main`, resolved once in Awake.
- `DistanceScale` — scales a world-space panel with its distance to the camera (min/max distance, min/max scale, optional parent-scale compensation). `Configure(...)` sets the values from a game's tuning asset. Same LateUpdate order and camera rule as `Billboard`.
- `ViewportPositioner` — editor-time component: places an object by viewport X/Y and distance, with size compensation.

## Dependencies

- `com.unity.cinemachine` (asmdef reference, not a UPM dep)
- `com.cupkekgames.services`, `com.cupkekgames.keyvaluedatabases`, `com.cupkekgames.fadeables`, `com.cupkekgames.addressableassets`, `com.cupkekgames.scenemanagement`, `com.cupkekgames.sequencer`, `com.cupkekgames.settings`, `com.cupkekgames.gamesave` (asmdef references — bring your own copy via the CupkekGames scoped registry)
