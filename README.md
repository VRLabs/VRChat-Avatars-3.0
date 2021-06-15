<div align="center">
  <h1>
      VRChat Avatars 3.0 Assets. VRChat アバター3.0用アセット
  </h1>
  <p>
     Assets and prefabs made for VRChat Avatars 3.0 system. VRChatのアバター3.0用に作られたアセットやPrefab
  </p>

  <a href="https://github.com/VRLabs/VRChat-Avatars-3.0/releases/latest">
    <img src="https://img.shields.io/github/v/release/VRLabs/VRChat-Avatars-3.0.svg?style=flat-square">
  </a>
  <br />
  <br />
</div>

# Downloads ダウンロード

If you need help, our support channel is on [Discord](https://discord.gg/THCRsJc). 質問等は[Discord](https://discord.gg/THCRsJc)にて専用サポートチャンネルがあります。

Testing in Unity may require the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator). Unity内でテストプレイをする場合は[Lyuma](https://github.com/lyuma/Av3Emulator)さんの3.0エミュレーターを必要とする物もあります。

Do not mix old 2.0 and new 3.0 VRLabs folders. Delete your old VRLabs folder. 古い2.0と新しい3.0のVRLabsフォルダーを混在させないでください。古いVRLabsフォルダを削除します。

These assets follow the VRChat animation standard with [write defaults](https://hai-vr.github.io/combo-gesture-expressions-av3/writedefaults) turned off. これらのアセットは、[書き込みのデフォルト](https://hai-vr.github.io/combo-gesture-expressions-av3/writedefaults)がオフになっているVRChatアニメーション標準に準拠しています。


## [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage)

A tool for managing playable layers and parameters for Avatars 3.0. アバター3.0でプレイするレイヤーやパラメターを管理するツール。
 
<details>
  <summary>Install notes</summary>

> This tool merges animator controllers to your avatar's playable layer controllers and syncs to your avatar's expression parameters.
>
> VRCSDK3 version 2021.01.19 or later is required.
>
> Open VRLabs > Avatars 3.0 Manager from the menu bar. Place your avatar in the "Avatar" field within the opened window.
> 
> Expand the playable layer to merge on and click "Add animator to merge". Place the animator controller to merge in the "Controller" field.
> 
> A suffix is appended to a new parameter if it shares its name with an existing parameter. Modify or remove suffixes as needed.
> 
> "Merge on current" merges on the avatar's controller, while "Merge on new" merges on a copy of it. Sync parameters as needed.

</details>

</details>

<details>
  <summary>導入手順</summary>

> 近日公開。

</details>

## [Damping Constraints](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/DampingConstraints.unitypackage)

Constraints with damping effects. Damping（制動、減衰）エフェクト付きのConstraint（紐付け）。

<details>
  <summary>Install notes</summary>

> There are constraints for position and rotation. 
> 
> Replace the Cube under Damping Constraint/Container with your own objects.
>
> Damping Constraint/Container will follow Damping Constraint/Target.
>
> Within the Container constraint, the smaller the weight to the Target, the more motion will be dampened.

</details>

<details>
  <summary>導入手順</summary>

> 位置(Position)と回転(Rotation)用のConstraintがあります。
> 
> Damping Constraint/ContainerについているCubeを任意のオブジェクトと交換してください。
>
> Damping Constraint/ContainerはDamping Constraint/Targetを追尾します。
>
> ContainerのConstraint内では重さ（weight）が少なければ少ないほどモーションが減衰します。

</details>

## [Fix Order](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/FixOrder.unitypackage)

This script recalculates the indices on layer weight controls within particle death controllers, so that the correct layer in the avatar's FX controller is referenced.

<details>
  <summary>Install notes</summary>

> Open VRLabs from the menu bar. Click "Fix Order". 
> 
> All enabled avatars are evaluated. Fixed controllers are generated at "Assets/VRLabs/GeneratedAssets/" and reassigned automatically.
>
> To fix non-VRLabs particle death controllers in child Animators, name the layer containing VRC Animator Layer Control as "Control ___" where ___ is the name of the FX layer to retarget.

</details>

<details>
  <summary>導入手順</summary>
  
> 近日公開。

</details>

## [Follower](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/Follower.unitypackage)

A world space follower, driven by constraints. Constraintによってワールドスペースでついてくるfollower。

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
>
> Merge the FX controller to your own FX controller, using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool. 
> 
> The Follower.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Follower/FollowerTarget. Move this object out of the Follower hierarchy. Position the FollowerTarget where you want.
> 
> Follower/Container is where you place your objects that you want to follow.
> 
> To change the speed of the follower, you can edit the Speed.anim clips(Local and Remote) inside the Animations/Network folder.
> 
> Use the [Fix Order](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/FixOrder.unitypackage) script before uploading or testing. Open VRLabs from the menu bar. Click "Fix Order". Run it again any time the layers related to this package change index order in your FX controller. This is so particle death controllers can reference the correct layers in their VRC Animator Layer Control state behaviors.

</details>

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合は[Lyuma](https://github.com/lyuma/Av3Emulator)さん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0の[Manager tool](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage)を使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "Follower.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからPrefabごとアバターのベースに追加してください。
> 
> Prefabを開き、Follower/FollowerTargetを探し、そのオブジェクトをFollowerのヒエラルキーから抜いてください。FollowerTargetを任意の場所に移動させてください。
> 
> Follower/Containerには追尾したいオブジェクトを置いてください
> 
> Followerのスピードを変えたい場合はAnimationsのフォルダー内にあるSpeed.animのクリップを編集してください。

</details>

## [Grab FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/GrabFX.unitypackage)

Ten grabbable avatar objects. つかむことができる10個のアバターオブジェクト。

<details>
  <summary>Install notes</summary>

> There are two prefabs in this package. The prefab in the Grab FX/Resources folder should be used instead of the main one if you have "Use Auto-Footsteps for 3 and 4 point tracking" disabled, or if you only use FBT.
>  
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
>
> Merge the FX controller to your own FX controller, using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool.
>
> "LeftGrabFX" and "RightGrabFX" are synced parameters, so click the checkbox within the tool to add them to your avatar's parameter asset. If you are using only one hand, sync only that parameter.
>
> The Grab FX.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
>
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
>
> Inside the Grab FX hierarchy is a Left Hand and a Right Hand object. Move these objects under your left and right wrist, and place them about on your palms.
>
> There is a numbered series from 1 to 10. Place your item prop under a number and reset the prop's transforms. Nested under each number are some objects that you will move.
>
> The #:Default object represents your item's default transforms while not grabbed. Move #:Default anywhere in your hierarchy, and adjust it's transforms until your item is where you want it. You must move the #:Default object out of the parent number object before adjusting the transform. The same rule follows for each object under a numbered parent.
>
> The #:Contact Area object represents the place you touch to grab the item. Enable the mesh renderer component on the #:Contact Area object. The mesh renderer component is for visualization, and can be turned off or deleted after setup.
>
> Move #:Contact Area to the same location in your hierarchy as the corresponding #:Default object. Scale the #:Contact Area object, and adjust it's transforms until it covers the handle of your item.
>
> Place the #:Left object under your left wrist bone, and reset the transform. Do the same thing for the #:Right object and your right wrist bone.
>
> Select the numbered parent object for your item. There will be a parent constraint. Within the parent constraint list of sources, set the #:Default source weight to 0, and set the #:Left source weight to 1. The item should appear under your left wrist.
>
> Adjust the transforms of the #:Left object until your item appears correctly in your hand.
>
> In the list of sources for your item, set the #:Left source weight back to 0, #:Right to 1, and repeat a similar process for the #:Right object. When finished, set the source weights back to their defaults. #:Default 1, #:Left 0, #:Right 0.
>
> The item numbers will be weighted to the #:Left or #:Right transforms as the Left Hand and Right Hand transforms touch the #:Contact Area bounds. 
>
> An item is only grabbable when the Collision/#/Enable object is active. If you are using the prefab in the Resources folder, the object you should toggle is the #:Contact Area.
>
> If you wish to edit the hand-grab radius, change the start size of a numbered particle under Collision/Left or Collision/Right hierarchy.
>
> Review the onLeftGrab and onRightGrab layers that were merged into your FX controller.
>
> If you do not need one hand at all, delete the corresponding layers. If you want to prevent a certain hand from grabbing a certain item, select the Idle state and mute the "to" transition for your item number.
>
> If you want to make the prefab smaller, delete the objects you will not use.
> 
> Use the [Fix Order](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/FixOrder.unitypackage) script before uploading or testing. Open VRLabs from the menu bar. Click "Fix Order". Run it again any time the layers related to this package change index order in your FX controller. This is so particle death controllers can reference the correct layers in their VRC Animator Layer Control state behaviors.

</details>

<details>
  <summary>導入手順</summary>
  
> 近日公開。

</details>

## [Light Slash](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/LightSlash.unitypackage)

Perform consecutive slashes with motion. モーションからのスラッシュ効果が表示できます。

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
> 
> Merge the FX and Gesture controllers using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool.
> 
> "LightSlashFX" is a synced parameter, so click the checkbox within the tool to add it to your avatar's parameter asset.
> 
> The Light Slash.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Use the [Fix Order](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/FixOrder.unitypackage) script before uploading or testing. Open VRLabs from the menu bar. Click "Fix Order". Run it again any time the layers related to this package change index order in your FX controller. This is so particle death controllers can reference the correct layers in their VRC Animator Layer Control state behaviors.
> 
> You can trigger the slash in play mode(with the emulator active) by trying to disable the Light Slash/Armature/ꜱʟᴀꜱʜ object. This is useful to check if your install was successful and view the placement of your effects.
> 
> Within the Light Slash/Armature hierarchy is the Upper Arm/Lower Arm/Wrist series of objects. Each of these objects has a parent constraint with None in the list of sources. Use the corresponding arm bones within your avatar's humanoid rig to provide a valid source for each constraint. 
>
> Locate Light Slash/Weapon. You can replace Light Slash/Weapon/キューブソード with your own prop. Keep your prop in the same placement and facing the same way as the default prop. Put Light Slash/Weapon under your avatar's wrist hierarchy(Not the LightSlash/Armature/.../Wrist). Set the Weapon object transforms so the prop appears correctly in your hand.
> 
> Weapon/Motion Offset is for motion detection. The detection direction is X+. Changing the transforms will greatly affect sensitivity to motion. I recommend only changing the offset if you need to make detection more sensitive to wrist rotations, which can be done by increasing the Y position.
> 
> The main way to scale sensitivity is to edit the bottom source weight (Source name: "Sensitivity", Default value: 0.1) in the Light Slash/Armature/Collider parent constraint component. A lower value makes it more difficult to slash.
> 
> Under Light Slash/Effects are letter Containers. Place your effects within these Containers. Weapon/Effect Offset is where the effects will appear.
> 
> The system is disabled when you animate off the Light Slash/Armature/Collider/Enable object.
 
</details>

<details>
  <summary>導入手順</summary>
  
> 近日公開。

</details>

## [Marker](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/Marker.unitypackage)

A marker for drawing. お絵描き用のペン。

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
> 
> The [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) is a required dependency.
>
> Drag the Marker.cs script onto your avatar. You can customize settings for installing the marker. Some settings have tooltips for explanation.
> 
> After generating the marker, the ink and eraser emit from MarkerTarget. 
>
> Adjust the MarkerTarget transform by entering playmode with the emulator and enabling T-Pose Calibration. Enable the marker via its submenu. For the index finger setup, make sure MarkerTarget is positioned on the tip of your index finger. For the handheld pen setup, move and rotate MarkerTarget so your hand correctly holds the pen. MarkerTarget may also be freely scaled, if needed.
>
> When finished adjusting MarkerTarget, copy its transform component to paste its values outside of playmode.
>
> Click "Finish Setup" to finalize your marker and remove the script from your avatar.

</details>

<details>
  <summary>導入手順</summary>
  
> 近日公開。

</details>

## [Particle Driver](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ParticleDriver.unitypackage)

A method for animating on particle death. パーティクルによるデスアニメーション用のツール。

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
> 
> Merge the FX controller to your own FX controller, using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool.
> 
> "FX" is a synced parameter, so click the checkbox within the tool to add it to your avatar's parameter asset.
>  
> The Particle Driver.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Particle Driver/ᴩᴀʀᴛɪᴄʟᴇᴅᴇᴀᴛʜ. ᴩᴀʀᴛɪᴄʟᴇᴅᴇᴀᴛʜ is a particle that when killed will change the weight of the ᴩᴀʀᴛɪᴄʟᴇᴅᴇᴀᴛʜ sublayer using VRC Animator Layer Control. The ᴩᴀʀᴛɪᴄʟᴇᴅᴇᴀᴛʜ float parameter will change with the weight of the ᴩᴀʀᴛɪᴄʟᴇᴅᴇᴀᴛʜ sublayer.
>
> Use the [Fix Order](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/FixOrder.unitypackage) script before uploading or testing. Open VRLabs from the menu bar. Click "Fix Order". Run it again any time the layers related to this package change index order in your FX or Gesture controllers. This is so particle death controllers can reference the correct layers in their VRC Animator Layer Control state behaviors.
> 
> By default, the particle settings on ᴩᴀʀᴛɪᴄʟᴇᴅᴇᴀᴛʜ will have it die inside Particle Driver/Cube.
>
> ᴩᴀʀᴛɪᴄʟᴇᴅᴇᴀᴛʜ is constrained to ParticleTarget.
> 
> When the ᴩᴀʀᴛɪᴄʟᴇᴅᴇᴀᴛʜ parameter changes, the onParticleDeath layer in the FX controller transitions to drive the synced parameter, "FX".
>
> The handleFX layer transition based on the FX parameter.
>
> Animate what you want. This is a blank template.

</details>

<details>
  <summary>導入手順</summary>
  
> 近日公開。

</details>

## [Particle Shader](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ParticleShader.unitypackage)

A shader for particle effects. パーティクルエフェクト用のシェーダー。

## [Raycast Prefab](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/RaycastPrefab.unitypackage)

Grounder IK prefab. Requires Final IK.

<details>
  <summary>Install notes</summary>

> Add Raycast.prefab to your scene and enter play mode. Rotate Raycast/CastingTarget.

</details>

<details>
  <summary>導入手順</summary>

> 近日公開。

</details>
 
## [Spring Constraint](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/SpringConstraint.unitypackage)

A constraint with spring behavior. バネモーションの入ったConstraint。

<details>
  <summary>Install notes</summary>

> Replace the Cube under Spring Constraint/Container with your own objects.
>
> The Container will follow Spring Constraint/SpringTarget.
>
> To change the characteristics of the spring, change the position constraint values on the Spring Constraint/Motion object. 
> 
> Sources > SpringTarget (default 1.1) controls the strength of the spring. Higher values make it harder to stretch the spring. Min: 1, Max: 2
>
> Sources > Motion (default 4) dampens acceleration, the higher the value the slower Spring Constraint/Container accelerates.

</details>

<details>
  <summary>導入手順</summary>

> Spring Constraint/ContainerについているCubeを任意のオブジェクトと交換してください。
>
> ContainerはSpring Constraint/SpringTargetを追尾します。
>
> Springの調整をする場合はSpring Constraint/Motionオブジェクトのposition constraintの数値を編集してください。
> 
> Sources > SpringTarget (デフォルト値 1.1)はバネの強さをコントロールします。数値が高ければ高いほど伸びにくくなります（最小値１、最大値２）
>
> Sources > Motion (デフォルト値 4)は加速を減衰、数値が高ければ高いほどSpring Constraint/Containerの加速がゆっくりになります。

</details>


## [World Constraint](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/WorldConstraint.unitypackage)

A world fixed object, held in place with a constraint. Constraintを使ってオブジェクトをワールド固定。

<details>
  <summary>Install notes</summary>

> The world constraining method itself is 1 constraint and simple. Look at it and profit.
>
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
> 
> Merge the FX controller to your own FX controller, using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool.
> 
> "WorldFX" is a synced parameter, so click the checkbox within the tool to add it to your avatar's parameter asset.
>
> The World Constraint.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
>
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate World Constraint/ResetTarget. Move this object out of the prefab to anywhere else on your avatar.
>
> World Constraint/Container will start at and reset to ResetTarget.
>
> Replace the Cube under World Constraint/Container with your own objects.
>
> Review the handleWorldFX layer that was merged into your FX controller. Change "WorldFX" parameter to cause transitions within this layer.

</details>

<details>
  <summary>導入手順</summary>

> 1つのConstraintで完結する比較的シンプルなメソッドです。
>
> ※Unity内でテストプレイする場合は[Lyuma](https://github.com/lyuma/Av3Emulator)さん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0の[Manager tool](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage)を使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "WorldFX"は同期型のパラメターなのでアバターのパラメターに追加する場合はツール内でチェックを入れてください。
>
> "World Constraint.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
>
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Prefabを開き、World Constraint/ResetTargetを探してください。Prefab外の任意の場所（アバター内）に移動させてください。
>
> World Constraint/ContainerはResetTargetからスタート、リセットします。
>
> World Constraint/ContainerについているCubeを任意のオブジェクトと交換してください。
>
> FX Controllerに追加したhandleWorldFXのレイヤーをご確認ください。このレイヤーでトランジションを使いたい場合は"WorldFX"のパラメターを使ってください。

</details>

## [World Physics](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/WorldPhysics.unitypackage)

Some bandaids to make physics work on avatars. アバターで物理を機能させるためのいくつかの修正。

<details>
  <summary>Install notes</summary>

> This package fixes two problems that break avatar physics in VRChat. First, it fixes a collision bug by enabling collider components only outside of the mirror. Second, it uses an animated world constraint to prevent incorrect movement over the network with rigidbodies in world space. Unity physics is complex and making things work as you intend beyond these fixes is your responsibility.
>
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
> 
> Merge the FX and Gesture controllers using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool.
> 
> The World Physics.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
>
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Use the [Fix Order](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/FixOrder.unitypackage) script before uploading or testing. Open VRLabs from the menu bar. Click "Fix Order". Run it again any time the layers related to this package change index order in your Gesture controller. This is so particle death controllers can reference the correct layers in their VRC Animator Layer Control state behaviors.
>
> World Physics/Rigidbody and World Physics/Rigidbody/Collider are set up for a physics demo. A cube falls and collides with the world.
>
> If you want to see the demo work, move World Physics/RigidbodyTarget out of the World Physics hierarchy and to the base of the avatar. Lift the RigidbodyTarget position on the Y axis, so there is room for the cube to fall. Use the emulator or test in-game.
>
> Select the World Physics object. The animator component references "Fix Colliders.controller", which plays "Fix Colliders.anim" outside of the local mirror.
>
> Edit Fix Colliders to enable collider components used for physics. Collider components being used for physics should be off by default, or they'll be on in the mirror and break collision. Make sure you're animating using paths that properly reference children of the World Physics object. Your paths should not have the "World Physics" name in them. 
>
> Animate the collider game object active or inactive if you need to handle the collider in your FX layer. Don't animate the collider component or you may undo the fix.
>
> Pay attention to the way the handlePhysics layer is split by IsLocal. The "Is Kinematic" rigidbody setting on the World Physics object should be on locally, and off remotely.
>
> The "Is Kinematic" property doesn't seem to persist, so you must constantly animate this property if you want it to stay the way you animated it.
>
> Using gravity seems to have some minor local-only issues on the Y axis and with culling. Not really a big deal, hard to even notice. Doesn't happen if you don't use gravity on a given rigidbody.

</details>

<details>
  <summary>導入手順</summary>

> 近日公開。

</details>
