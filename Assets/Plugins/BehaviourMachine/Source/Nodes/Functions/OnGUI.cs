//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnGUI is called for rendering and handling GUI events",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnGUI.html")]
    public class OnGUI : FunctionNode {

        #region Properties
        static float s_Scale = 1f;

        /// <summary>
        /// The scale of the GUI.
        /// Use the OnGUI's properties defaultWidth and defaultHeight.
        /// </summary>
        public static float scale {get {return s_Scale;}}
        #endregion Properties


        /// <summary>
        /// The width that the GUI was designed.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Scale", tooltip = "The width that the GUI was designed")]
        public FloatVar defaultWidth;

        /// <summary>
        /// The height that the GUI was designed.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Scale", tooltip = "The height that the GUI was designed")]
        public FloatVar defaultHeight;


        #region Constructor
        /// <summary>
        /// Class constructor.
        /// Creates the GUICallback component.
        /// </summary>
        public OnGUI () {
            if (Application.isPlaying && InternalGlobalBlackboard.Instance != null) {
                InternalGlobalBlackboard.CreateGUICallback();
            }
        }
        #endregion Constructor


        public override void Reset () {
            defaultWidth = new ConcreteFloatVar();
            defaultHeight = new ConcreteFloatVar();
        }

        public override void OnEnable () {
            if (this.enabled) {
                GUICallback.onGUI += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            GUICallback.onGUI -= OnTick;
            m_Registered = false;
        }

        public override Status Update () {
            // Scale gui
            float widthScale = defaultWidth.isNone ? 1f : Screen.width/defaultWidth.Value;
            float heightScale = defaultHeight.isNone ? 1f : Screen.height/defaultHeight.Value;
            float scale = Mathf.Min(widthScale, heightScale); // The current scale is the minimum scale dimension
            var screenRect = new Rect(0f, 0f, Screen.width/scale, Screen.height/scale);

            // Save old gui matrix
            var oldGUIMatrix = GUI.matrix;
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(scale, scale, 1f));

            // Create group and layout area
            GUI.BeginGroup(screenRect);
            GUILayout.BeginArea(screenRect);

            // Update static values
            OnGUI.s_Scale = scale;

            Status currentStatus = base.Update();

            // Update static values
            OnGUI.s_Scale = 1f;

            // Close group and layout area
            GUILayout.EndArea();
            GUI.EndGroup();

            // Restore gui matrix
            GUI.matrix = oldGUIMatrix;

            return currentStatus;
        }

        public override void EditorOnTick () {
            if (!enabled)
                return;

            // Scale gui
            float widthScale = defaultWidth.isNone ? 1f : Screen.width/defaultWidth.Value;
            float heightScale = defaultHeight.isNone ? 1f : Screen.height/defaultHeight.Value;
            float scale = Mathf.Min(widthScale, heightScale); // The current scale is the minimum scale dimension
            var screenRect = new Rect(0f, 0f, Screen.width/scale, Screen.height/scale);

            // Save old gui matrix
            var oldGUIMatrix = GUI.matrix;
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(scale, scale, 1f));

            // Create group and layout area
            GUI.BeginGroup(screenRect);
            GUILayout.BeginArea(screenRect);

            // Update static values
            OnGUI.s_Scale = scale;

            base.EditorOnTick();

            // Update static values
            OnGUI.s_Scale = 1f;

            // Close group and layout area
            GUILayout.EndArea();
            GUI.EndGroup();

            // Restore gui matrix
            GUI.matrix = oldGUIMatrix;
        }
    }
}