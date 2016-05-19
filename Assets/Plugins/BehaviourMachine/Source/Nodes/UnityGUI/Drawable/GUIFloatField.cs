//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make a single-line text field where the user can edit a float.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/GUI/",
                icon = "GUIText",
                description = "Make a single-line text field where the user can edit a float")]
    public class GUIFloatField : GUIRectNode {

        /// <summary>
        /// Float to edit.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Float to edit")]
        public FloatVar floatVar;

        /// <summary>
        /// The maximum length of the float. If left out, the user can type for ever and ever.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Infinity", tooltip = "The maximum length of the float. If left out, the user can type for ever and ever")]
        public IntVar maxLength;

        /// <summary>
        /// The style to use.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Default", tooltip = "The style to use")]
        public StringVar guiStyle;

        public override void Reset () {
            base.Reset();

            floatVar = new ConcreteFloatVar();
            maxLength = new ConcreteIntVar();
            guiStyle = new ConcreteStringVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || floatVar.isNone)
                return Status.Error;

            if (maxLength.isNone) {
                if (guiStyle.isNone)
                    floatVar.Value = float.Parse(GUI.TextField(this.GetRect(), floatVar.Value.ToString()));
                else
                    floatVar.Value = float.Parse(GUI.TextField(this.GetRect(), floatVar.Value.ToString(), guiStyle.Value));
            }
            else {
                if (guiStyle.isNone)
                    floatVar.Value = float.Parse(GUI.TextField(this.GetRect(), floatVar.Value.ToString(), maxLength.Value));
                else
                    floatVar.Value = float.Parse(GUI.TextField(this.GetRect(), floatVar.Value.ToString(), maxLength.Value, guiStyle.Value));
            }

            return Status.Success;
        }

        public override void  EditorOnTick () {
            OnTick();
        }
    }
}