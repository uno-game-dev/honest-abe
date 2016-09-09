//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Begin a Horizontal control group.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Group/",
                icon = "GUILayer",
                description = "Begin a Horizontal control group",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.BeginHorizontal.html")]
    public class LayoutAlign : CompositeNode, IGUINode {

        /// <summary>
        /// Options for align the child.
        /// </summary>
        public enum AlignPosition {
            Left,
            Right,
            Up,
            Down,
        }

        /// <summary>
        /// The position to align the child.
        /// </summary>
        [Tooltip("The position to align the child")]
        public AlignPosition position;

        public override void Reset () {
            position = AlignPosition.Left;
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            Status currentStatus = this.status;

            switch (position) {
                case AlignPosition.Left:
                    GUILayout.BeginHorizontal(LayoutOptions.GetCurrent());
                    GUILayout.FlexibleSpace();
                    currentStatus = base.Update();
                    GUILayout.EndHorizontal();
                    break;
                case AlignPosition.Right:
                    GUILayout.BeginHorizontal(LayoutOptions.GetCurrent());
                    currentStatus = base.Update();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    break;
                case AlignPosition.Up:
                    GUILayout.BeginVertical(LayoutOptions.GetCurrent());
                    currentStatus = base.Update();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndVertical();
                    break;
                case AlignPosition.Down:
                    GUILayout.BeginVertical(LayoutOptions.GetCurrent());
                    GUILayout.FlexibleSpace();
                    currentStatus = base.Update();
                    GUILayout.EndVertical();
                    break;
            }

            return currentStatus;
        }

        public override void EditorOnTick () {
            // Is OnGUI?
            if (Event.current == null)
                return;

            switch (position) {
                case AlignPosition.Left:
                    GUILayout.BeginHorizontal(LayoutOptions.GetCurrent());
                    GUILayout.FlexibleSpace();
                    base.EditorOnTick();
                    GUILayout.EndHorizontal();
                    break;
                case AlignPosition.Right:
                    GUILayout.BeginHorizontal(LayoutOptions.GetCurrent());
                    base.EditorOnTick();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    break;
                case AlignPosition.Up:
                    GUILayout.BeginVertical(LayoutOptions.GetCurrent());
                    base.EditorOnTick();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndVertical();
                    break;
                case AlignPosition.Down:
                    GUILayout.BeginVertical(LayoutOptions.GetCurrent());
                    GUILayout.FlexibleSpace();
                    base.EditorOnTick();
                    GUILayout.EndVertical();
                    break;
            }
        }
    }
}