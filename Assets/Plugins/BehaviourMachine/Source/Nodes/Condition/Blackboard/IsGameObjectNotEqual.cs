//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if "A" is not equal to "B"; returns Failure otherwise.
    /// </summary>
    [NodeInfo ( category = "Condition/Blackboard/",
                icon = "GameObject",
                description = "Returns Success if \"A\" is not equal to \"B\"; returns Failure otherwise")]
    public class IsGameObjectNotEqual : ConditionNode {

        /// <summary>
        /// The first game object.
        /// </summary>
    	[VariableInfo(tooltip = "The first game object")]
        public GameObjectVar a;

        /// <summary>
        /// The second game object.
        /// </summary>
        [VariableInfo(tooltip = "The second game object")]
        public GameObjectVar b;

        public override void Reset () {
            base.Reset();

            a = new ConcreteGameObjectVar(this.self);
            b = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Validate members
            if (a.isNone || b.isNone)
                return Status.Error;

            if (a.Value != b.Value) {
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
