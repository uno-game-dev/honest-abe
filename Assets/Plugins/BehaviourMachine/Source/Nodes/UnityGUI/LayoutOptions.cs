//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {

    /// <summary>
    /// The children GUILayout nodes will use the options in this node.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/",
                icon = "GUILayer",
                description = "The children GUILayout nodes will use the options in this node",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/LayoutOptions.html")]
    public class LayoutOptions : DecoratorNode, IGUINode {

        #region Static Methods
        static List<GUILayoutOption[]> s_Options = new List<GUILayoutOption[]>() {new GUILayoutOption[0]};

        public static GUILayoutOption[] GetCurrent () {
            return s_Options[s_Options.Count -1];
        }
        #endregion Static Methods

        /// <summary>
        /// Option passed to a control to give it an absolute width.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Option passed to a control to give it an absolute width")]
        public FloatVar width;

        /// <summary>
        /// Option passed to a control to give it an absolute height.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Option passed to a control to give it an absolute height")]
        public FloatVar height;

        /// <summary>
        /// Option passed to a control to specify a minimum width.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Option passed to a control to specify a minimum width")]
        public FloatVar minWidth;

        /// <summary>
        /// Option passed to a control to specify a maximum width.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Option passed to a control to specify a maximum width")]
        public FloatVar maxWidth;

        /// <summary>
        /// Option passed to a control to specify a minimum height.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Option passed to a control to specify a minimum height")]
        public FloatVar minHeight;

        /// <summary>
        /// Option passed to a control to specify a maximum height.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Option passed to a control to specify a maximum height")]
        public FloatVar maxHeight;

        /// <summary>
        /// Option passed to a control to allow or disallow horizontal expansion.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Option passed to a control to allow or disallow horizontal expansion")]
        public BoolVar expandWidth;

        /// <summary>
        /// Option passed to a control to allow or disallow vertical expansion.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Option passed to a control to allow or disallow vertical expansion")]
        public BoolVar expandHeight;

        public override void Reset () {
            width = new ConcreteFloatVar();
            height = new ConcreteFloatVar();
            minWidth = new ConcreteFloatVar();
            maxWidth = new ConcreteFloatVar();
            minHeight = new ConcreteFloatVar();
            maxHeight = new ConcreteFloatVar();
            expandWidth = new ConcreteBoolVar();
            expandHeight = new ConcreteBoolVar();
        }

    	public override Status Update () {
            s_Options.Add(GetOptions());

            Status currentStatus = base.Update();

            s_Options.RemoveAt(s_Options.Count -1);

            return currentStatus;
        }

        public override void EditorOnTick () {
            this.OnTick();
        }

        /// <summary>
        /// Returns the gui layout options in this node.
        /// </summary>
        public GUILayoutOption[] GetOptions () {
            var options = new List<GUILayoutOption>();

            if (!width.isNone) options.Add(GUILayout.Width(width.Value / OnGUI.scale));
            if (!height.isNone) options.Add(GUILayout.Height(height.Value / OnGUI.scale));
            if (!maxWidth.isNone) options.Add(GUILayout.MaxWidth(maxWidth.Value / OnGUI.scale));
            if (!minWidth.isNone) options.Add(GUILayout.MinWidth(minWidth.Value / OnGUI.scale));
            if (!maxHeight.isNone) options.Add(GUILayout.MaxHeight(maxHeight.Value / OnGUI.scale));
            if (!minHeight.isNone) options.Add(GUILayout.MinHeight(minHeight.Value / OnGUI.scale));
            if (!expandWidth.isNone) options.Add(GUILayout.ExpandWidth(expandWidth.Value));
            if (!expandHeight.isNone) options.Add(GUILayout.ExpandHeight(expandHeight.Value));

            return options.ToArray();
        }
    }
}