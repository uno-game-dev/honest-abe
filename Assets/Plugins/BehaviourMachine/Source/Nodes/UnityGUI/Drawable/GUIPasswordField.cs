//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Make a text field where the user can enter a password.
    /// </summary>
    [NodeInfo(  category = "UnityGUI/Drawable/GUI/",
                icon = "GUIText",
                description = "Make a text field where the user can enter a password",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUI.PasswordField.html")]
    public class GUIPasswordField : GUIRectNode {

        /// <summary>
        /// Password to edit.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Password to edit")]
        public StringVar password;

        /// <summary>
        /// Character to mask the password with. Only the first char is used.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "*", tooltip = "Character to mask the password with. Only the first char is used")]
        public StringVar maskChar;

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
            password = new ConcreteStringVar();
            maxLength = new ConcreteIntVar();
            guiStyle = new ConcreteStringVar();
            maskChar = new ConcreteStringVar();
        }

    	public override Status Update () {
            // Is OnGUI?
            if (Event.current == null || password.isNone)
                return Status.Error;

            // Get mask char
            char maskChar = this.maskChar.isNone || this.maskChar.Value.Length < 1 ? '*' : this.maskChar.Value[0];

            if (maxLength.isNone) {
                if (guiStyle.isNone)
                    password.Value = GUI.PasswordField(this.GetRect(), password.Value, maskChar);
                else
                    password.Value = GUI.PasswordField(this.GetRect(), password.Value, maskChar, guiStyle.Value);
            }
            else {
                if (guiStyle.isNone)
                    password.Value = GUI.PasswordField(this.GetRect(), password.Value, maskChar, maxLength.Value);
                else
                    password.Value = GUI.PasswordField(this.GetRect(), password.Value, maskChar, maxLength.Value, guiStyle.Value);
            }

            return Status.Success;
        }

        public override void  EditorOnTick () {
            OnTick();
        }
    }
}