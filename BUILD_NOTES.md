# Build notes and development plan

Unity version: Recommended Unity 2021.3 LTS or later (2022.x LTS is fine). I will target Android 13 API level. Use Android SDK & NDK compatible with the chosen Unity version.

Planned free assets to import (examples; will list exact packages & licenses when importing):
- Free character model (humanoid) from Unity Asset Store free packs
- Free weapons pack (simple gun models)
- Free terrain/props pack
- Free sound effects (gunshots, footsteps)

Development steps:
1) Import assets (free) and commit (I will mark them via Git LFS if large).
2) Create Main scene, set up NavMesh (bake), add spawn points.
3) Hook up Player prefab (CharacterController, camera), Weapon prefab (muzzle point), Enemy prefab (NavMeshAgent + AIEnemy script).
4) UI: health bar, ammo count, remaining enemies.
5) Test on Android emulator / device and iterate.

APK build: I will produce a debug-signed APK using Unity's default debug keystore and attach it in a release.
