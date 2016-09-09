//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Activates/Deactivates the "Game Object".
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Activates/Deactivates the \"Game Object\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject.SetActive.html")]
    public class SetActive : ActionNode {

        /// <summary>
        /// The game object to set active.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to set active")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The "New Value" of the activeSelf.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "The \"New Value\" of the activeSelf")]
        public BoolVar newValue;

    	public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newValue = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null)
                return Status.Error;

            // Set active
            gameObject.Value.SetActive(newValue.isNone ? !gameObject.Value.activeSelf : newValue.Value);

            return Status.Success;
        }
    }
}