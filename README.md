<div align="center">
  <h1>VRChat Avatars 3.0 Assets</h1>
  <p>
     Assets and prefabs made for VRChat Avatars 3.0 system.
  </p>

  <a href="https://github.com/VRLabs/VRChat-Avatars-3.0/releases/latest">
    <img src="https://img.shields.io/github/v/release/VRLabs/VRChat-Avatars-3.0.svg?style=flat-square">
  </a>
  <br />
  <br />
</div>

# Downloads

Testing in Unity may require the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).

## [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage)

 A tool for managing playable layers and parameters for Avatars 3.0.

## [Damping Constraints](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/DampingConstraints.unitypackage)

Constraints with damping effects.

## [Follower](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/Follower.unitypackage)

A world space follower, driven by constraints.

## [Grab FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/GrabFX.unitypackage)

Handle an avatar object with touch.

## [Jiggle FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/JiggleFX.unitypackage)

Play an effect when the target object is jiggled or shaken.

## [Light Slash](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/LightSlash.unitypackage)

Perform consecutive slashes with motion.

<details>
  <summary>Install Notes</summary>
  
```  
Testing in Unity requires the 3.0 Emulator by Lyuma.

Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.

LightSlashFX is a synced parameter, so click the checkbox within the tool to add it to your avatar's parameter asset.

The Light Slash.prefab should go to the base of your Unity scene, which will give it base Unity scaling.

Unpack the prefab by right-clicking it and move the prefab to base of your avatar.

Expand the prefab, and locate "Light Slash/Targets". Move this object under your prop hierarchy, then reset it's position and rotation.

Targets/Motion is for motion detection. The detection direction is X-, opposite of the red arrow.  

Targets/Effect is where the slash effect will appear. Position and angle this transform until you are happy with where the effect appears.

If you need to adjust difficulty of the motion, adjust the bottom constraint source on "Light Slash/Collider". The default is .2, and .1 should be very hard to slash.

The system is disabled when you animate off the Light Slash/Collider object.
```
</details>

## [Marker](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/Marker.unitypackage)

A pen for drawing.

## [Particle Driver](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ParticleDriver.unitypackage)

A method for animating on particle death.

## [Proximity FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ProximityFX.unitypackage)

Player-local proximity effects.

## [Particle Shader](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ParticleShader.unitypackage)

 A shader for particle effects.

## [World Constraint](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/WorldConstraint.unitypackage)

A world fixed object, held in place with a constraint.
