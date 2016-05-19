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
    [NodeInfo(  category = "UnityGUI/Drawable/Layout/",
                icon = "GUIText",
                description = "Make a text field where the user can enter a password",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GUILayout.PasswordField.html")]
    public class PasswordField : ActionNode, IGUINode {

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
                    password.Value = GUILayout.PasswordField(password.Value, maskChar, LayoutOptions.GetCurrent());
                else
                    password.Value = GUILayout.PasswordField(password.Value, maskChar, guiStyle.Value, LayoutOptions.GetCurrent());
            }
            else {
                if (guiStyle.isNone)
                    password.Value = GUILayout.PasswordField(password.Value, maskChar, maxLength.Value, LayoutOptions.GetCurrent());
                else
                    password.Value = GUILayout.PasswordField(password.Value, maskChar, maxLength.Value, guiStyle.Value, LayoutOptions.GetCurrent());
            }

            return Status.Success;
        }

        public override void  EditorOnTick () {
            OnTick();
        }
    }
}