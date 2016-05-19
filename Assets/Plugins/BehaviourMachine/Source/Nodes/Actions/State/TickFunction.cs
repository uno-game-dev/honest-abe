//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Call/Tick a FunctionNode in the supplied tree. The tree should be enabled and running. Returns the status of the ticked function or Error if the function could not be found.
    /// </summary>
    [NodeInfo ( category = "Action/State/",
                icon = "Function",
                description = "Call/Tick a FunctionNode in the supplied tree. The tree should be enabled and running. Returns the status of the ticked function or Error if the function could not be found")]
    public class TickFunction : ActionNode {

        /// <summary>
        /// The GameObject that has a BehaviourTree in it.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Use Self",tooltip = "The GameObject that has a BehaviourTree in it.")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The name of the target tree.
        /// </summary>
        [VariableInfo(tooltip = "The name of the target tree.")]
        public StringVar treeName;

        /// <summary>
        /// The name of the function to be ticked.
        /// </summary>
        [VariableInfo(tooltip = "The name of the function to be ticked.")]
        public StringVar functionName;

        public override void Reset () {
            gameObject = this.self;
            treeName = "BehaviourTree";
            functionName = "FunctionNode";
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null || treeName.isNone || functionName.isNone)
                return Status.Error;

            // Get all trees in the game object
            foreach (InternalBehaviourTree t in gameObject.Value.GetComponents<InternalBehaviourTree>()) {
                if (t.stateName == treeName.Value)
                    return tree.TickFunction(functionName.Value);
            }

            return Status.Error;
        }
    }
}
