//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Change the isTrigger property of the "Game Object\'s" collider2D.
    /// </summary>
    [NodeInfo(  category = "Action/Collider2D/",
                icon = "BoxCollider2D",
                description = "Change the isTrigger property of the \"Game Object\'s\" collider2D",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Collider2D-isTrigger.html")]
    public class SetIsTrigger2D : ActionNode {

        /// <summary>
        /// A game object that has a collider2D.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has a collider2D")]
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
            // Get the collider2D
            var collider2D = gameObject.Value != null ? gameObject.Value.GetComponent<Collider2D>() : null;

            // Validate members
            if (collider2D == null)
                return Status.Error;

            // Set the isTrigger
            collider2D.isTrigger = (newValue.isNone) ? !collider2D.isTrigger : newValue.Value;

            return Status.Success;
        }
    }
}
#endif