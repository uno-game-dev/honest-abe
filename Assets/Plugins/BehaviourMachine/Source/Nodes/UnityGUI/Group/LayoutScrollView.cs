//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Begin an automatically laid out scrollview.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Group/",
                icon = "GUIText",
                description = "Begin an automatically laid out scrollview",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.BeginScrollView.html")]
    public class LayoutScrollView : CompositeNode, IGUINode {

        /// <summary>
        /// The modified scrollPosition.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", tooltip = "The modified scrollPosition")]
        public Vector3Var storeScrollPosition;

        /// <summary>
        /// Optional parameter to always show the horizontal scrollbar.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optional parameter to always show the horizontal scrollbar")]
        public BoolVar alwayShowHorizontal;

        /// <summary>
        /// Optional parameter to always show the vertical scrollbar.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optional parameter to always show the vertical scrollbar")]
        public BoolVar alwayShowVertical;

        /// <summary>
        /// Optional GUIStyle to use for the horizontal scrollbar.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Default", tooltip = "Optional GUIStyle to use for the horizontal scrollbar")]
        public StringVar horizontalStyle;

        /// <summary>
        /// Optional GUIStyle to use for the vertical scrollbar.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Default", tooltip = "Optional GUIStyle to use for the vertical scrollbar")]
        public StringVar verticalStyle;

        /// <summary>
        /// The background style to use.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Default", tooltip = "The background style to use")]
        public StringVar backgroundStyle;

        public override void Reset () {
            storeScrollPosition = new ConcreteVector3Var();
            alwayShowHorizontal = new ConcreteBoolVar();
            alwayShowVertical = new ConcreteBoolVar();

            horizontalStyle = new ConcreteStringVar();
            verticalStyle = new ConcreteStringVar();
            backgroundStyle = new ConcreteStringVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            storeScrollPosition.vector2Value = GUILayout.BeginScrollView(storeScrollPosition.vector2Value, alwayShowHorizontal.Value, alwayShowVertical.Value, horizontalStyle.isNone ? GUI.skin.horizontalScrollbar : horizontalStyle.Value, verticalStyle.isNone ? GUI.skin.verticalScrollbar : verticalStyle.Value, backgroundStyle.isNone ? GUI.skin.scrollView : backgroundStyle.Value, LayoutOptions.GetCurrent());

            Status currentStatus = base.Update();

            GUILayout.EndScrollView();

            return currentStatus;
        }

        public override void EditorOnTick () {
            // Is OnGUI?
            if (Event.current == null)
                return;

            storeScrollPosition.vector2Value = GUILayout.BeginScrollView(storeScrollPosition.vector2Value, alwayShowHorizontal.Value, alwayShowVertical.Value, horizontalStyle.isNone ? GUI.skin.horizontalScrollbar : horizontalStyle.Value, verticalStyle.isNone ? GUI.skin.verticalScrollbar : verticalStyle.Value, backgroundStyle.isNone ? GUI.skin.scrollView : backgroundStyle.Value, LayoutOptions.GetCurrent());

            base.EditorOnTick();

            GUILayout.EndScrollView();
        }
    }
}