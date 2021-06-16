#if VRC_SDK_VRCSDK3
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.ScriptableObjects;
using static VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;

namespace VRLabs.AV3Manager
{
    public class LayerOptions
    {
        // VRC Custom animation layer struct.
        public CustomAnimLayer Layer;

        // Controller used in the custom animation layer.
        public AnimatorController Controller;

        // Animators to merge to the controller.
        public AnimatorToMerge AdditionalController;

        // Parameters shown in controller.
        public List<(AnimatorControllerParameter, bool)> Parameters = new List<(AnimatorControllerParameter, bool)>();

        // Animator layer index.
        private readonly int _index;
        // Window this object is displayed on.
        private readonly AV3ManagerWindow _window;
        // Show all content
        private bool _show;
        // Show merger content
        private bool _showMerger;

        // UI display text
        private static class Content
        {
            public static GUIContent Controller = new GUIContent("Controller", "Controller used for the playable layer.");
            public static GUIContent MergeController = new GUIContent("Controller", "Controller to merge.");
            public static GUIContent CustomLayerButton = new GUIContent("Use Custom Animator Layer", "Use your own animator for this layer.");
            public static GUIContent DefaultLayerButton = new GUIContent("Use Default VRC Layer", "Use the default animator set by VRC for this layer.");
            public static GUIContent MergeBoldMini = new GUIContent("Animator to merge");
            public static GUIContent MergeCurrent = new GUIContent("Merge on current", "Merge animator to the current layer animator.");
            public static GUIContent MergeNew = new GUIContent("Merge as new", "Merge animator to a copy of the current layer animator.");
            public static GUIContent CancelMerge = new GUIContent("Cancel", "Cancel the operation.");
            public static GUIContent AddMerge = new GUIContent("Add animator to merge", "Select animator to merge to the current layer animator.");
        }

        // Constructor
        public LayerOptions(AV3ManagerWindow window, CustomAnimLayer layer, int index)
        {
            _window = window;
            Layer = layer;
            _index = index;
            if (layer.animatorController is AnimatorController controller)
            {
                Controller = controller;
                UpdateParameterList();
            }
            AdditionalController = new AnimatorToMerge(null, _window);
        }

        public int Index => _index;

        // Draws this object
        public void DrawLayerOptions()
        {
            // Header
            EditorGUILayout.BeginVertical("box");
            Rect r = EditorGUILayout.BeginHorizontal();
            _show = EditorGUILayout.Toggle(_show, EditorStyles.foldout, GUILayout.MaxWidth(15.0f));
            EditorGUILayout.LabelField(Layer.type.ToString(), EditorStyles.boldLabel);
            _show = GUI.Toggle(r, _show, GUIContent.none, new GUIStyle());
            EditorGUILayout.EndHorizontal();
            if (_show)
            {
                if (Layer.isDefault)
                {
                    // Custom layer button
                    if (GUILayout.Button(Content.CustomLayerButton))
                    {
                        Layer.isDefault = false;
                        _window.UpdateLayer(_index, Layer);
                    }
                }
                else
                {
                    // Default layer button
                    if (GUILayout.Button(Content.DefaultLayerButton))
                    {
                        Layer.isDefault = true;
                        Controller = null;
                        Layer.animatorController = null;
                        _window.UpdateLayer(_index, Layer);
                        UpdateParameterList();
                        _window.CleanupParametersList();
                        _window.RefreshWDState();
                    }
                    GUILayout.Space(10);
                    EditorGUI.BeginChangeCheck();
                    // Animator used
                    Controller = (AnimatorController)EditorGUILayout.ObjectField(Content.Controller, Controller, typeof(AnimatorController), false);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Layer.animatorController = Controller;
                        _window.UpdateLayer(_index, Layer);
                        UpdateParameterList();
                        _window.CleanupParametersList();
                        _window.RefreshWDState();
                    }

                    // Only show the list of parameters if there is a controller
                    if (Layer.animatorController != null)
                        DrawParameterList();

                    // If the merger is shown
                    if (_showMerger)
                    {
                        GUILayout.Space(10);
                        EditorGUILayout.LabelField(Content.MergeBoldMini, EditorStyles.miniBoldLabel);
                        // Controller to merge
                        GUILayout.Space(6);
                        AdditionalController.DrawMergingAnimator();

                        GUILayout.Space(10);

                        EditorGUILayout.BeginHorizontal();

                        // Buttons
                        if (GUILayout.Button(Content.MergeCurrent) && AdditionalController.Controller != null)
                        {
                            Layer.animatorController = Controller = AnimatorCloner.MergeControllers(Controller, AdditionalController.Controller, AdditionalController.GetParameterMergingDictionaty());
                            _window.UpdateLayer(_index, Layer);
                            UpdateParameterList();
                            _showMerger = false;
                            AdditionalController.Clear();
                            _window.RefreshWDState();
                        }
                        if (GUILayout.Button(Content.MergeNew) && AdditionalController.Controller != null)
                        {
                            Layer.animatorController = Controller = AnimatorCloner.MergeControllers(Controller, AdditionalController.Controller, AdditionalController.GetParameterMergingDictionaty(), true);
                            _window.UpdateLayer(_index, Layer);
                            UpdateParameterList();
                            _showMerger = false;
                            AdditionalController.Clear();
                            _window.RefreshWDState();
                        }
                        if (GUILayout.Button(Content.CancelMerge))
                        {
                            _showMerger = false;
                            AdditionalController.Clear();
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    // Merger show button.
                    else
                    {
                        GUILayout.Space(10);
                        if (GUILayout.Button(Content.AddMerge))
                            _showMerger = true;
                    }
                }
            }

            EditorGUILayout.EndVertical();
        }

        // Draws UI of the parameters of the layer animator
        public void DrawParameterList()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Syncable Parameters", EditorStyles.miniBoldLabel);
            EditorGUILayout.LabelField("Synced", EditorStyles.miniBoldLabel, GUILayout.Width(38));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(2);
            for (int i = 0; i < Parameters.Count; i++)
            {
                (AnimatorControllerParameter p, bool b) = Parameters[i];
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(p.name);
                EditorGUI.BeginChangeCheck();
                EditorGUI.BeginDisabledGroup(!((_window.UsedParameterMemory + VRCExpressionParameters.TypeCost(AV3ManagerFunctions.GetValueTypeFromAnimatorParameterType(p.type))) <= VRCExpressionParameters.MAX_PARAMETER_COST) && !b);
                b = EditorGUILayout.Toggle(b, GUILayout.Width(20));
                EditorGUI.EndDisabledGroup();
                if (EditorGUI.EndChangeCheck())
                {
                    Parameters[i] = (p, b);
                    _window.UpdateParameter(p, b);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        // Update list of parameters
        public void UpdateParameterList()
        {
            Parameters = new List<(AnimatorControllerParameter, bool)>();
            if (Controller == null) return;
            foreach (var p in Controller.parameters.Where(x => x.type == AnimatorControllerParameterType.Int || x.type == AnimatorControllerParameterType.Float || x.type == AnimatorControllerParameterType.Bool))
                if (AV3ManagerWindow.VrcParameters.Count(x => x.Equals(p.name)) <= 0)
                    Parameters.Add((p, _window.IsParameterInList(p)));
        }
    }
}

#endif