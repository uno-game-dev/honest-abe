//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make a Multi-line text area where the user can edit a string.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/GUI/",
                icon = "GUIText",
                description = "Make a Multi-line text area where the user can edit a string",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI.TextArea.html")]
    public class GUITextArea : GUIRectNode {

        /// <summary>
        /// Text to edit.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Text to edit")]
        public StringVar text;

        /// <summary>
        /// The maximum length of the string. If left out, the user can type for ever and ever.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Infinity", tooltip = "The maximum length of the string. If left out, the user can type for ever and ever")]
        public IntVar maxLength;

        /// <summary>
        /// The style to use.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Default", tooltip = "The style to use")]
        public StringVar guiStyle;

        public override void Reset () {
            base.Reset();

            text = new ConcreteStringVar();
            maxLength = new ConcreteIntVar();
            guiStyle = new ConcreteStringVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || text.isNone)
                return Status.Error;

            if (maxLength.isNone) {
                if (guiStyle.isNone)
                    text.Value = GUI.TextArea(this.GetRect(), text.Value);
                else
                    text.Value = GUI.TextArea(this.GetRect(), text.Value, guiStyle.Value);
            }
            else {
                if (guiStyle.isNone)
                    text.Value = GUI.TextArea(this.GetRect(), text.Value, maxLength.Value);
                else
                    text.Value = GUI.TextArea(this.GetRect(), text.Value, maxLength.Value, guiStyle.Value);
            }

            return Status.Success;
        }

        public override void  EditorOnTick () {
            OnTick();
        }
    }
}