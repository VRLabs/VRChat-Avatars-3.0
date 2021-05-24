// Marker by ksivl / VRLabs 3.0 Assets https://github.com/VRLabs/VRChat-Avatars-3.0
#if UNITY_EDITOR
#pragma warning disable IDE0090 // Use 'new(...)'
using Boo.Lang;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRLabs.AV3Manager;
using Vector3 = UnityEngine.Vector3;

namespace MarkerSystem
{
    [CustomEditor(typeof(Marker))]
	class MarkerEditor : Editor
	{
		public VRCAvatarDescriptor descriptor;
		public Animator avatar;
		public System.Collections.Generic.List<string> warnings = new System.Collections.Generic.List<string>();
		public int bitCount;

		public bool leftHanded, writeDefaults, useIndexFinger, brushSize, eraserSize, localSpace;
		public int localSpaceFullBody, gestureToDraw;

		private readonly string path_defaultGesture = "Assets/VRCSDK/Examples3/Animation/Controllers/vrc_AvatarV3HandsLayer.controller";
		private readonly string path_defaultMaskL = "Assets/VRCSDK/Examples3/Animation/Masks/vrc_Hand Left.mask";
		private readonly string path_defaultMaskR = "Assets/VRCSDK/Examples3/Animation/Masks/vrc_Hand Right.mask";

		public void Reset()
		{
			if (((Marker)target).gameObject.GetComponent<VRCAvatarDescriptor>() != null)
			{
				descriptor = ((Marker)target).gameObject.GetComponent<VRCAvatarDescriptor>();
			}

			leftHanded = ((Marker)target).leftHanded;
			writeDefaults = ((Marker)target).writeDefaults;
			brushSize = ((Marker)target).brushSize;
			eraserSize = ((Marker)target).eraserSize;
			localSpace = ((Marker)target).localSpace;
			localSpaceFullBody = ((Marker)target).localSpaceFullBody;
			useIndexFinger = ((Marker)target).useIndexFinger;
			gestureToDraw = ((Marker)target).gestureToDraw;
		}

		public override void OnInspectorGUI()
		{
			GUIStyle boxStyle = new GUIStyle("box") { stretchWidth = true };
			boxStyle.normal.textColor = new GUIStyle("label").normal.textColor;
			GUIStyle titleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, richText = true };

			GUILayout.Space(8);
			EditorGUILayout.BeginHorizontal(boxStyle);
			EditorGUILayout.LabelField("<b><size=14>Marker 3.0</size></b> <size=10>by VRLabs</size>", titleStyle, GUILayout.MinHeight(20f));
			EditorGUILayout.EndHorizontal();

			if (EditorApplication.isPlaying)
			{
				if (((Marker)target).finished == false)
				{
					GUILayout.Space(8);
					EditorGUILayout.LabelField("Please exit Play Mode to use this script.");
					return;
				}
			}

