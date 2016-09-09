//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Change the isTrigger property of the "Game Object\'s" collider.
    /// </summary>
    [NodeInfo(  category = "Action/Collider/",
                icon = "BoxCollider",
                description = "Change the isTrigger property of the \"Game Object\'s\" collider",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Collider-isTrigger.html")]
    public class SetIsTrigger : ActionNode {

        /// <summary>
        /// A game object that has a collider.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has a collider")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The "New Value" of the isTrigger.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "The \"New Value\" of the isTrigger")]
        public BoolVar newValue;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newValue = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Get the collider
            var collider = gameObject.Value != null ? gameObject.Value.GetComponent<Collider>() : null;

            // Validate members
            if (collider == null)
                return Status.Error;

            // Set the isTrigger
            collider.isTrigger = (newValue.isNone) ? !collider.isTrigger : newValue.Value;

            return Status.Success;
        }
    }
}