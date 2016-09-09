//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if the component in the vector "A" is less than "B"; Failure otherwise.
    /// </summary>
    [NodeInfo ( category = "Condition/Blackboard/",
                icon = "Blackboard",
                description = "Returns Success if the component in the vector \"A\" is less than \"B\"; Failure otherwise")]
    public class IsVector3XYZLess : ConditionNode {

        /// <summary>
        /// The first Vector3.
        /// </summary>
    	[VariableInfo(tooltip = "The first Vector3")]
        public Vector3Var a;

        /// <summary>
        /// The vector "A" component to test.
        /// </summary>
        [Tooltip("The vector \"A\" component to test")]
        public Vector3Component aComponent;

        /// <summary>
        /// The second Vector3.
        /// </summary>
        [VariableInfo(tooltip = "The second Vector3")]
        public FloatVar b;

        public override void Reset () {
            base.Reset();

            a = new ConcreteVector3Var();
            aComponent = Vector3Component.x;
            b = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (a.isNone || b.isNone)
                return Status.Error;

            // Get the A component
            float aValue = 0f;
            switch (aComponent) {
                case Vector3Component.x:
                    aValue = a.Value.x;
                    break;
                case Vector3Component.y:
                    aValue = a.Value.y;
                    break;
                case Vector3Component.z:
                    aValue = a.Value.z;
                    break;
            }

            if (aValue < b.Value) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }
    }
}
