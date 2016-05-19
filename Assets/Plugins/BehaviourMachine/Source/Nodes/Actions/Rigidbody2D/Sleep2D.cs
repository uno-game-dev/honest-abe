//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Forces a rigidbody2D to sleep at least one frame.
    /// </summary>
    [NodeInfo ( category = "Action/Rigidbody2D/",
                icon = "Rigidbody2D",
                description = "Forces a rigidbody2D to sleep at least one frame")]
    public class Sleep2D : ActionNode {

    	/// <summary>
        /// The game object that has a rigidbody2D in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody2D in it")]
        public GameObjectVar gameObject;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Get the rigidbody2D
            var rigidbody2D = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody2D>() : null;

            // Validate members
            if (rigidbody2D == null)
                return Status.Error;

            // Sleep
            rigidbody2D.Sleep();
            return Status.Success;
        }
    }
}
#endif