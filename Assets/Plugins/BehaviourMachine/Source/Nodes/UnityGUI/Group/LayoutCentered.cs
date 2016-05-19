//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Begin a centralized group using FlexibleSpaces and the Horizontal/Vertical layouts; in that order.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Group/",
                icon = "GUILayer",
                description = "Begin a centralized group using FlexibleSpaces and the Horizontal/Vertical layouts; in that order")]
    public class LayoutCentered : CompositeNode, IGUINode {

        /// <summary>
        /// Centralize in the horizontal.
        /// </summary>
        [Tooltip("Centralize in the horizontal")]
        public bool horizontal;

        /// <summary>
        /// Centralize in the horizontal.
        /// </summary>
        [Tooltip("Centralize in the vertical")]
        public bool vertical;

        public override void Reset() {
            horizontal = true;
            vertical = true;
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            if (horizontal) {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
            }

            if (vertical) {
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
            }

            Status currentStatus = base.Update();

            if (vertical) {
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            if (horizontal) {
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            return currentStatus;
        }

        public override void EditorOnTick () {
            // Is OnGUI?
            if (Event.current == null)
                return;

            if (horizontal) {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
            }

            if (vertical) {
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
            }

            base.EditorOnTick();

            if (vertical) {
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            if (horizontal) {
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
    }
}