			if (((Marker)target).finished == false)
			{
				GUILayout.Space(8);

				descriptor = (VRCAvatarDescriptor)EditorGUILayout.ObjectField("Avatar", descriptor, typeof(VRCAvatarDescriptor), true);
				if (descriptor != null)
                {
					avatar = descriptor.gameObject.GetComponent<Animator>();
				}
				GUILayout.Space(8);

				leftHanded = EditorGUILayout.ToggleLeft("Left-handed", leftHanded);
				writeDefaults = EditorGUILayout.ToggleLeft(new GUIContent("Write Defaults", "Check this if you are animating your avatar with Write Defaults on. Otherwise, leave unchecked."), writeDefaults);

				string[] gestureOptions = new string[]
				{
				null, "Fist", "Openhand", "Fingerpoint", "Victory", "Rock'n'Roll", "Handgun", "Thumbs up"
				};
				gestureToDraw = EditorGUILayout.Popup(new GUIContent("Gesture to draw", "Fingerpoint is recommended. Avoid Rock'n'Roll on Oculus controllers; you'll accidentally draw."), gestureToDraw, gestureOptions);

				GUILayout.Space(8);

				brushSize = EditorGUILayout.ToggleLeft("Adjustable brush size", brushSize);
				eraserSize = EditorGUILayout.ToggleLeft("Adjustable eraser size", eraserSize);
				useIndexFinger = EditorGUILayout.ToggleLeft(new GUIContent("Use index finger to draw", "By default, you draw with a shiny pen. Check this to draw with your index finger instead."), useIndexFinger);
				localSpace = EditorGUILayout.ToggleLeft(new GUIContent("Enable local space", "Check this to be able to attach your drawings to various locations on your body! If unchecked, you can only attach your drawing to your player capsule."), localSpace);

				if (localSpace)
				{
					GUIContent[] layoutOptions = { new GUIContent("Half-Body (Hips, Chest, Head, Hands)", "You can attach the drawing to your hips, chest, head, or either hand."), new GUIContent("Full-Body (Half-Body Plus Feet)", "You can also attach the drawing to your feet! (For half-body users, the drawing would follow VRChat's auto-footstep IK)") };
					GUILayout.BeginVertical("Box");
					localSpaceFullBody = GUILayout.SelectionGrid(localSpaceFullBody, layoutOptions, 1);
					GUILayout.EndVertical();
				}

				GUILayout.Space(8);

				GetBitCount();
				EditorGUILayout.LabelField("Parameter memory bits needed: " + bitCount);

				CheckRequirements();

				GUIStyle buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold };
				if (warnings.Count == 0)
				{
					if (GUILayout.Button("Generate Marker", buttonStyle))
					{
						Debug.Log("Generating Marker...");
						try
						{
							Generate();
						}
						catch (Exception e)
						{
							Debug.LogException(e);
							EditorUtility.DisplayDialog("Error Generating Marker", "Sorry, an error occured generating the Marker. If this persists, contact ksivl#4278 with your console error.", "OK");
						};
					}
				}
				else
				{
					for (int i = 0; i < warnings.Count; i++)
					{
						GUILayout.Box(warnings[i], boxStyle);
					}
					GUI.enabled = false;
					GUILayout.Button("Generate Marker", buttonStyle);
					GUI.enabled = true;
				}
			}
			else if (((Marker)target).finished == true)
			{
				GUILayout.Space(8);
				if (GUILayout.Button(new GUIContent("Adjust MarkerTarget transform", "If needed, move, rotate, or scale MarkerTarget so it's either in your hand (marker model) or at the tip of your index finger (no marker model).")))
				{
					if (((Marker)target).markerTarget.gameObject == null)
					{
						Debug.LogError("Can't find MarkerTarget! It may have been moved or deleted.");
					} 
					else
					{
						Selection.activeGameObject = ((Marker)target).markerTarget.gameObject;
					}
				}

				GUILayout.Space(8);

				GUIStyle buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold };
				if (EditorApplication.isPlaying)
				{
					GUI.enabled = false;
					GUILayout.Button("Finish Setup", buttonStyle);
					GUI.enabled = true;
				}
				else
				{
					if (GUILayout.Button("Finish Setup", buttonStyle))
					{
						if (!((Marker)target).useIndexFinger)
						{
							((Marker)target).markerModel.GetComponent<MeshRenderer>().enabled = false;  // turn off marker model
						}
						DestroyImmediate(((Marker)target).system.GetComponent<ScaleConstraint>()); // was used to scale Draw & Eraser
						DestroyImmediate((Marker)target);
						// end script
					}
				}
			}

			((Marker)target).leftHanded = leftHanded;
			((Marker)target).writeDefaults = writeDefaults;
			((Marker)target).brushSize = brushSize;
			((Marker)target).eraserSize = eraserSize;
			((Marker)target).localSpace = localSpace;
			((Marker)target).localSpaceFullBody = localSpaceFullBody;
			((Marker)target).useIndexFinger = useIndexFinger;
			((Marker)target).gestureToDraw = gestureToDraw;
		}

		public void Generate()
		{
			// Unique directory setup, named after avatar
			Directory.CreateDirectory("Assets/VRLabs/Marker/Generated/");
			AssetDatabase.Refresh();
			// Folder name cannot contain these chars
			string cleanedName = string.Join("", descriptor.name.Split('/', '?', '<', '>', '\\', ':', '*', '|', '\"'));
			string guid = AssetDatabase.CreateFolder("Assets/VRLabs/Marker/Generated", cleanedName);
			string directory = AssetDatabase.GUIDToAssetPath(guid) + "/";

			// Install layers, parameters, and menu before prefab setup
			// FX layer
			if (useIndexFinger)
			{
				AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_FX (Finger).controller", directory + "FXtemp.controller");
			}
			else
			{
				AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_FX.controller", directory + "FXtemp.controller");
			}
			AnimatorController FX = AssetDatabase.LoadAssetAtPath(directory + "FXtemp.controller", typeof(AnimatorController)) as AnimatorController;

			// remove controller layers before merging to avatar, corresponding to setup
			if (leftHanded)
			{
				RemoveLayer(FX, "M_Marker R");
			}
			else
			{
				RemoveLayer(FX, "M_Marker L");
			}

			if (!brushSize)
			{
				RemoveLayer(FX, "M_Size");
				RemoveParameter(FX, "M_Size");
			}

			if (!eraserSize)
			{
				RemoveLayer(FX, "M_EraserSize");
				RemoveParameter(FX, "M_EraserSize");
			}

			if (!localSpace)
			{
				RemoveLayer(FX, "M_Space");
				RemoveParameter(FX, "M_Space");
				RemoveLayer(FX, "M_Cull");
			}
			else
			{
				RemoveLayer(FX, "M_SpaceSimple");
				RemoveParameter(FX, "M_SpaceSimple");
				RemoveLayer(FX, "M_CullSimple");
			}

			if (writeDefaults)
			{
				AV3ManagerFunctions.SetWriteDefaults(FX);
			}
			if (gestureToDraw != 3) // uses fingerpoint by default
			{
				ChangeGestureCondition(FX, 0, gestureToDraw);
			}

			// Set parameter driver on 'Clear' state to reset local space
			AnimatorState state = FX.layers[0].stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Clear")).state;
			VRCAvatarParameterDriver driver = (VRCAvatarParameterDriver)state.behaviours[0];
			string driverParamName = localSpace ? "M_Space" : "M_SpaceSimple";
			VRC.SDKBase.VRC_AvatarParameterDriver.Parameter param = new VRC.SDKBase.VRC_AvatarParameterDriver.Parameter()
			{
				name = driverParamName,
				type = VRC.SDKBase.VRC_AvatarParameterDriver.ChangeType.Set,
				value = 0f
			};
			driver.parameters.Add(param);

			EditorUtility.SetDirty(FX);
			AV3ManagerFunctions.MergeToLayer(descriptor, FX, AV3ManagerFunctions.PlayableLayer.FX, directory);
			AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(FX)); // delete temporary FX layer

			// Gesture layer
			AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_Gesture.controller", directory + "gestureTemp.controller"); // to modify
			AnimatorController gesture = AssetDatabase.LoadAssetAtPath(directory + "gestureTemp.controller", typeof(AnimatorController)) as AnimatorController;

			if (descriptor.baseAnimationLayers[2].isDefault == true || descriptor.baseAnimationLayers[2].animatorController == null)
			{
				AssetDatabase.CopyAsset(path_defaultGesture, directory + "Gesture.controller");
				AnimatorController gestureOriginal = AssetDatabase.LoadAssetAtPath(directory + "Gesture.controller", typeof(AnimatorController)) as AnimatorController;

				descriptor.customExpressions = true;
				descriptor.baseAnimationLayers[2].isDefault = false;
				descriptor.baseAnimationLayers[2].animatorController = gestureOriginal;

				if (writeDefaults)
				{
					AV3ManagerFunctions.SetWriteDefaults(gestureOriginal);
					EditorUtility.SetDirty(gestureOriginal);
				}
			}
			
			gesture.RemoveLayer((leftHanded) ? 1 : 0);
			if (useIndexFinger)
			{   // use different hand animations
				for (int i = 0; i < 3; i++)
				{
					if (gesture.layers[0].stateMachine.states[i].state.motion.name == "M_Gesture")
					{
						gesture.layers[0].stateMachine.states[i].state.motion = AssetDatabase.LoadAssetAtPath("Assets/VRLabs/Marker/Resources/Animations/Gesture/M_Gesture (Finger).anim", typeof(AnimationClip)) as AnimationClip;
					}
					else if (gesture.layers[0].stateMachine.states[i].state.motion.name == "M_Gesture Draw")
					{
						gesture.layers[0].stateMachine.states[i].state.motion = AssetDatabase.LoadAssetAtPath("Assets/VRLabs/Marker/Resources/Animations/Gesture/M_Gesture Draw (Finger).anim", typeof(AnimationClip)) as AnimationClip;
					}
				}
			}
			if (gestureToDraw != 3)
			{
				ChangeGestureCondition(gesture, 0, gestureToDraw);
			}
			if (writeDefaults)
			{
				AV3ManagerFunctions.SetWriteDefaults(gesture, true);
			}

			EditorUtility.SetDirty(gesture);
			AV3ManagerFunctions.MergeToLayer(descriptor, gesture, AV3ManagerFunctions.PlayableLayer.Gesture, directory);
			AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(gesture)); // delete temporary gesture layer

			// layer weight control from merged layer may need index set correctly
			AnimatorController avatarGesture = (AnimatorController)descriptor.baseAnimationLayers[2].animatorController;
			for (int i = 0; i < avatarGesture.layers.Length; i++)
			{   // the controls' layer is normally 3 (AllParts, LeftHand, RightHand, >>>M_Gesture<<<)
				if (avatarGesture.layers[i].name.Contains("M_Gesture") && (i != 3))
				{   
					for (int j = 0; j < 3; j++)
					{
						if (avatarGesture.layers[i].stateMachine.states[j].state.behaviours.Length != 0)
						{
							VRCAnimatorLayerControl ctrl = (VRCAnimatorLayerControl)avatarGesture.layers[i].stateMachine.states[j].state.behaviours[0];
							ctrl.layer = i;
						}
					}
				}
			}
			
			EditorUtility.SetDirty(avatarGesture);

			// Parameters
			VRCExpressionParameters.Parameter
			p_marker = new VRCExpressionParameters.Parameter
				{ name = "M_Marker", valueType = VRCExpressionParameters.ValueType.Bool, saved = false },
			p_eraser = new VRCExpressionParameters.Parameter
				{ name = "M_Eraser", valueType = VRCExpressionParameters.ValueType.Bool, saved = false },
			p_clear = new VRCExpressionParameters.Parameter
				{ name = "M_Clear", valueType = VRCExpressionParameters.ValueType.Bool, saved = false },
			p_color = new VRCExpressionParameters.Parameter
				{ name = "M_Color", valueType = VRCExpressionParameters.ValueType.Float, saved = true };
			AV3ManagerFunctions.AddParameter(descriptor, p_marker, directory);
			AV3ManagerFunctions.AddParameter(descriptor, p_eraser, directory);
			AV3ManagerFunctions.AddParameter(descriptor, p_clear, directory);
			AV3ManagerFunctions.AddParameter(descriptor, p_color, directory);

			if (localSpace)
			{
				VRCExpressionParameters.Parameter p_space = new VRCExpressionParameters.Parameter
					{ name = "M_Space", valueType = VRCExpressionParameters.ValueType.Int, saved = false };
				AV3ManagerFunctions.AddParameter(descriptor, p_space, directory);
			}
			else
			{
				VRCExpressionParameters.Parameter p_spaceSimple = new VRCExpressionParameters.Parameter
					{ name = "M_SpaceSimple", valueType = VRCExpressionParameters.ValueType.Bool, saved = false };
				AV3ManagerFunctions.AddParameter(descriptor, p_spaceSimple, directory);
			}

			if (brushSize)
			{
				VRCExpressionParameters.Parameter p_size = new VRCExpressionParameters.Parameter
					{ name = "M_Size", valueType = VRCExpressionParameters.ValueType.Float, saved = false };
				AV3ManagerFunctions.AddParameter(descriptor, p_size, directory);
			}
			if (eraserSize)
			{
				VRCExpressionParameters.Parameter p_eraserSize = new VRCExpressionParameters.Parameter
					{ name = "M_EraserSize", valueType = VRCExpressionParameters.ValueType.Float, saved = false };
				AV3ManagerFunctions.AddParameter(descriptor, p_eraserSize, directory);
			}

			VRCExpressionParameters.Parameter p_menu = new VRCExpressionParameters.Parameter
				{ name = "M_Menu", valueType = VRCExpressionParameters.ValueType.Bool, saved = false };
			AV3ManagerFunctions.AddParameter(descriptor, p_menu, directory);

			// handle menu instancing
			AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_Menu.asset", directory + "Marker Menu.asset");
			VRCExpressionsMenu markerMenu = AssetDatabase.LoadAssetAtPath(directory + "Marker Menu.asset", typeof(VRCExpressionsMenu)) as VRCExpressionsMenu;
			
			if (!localSpace) // change from submenu to 1 toggle
			{
				VRCExpressionsMenu.Control.Parameter pm_spaceSimple = new VRCExpressionsMenu.Control.Parameter 
					{ name = "M_SpaceSimple" };
				markerMenu.controls[6].type = VRCExpressionsMenu.Control.ControlType.Toggle;
				markerMenu.controls[6].parameter = pm_spaceSimple;
				markerMenu.controls[6].subMenu = null; // or else the submenu is still there internally, SDK complains
			}
			else
			{
				AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_Menu Space.asset", directory + "Marker Space Submenu.asset");
				VRCExpressionsMenu subMenu = AssetDatabase.LoadAssetAtPath(directory + "Marker Space Submenu.asset", typeof(VRCExpressionsMenu)) as VRCExpressionsMenu;

				if (localSpaceFullBody == 0) // remove left and right foot controls
				{
					subMenu.controls.RemoveAt(7);
					subMenu.controls.RemoveAt(6);
				}
				markerMenu.controls[6].subMenu = subMenu;
				EditorUtility.SetDirty(subMenu);
			}

			if (!brushSize)
			{
				RemoveMenuControl(markerMenu, "Brush Size");
			}

			if (!eraserSize)
			{
				RemoveMenuControl(markerMenu, "Eraser Size");
			}

			EditorUtility.SetDirty(markerMenu);

			VRCExpressionsMenu.Control.Parameter pm_menu = new VRCExpressionsMenu.Control.Parameter
				{ name = "M_Menu" };
			Texture2D markerIcon = AssetDatabase.LoadAssetAtPath("Assets/VRLabs/Marker/Resources/Icons/M_Icon_Menu.png", typeof(Texture2D)) as Texture2D;
			AV3ManagerFunctions.AddSubMenu(descriptor, markerMenu, "Marker", directory, pm_menu, markerIcon);

			// setup in scene
			GameObject marker = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/VRLabs/Marker/Resources/Marker.prefab", typeof(GameObject))) as GameObject;
			if (PrefabUtility.IsPartOfPrefabInstance(marker))
				PrefabUtility.UnpackPrefabInstance(marker, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
			marker.transform.SetParent(avatar.transform, false);

			Transform system = marker.transform.Find("System");
			Transform targets = marker.transform.Find("Targets");
		    Transform markerTarget = targets.Find("MarkerTarget");
			Transform markerModel = targets.Find("Model");
			Transform eraser = system.Find("Eraser");

			// constrain cull object to avatar
			Transform cull = marker.transform.Find("Cull");
			cull.GetComponent<ParentConstraint>().SetSource(0, new ConstraintSource { sourceTransform = descriptor.transform, weight = 1f });

			if (useIndexFinger) 
			{ 
				DestroyImmediate(markerTarget.GetChild(0).gameObject); // destroy Flip
				Transform indexDistal = leftHanded ? avatar.GetBoneTransform(HumanBodyBones.LeftIndexDistal) : avatar.GetBoneTransform(HumanBodyBones.RightIndexDistal);

				 // prefer the end bone of the index finger if it exists
				if (indexDistal.Find(indexDistal.gameObject.name + "_end") != null)
				{
					markerTarget.SetParent(indexDistal.Find(indexDistal.gameObject.name + "_end"), true);
				}
				else
				{
					markerTarget.SetParent(indexDistal, false);
				}
			}
			else // using model: scale Model to target freely, and until script is destroyed, scale System to target uniformly with X-axis 
			{
				markerModel.SetParent(marker.transform); // move it out of Targets hierarchy
				if (leftHanded)
				{
					markerTarget.SetParent(avatar.GetBoneTransform(HumanBodyBones.LeftHand), true);
				}
				else
				{
					markerTarget.SetParent(avatar.GetBoneTransform(HumanBodyBones.RightHand), true);
				}
				((Marker)target).markerModel = markerModel; // to turn its mesh renderer off when script is finished
			}
			markerTarget.localPosition = Vector3.zero;
			markerTarget.localRotation = Quaternion.Euler(0f, 0f, 0f);

			if (localSpace)
			{
				targets.Find("M_LocalSpace2").SetParent(avatar.GetBoneTransform(HumanBodyBones.Hips), false);
				targets.Find("M_LocalSpace3").SetParent(avatar.GetBoneTransform(HumanBodyBones.Chest), false);

				Transform headRef = targets.Find("HeadRef"); // special setup for head bone b/c local shrink
				headRef.SetParent(avatar.GetBoneTransform(HumanBodyBones.Head), false); // get head position,
				headRef.SetParent(avatar.GetBoneTransform(HumanBodyBones.Neck), true); // but parent to neck
				headRef.GetComponent<RotationConstraint>().SetSource(0, new ConstraintSource
					{ sourceTransform = avatar.GetBoneTransform(HumanBodyBones.Head), weight = 1f} );

				targets.Find("M_LocalSpace5").SetParent(avatar.GetBoneTransform(HumanBodyBones.LeftHand), false);
				targets.Find("M_LocalSpace6").SetParent(avatar.GetBoneTransform(HumanBodyBones.RightHand), false);

				if (localSpaceFullBody == 1)
				{
					targets.Find("M_LocalSpace7").SetParent(avatar.GetBoneTransform(HumanBodyBones.LeftFoot), false);
					targets.Find("M_LocalSpace8").SetParent(avatar.GetBoneTransform(HumanBodyBones.RightFoot), false);
				}
			} 
			targets.Find("M_LocalSpace1").SetParent(avatar.transform, false);
			DestroyImmediate(targets.gameObject); // remove the "Targets" container object when finished

			// set anything not adjustable to a medium-ish amount
			if (!eraserSize) 
			{
				eraser.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
			}
			if (!brushSize)
			{
				ParticleSystem.MinMaxCurve size = new ParticleSystem.MinMaxCurve(0.024f);
				Transform draw = system.transform.Find("Draw");
				Transform preview = draw.GetChild(0);
				ParticleSystem.MainModule main = draw.GetComponent<ParticleSystem>().main;
				main.startSize = size;
				main = preview.GetComponent<ParticleSystem>().main;
				main.startSize = size;
			}

			((Marker)target).system = system;
			((Marker)target).markerTarget = markerTarget;
			((Marker)target).finished = true;
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			Debug.Log("Successfully generated Marker!");
		}

		private void CheckRequirements()
		{
			warnings.Clear();

			if (!AssetDatabase.IsValidFolder("Assets/VRLabs/Marker"))
			{
				warnings.Add("The folder at path 'Assets/VRLabs/Marker' could not be found. Make sure you are importing a Unity package and not moving the folder.");
			}

			if (descriptor == null)
			{
				warnings.Add("There is no avatar descriptor on this GameObject. Please move this script onto your avatar, or create an avatar descriptor here.");
			}
			else
			{
				if (descriptor.expressionParameters != null && descriptor.expressionParameters.CalcTotalCost() > (128 - bitCount))
				{
					warnings.Add("You don't have enough free memory in your avatar's Expression Parameters to generate. You need " + (128 - bitCount) + " or less bits of parameter memory utilized.");
				}
				if (descriptor.expressionsMenu != null)
				{
					if (descriptor.expressionsMenu.controls.Count == 8)
					{
						warnings.Add("Your avatar's topmost menu is full. Please have at least one empty control slot available.");
					}
				}

				if (AssetDatabase.LoadAssetAtPath(path_defaultGesture, typeof(AnimatorController)) as AnimatorController == null)
				{
					warnings.Add("VRCSDK Gesture controller at path '" + path_defaultGesture + "' could not be found. Please place the default Gesture controller there.");
				}
				if (AssetDatabase.LoadAssetAtPath(path_defaultMaskL, typeof(AvatarMask)) as AvatarMask == null)
				{
					warnings.Add("VRCSDK default avatar mask for the left hand at path '" + path_defaultMaskL + "' could not be found. Please place the default left hand mask there.");
				}
				if (AssetDatabase.LoadAssetAtPath(path_defaultMaskR, typeof(AvatarMask)) as AvatarMask == null)
				{
					warnings.Add("VRCSDK default avatar mask for the right hand at path '" + path_defaultMaskR + "' could not be found. Please place the default right hand mask there.");
				}

				if (!descriptor.baseAnimationLayers[2].isDefault) // check gesture layer validity
				{
					if (descriptor.baseAnimationLayers[2].animatorController != null && descriptor.baseAnimationLayers[2].animatorController.name != "")
					{
                        if (descriptor.baseAnimationLayers[2].animatorController is AnimatorController gesture)
                        {
                            if (gesture.layers[0].avatarMask == null || gesture.layers[0].avatarMask.name == "")
                            {
                                warnings.Add("The first layer of your avatar's gesture layer is missing a mask. Try setting a mask, or using a copy of the VRCSDK gesture controller, or removing the controller from your avatar descriptor.");
                            }
                        }
                        else
                        {
                            warnings.Add("The gesture layer on this avatar is not an animator controller.");
                        }
                    }
				}
				if (avatar == null)
				{
					warnings.Add("There is no Animator on this avatar. Please add an Animator component on your avatar.");
				}
				else if (avatar.avatar == null)
				{
					warnings.Add("Please add an avatar in this avatar's Animator component.");
				}
				else
				{
					if (!avatar.isHuman)
					{
						warnings.Add("Please use this script on an avatar with a humanoid rig.");
					}
					else
					{
						if (useIndexFinger && ((avatar.GetBoneTransform(HumanBodyBones.LeftIndexDistal) == null) || (avatar.GetBoneTransform(HumanBodyBones.RightIndexDistal) == null)))
						{
							warnings.Add("Your avatar rig's left and/or right index finger's 3rd bone is unmapped!");
						}
						if ((avatar.GetBoneTransform(HumanBodyBones.LeftHand) == null) || (avatar.GetBoneTransform(HumanBodyBones.RightHand) == null))
                        {
							warnings.Add("Your avatar rig's left and/or right hand is unmapped!");
						}
						if (localSpace)
						{
							if ((avatar.GetBoneTransform(HumanBodyBones.Hips) == null) || (avatar.GetBoneTransform(HumanBodyBones.Chest) == null) || (avatar.GetBoneTransform(HumanBodyBones.Head) == null) || (avatar.GetBoneTransform(HumanBodyBones.Neck) == null))
							{
								warnings.Add("Your avatar rig's hips, chest, neck, and/or head is unmapped!");
							}
							if (localSpaceFullBody == 1)
							{
								if ((avatar.GetBoneTransform(HumanBodyBones.LeftFoot) == null) || (avatar.GetBoneTransform(HumanBodyBones.RightFoot) == null))
								{
									warnings.Add("Your avatar rig's left and/or right foot is unmapped!");
								}
							}
						}
					}
				}
			}	
		}

		private int GetBitCount()
		{
			bitCount = 12; // M_Marker, M_Clear, M_Eraser, and M_Menu are bools(1+1+1+1); M_Color is a float(+8). always included
			if (brushSize) // float
			{
				bitCount += 8;
			}
			if (eraserSize) // float
			{
				bitCount += 8;
			}
			if (localSpace) // int
			{
				bitCount += 8;
			}
			else // bool
			{
				bitCount += 1;
			}
			return bitCount;
		}

		private void RemoveLayer(AnimatorController controller, string name)
		{   // helper function: remove layer by name
			for (int i = 0; i < controller.layers.Length; i++)
			{
				if (controller.layers[i].name.Equals(name))
				{
					controller.RemoveLayer(i);
					break;
				}
			}
		}

		private void RemoveParameter(AnimatorController controller, string name)
		{	// helper function: remove parameter by name
			for (int i = 0; i < controller.parameters.Length; i++)
			{
				if (controller.parameters[i].name.Equals(name))
				{
					controller.RemoveParameter(i);
					break;
				}
			}
		}

		private void RemoveMenuControl(VRCExpressionsMenu menu, string name)
		{	// helper function: remove menu control
			for (int i = 0; i < menu.controls.Count; i++)
			{
				if (menu.controls[i].name.Equals(name))
				{
					menu.controls.RemoveAt(i);
					break;
				}
			}
		}

		private void ChangeGestureCondition(AnimatorController controller, int layerToModify, int newGesture)
		{   // helper function: change gesture condition, in all transitions of 1 layer of controller
			AnimatorStateMachine stateMachine = controller.layers[layerToModify].stateMachine;
			ChildAnimatorState[] states = stateMachine.states;
			List<AnimatorStateTransition> transitions = new List<AnimatorStateTransition>();
			for (int i = 0; i < states.Length; i++)
			{
				transitions.AddRange(states[i].state.transitions);
			}
			AnimatorCondition[] conditions;
			for (int i = 0; i < transitions.Count; i++)
			{
				conditions = transitions[i].conditions;
				for (int j = 0; j < conditions.Length; j++)
				{
					if (conditions[j].parameter.Contains("Gesture"))
					{
						AnimatorCondition conditionToRemove = conditions[j];
						transitions[i].RemoveCondition(conditionToRemove);
						transitions[i].AddCondition(conditionToRemove.mode, newGesture, conditionToRemove.parameter);
						break; // in my case, only one condition per transition includes GestureLeft / GestureRight
					}
				}
			}
		}
	}
}
#pragma warning restore IDE0090 // Use 'new(...)'
#endif