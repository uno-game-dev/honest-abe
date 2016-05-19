//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the value of a Rigidbody2D's useGravity property.
    /// <summary>
    [NodeInfo(  category = "Action/Rigidbody2D/",
                icon = "Rigidbody2D",
                description = "Sets the value of a Rigidbody2D\'s useGravity property",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D-useGravity.html")]
    public class SetGravityScale : ActionNode {

        /// <summary>
        /// The game object that has a rigidbody2D.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody2D")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new value of the gravityScale property.
        /// <summary>
        [VariableInfo(tooltip = "The new value of the gravityScale property")]
        public FloatVar newGravityScale;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newGravityScale = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the rigidbody2D
            var rigidbody2D = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody2D>() : null;

            // Validate members
            if (rigidbody2D == null || newGravityScale.isNone)
                return Status.Error;

            // Set the gravityScale
            rigidbody2D.gravityScale = newGravityScale.Value;

            return Status.Success;
        }
    }
}
#endif