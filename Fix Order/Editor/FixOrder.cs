using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using System.IO;
using System.Linq;
using VRC.SDK3.Avatars.Components;

namespace VRLabs.FixOrder
{
    [ExecuteInEditMode]
    public class FixOrder : Editor
    {
        static readonly int[] toPlayable = { 3, 4, 2, 1 }; // VRC_AnimatorLayerControl.BlendableLayer
        static readonly string path = "Assets/VRLabs/GeneratedAssets/";
        static readonly string tag = "Control ";
        public static int count;
        [MenuItem("VRLabs/Fix Order")]
        public static void Fix()
        {
            VRCAvatarDescriptor[] avatars = FindObjectsOfType(typeof(VRCAvatarDescriptor)) as VRCAvatarDescriptor[];
            foreach (VRCAvatarDescriptor avatar in avatars)
            {
                foreach (Animator animator in avatar.GetComponentsInChildren<Animator>(true))
                {
                    if (animator.runtimeAnimatorController == null || animator.gameObject == avatar.gameObject) { continue; }
                    AnimatorController controller = (AnimatorController)animator.runtimeAnimatorController;
                    bool hasBeenCopied = false;

                    var query = (from AnimatorControllerLayer l in controller.layers
                                    where l.name.StartsWith(tag)
                                    select l).ToArray();
                    for (int i = 0; i < query.Length; i++) //foreach (AnimatorControllerLayer l in query)
                    {
                        AnimatorControllerLayer l = query[i];
                        for (int j = 0; j < l.stateMachine.states.Length; j++)  //foreach (ChildAnimatorState state in l.stateMachine.states)
                        {
                            ChildAnimatorState state = l.stateMachine.states[j];
                            for (int k = 0; k < state.state.behaviours.Length; k++) //foreach (StateMachineBehaviour behaviour in state.state.behaviours)
                            {
                                StateMachineBehaviour behaviour = state.state.behaviours[k];
                                if (behaviour.GetType() == typeof(VRCAnimatorLayerControl))
                                {
                                    VRCAnimatorLayerControl ctrl = (VRCAnimatorLayerControl)behaviour;
                                    int layer = toPlayable[(int)ctrl.playable];
                                    if (avatar.baseAnimationLayers[layer].isDefault == true || avatar.baseAnimationLayers[layer].animatorController == null) { continue; }
                                    AnimatorController playable = (AnimatorController)avatar.baseAnimationLayers[layer].animatorController;
                                    int index = playable.layers.ToList().FindIndex(x => x.name.Equals(l.name.Substring(tag.Length)));
                                    if (index == -1) { continue; }
                                    if (ctrl.layer == index) { continue; }
                                    if (!hasBeenCopied)
                                    {
                                        controller = MakeCopy(avatar, controller);
                                        hasBeenCopied = true;
                                    }
                                    ctrl = (VRCAnimatorLayerControl)controller.layers[i].stateMachine.states[j].state.behaviours[k];
                                    ctrl.layer = index;
                                    animator.runtimeAnimatorController = controller;
                                    count++;
                                }
                            }
                        }
                    }
                }
            }
            if (count != 0)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            Debug.Log("Fixed " + count + (count == 1 ? " index" : " indices") + " on " + avatars.Length + (avatars.Length == 1 ? " avatar." : " avatars."));
            count = 0;
        }
        private static AnimatorController MakeCopy(VRCAvatarDescriptor avi, AnimatorController c)
        {
            Directory.CreateDirectory(path + avi.name);
            AssetDatabase.Refresh();
            string newPath = AssetDatabase.GenerateUniqueAssetPath(path + avi.name + "/" + c.name + ".controller");
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(c), newPath);
            c = AssetDatabase.LoadAssetAtPath(newPath, typeof(AnimatorController)) as AnimatorController;
            EditorUtility.SetDirty(c);
            return c;
        }
    }
}