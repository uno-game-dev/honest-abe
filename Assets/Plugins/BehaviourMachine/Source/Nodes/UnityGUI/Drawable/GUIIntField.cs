//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make a single-line text field where the user can edit an int.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/GUI/",
                icon = "GUIText",
                description = "Make a single-line text field where the user can edit an int")]
    public class GUIIntField : GUIRectNode {

        /// <summary>
        /// Int to edit.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Int to edit")]
        public IntVar intVar;

        /// <summary>
        /// The maximum length of the int. If left out, the user can type for ever and ever.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Infinity", tooltip = "The maximum length of the int. If left out, the user can type for ever and ever")]
        public IntVar maxLength;

        /// <summary>
        /// The style to use.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Default", tooltip = "The style to use")]
        public StringVar guiStyle;

        public override void Reset () {
            base.Reset();
            intVar = new ConcreteIntVar();
            maxLength = new ConcreteIntVar();
            guiStyle = new ConcreteStringVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || intVar.isNone)
                return Status.Error;

            if (maxLength.isNone) {
                if (guiStyle.isNone)
                    intVar.Value = int.Parse(GUI.TextField(this.GetRect(), intVar.Value.ToString()));
                else
                    intVar.Value = int.Parse(GUI.TextField(this.GetRect(), intVar.Value.ToString(), guiStyle.Value));
            }
            else {
                if (guiStyle.isNone)
                    intVar.Value = int.Parse(GUI.TextField(this.GetRect(), intVar.Value.ToString(), maxLength.Value));
                else
                    intVar.Value = int.Parse(GUI.TextField(this.GetRect(), intVar.Value.ToString(), maxLength.Value, guiStyle.Value));
            }

            return Status.Success;
        }

        public override void  EditorOnTick () {
            OnTick();
        }
    }
}