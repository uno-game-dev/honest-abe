//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Converts a bool variable to a GameObject.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Converts a bool variable to a GameObject")]
    public class BoolToGameObject : ActionNode {

        /// <summary>
        /// The variable to be converted.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to be converted")]
        public BoolVar boolVariable;

        /// <summary>
        /// The GameObject value to be stored if the "Bool Variable" is True.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self",tooltip = "The GameObject value to be stored if the \"Bool Variable\" is True")]
        public GameObjectVar trueValue;

        /// <summary>
        /// The GameObject value to be stored if the "Bool Variable" is False.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self",tooltip = "The GameObject value to be stored if the \"Bool Variable\" is False")]
        public GameObjectVar falseValue;

        /// <summary>
        /// Stores the GameObject value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the GameObject value")]
        public GameObjectVar storeGameObject;

        public override void Reset () {
            boolVariable = new ConcreteBoolVar();
            trueValue = new ConcreteGameObjectVar(this.self);
            falseValue = new ConcreteGameObjectVar(this.self);
            storeGameObject = new ConcreteGameObjectVar();
        }

        public override Status Update () {
            // Validate members
            if (boolVariable.isNone || storeGameObject.isNone)
                return Status.Error;

            storeGameObject.Value = boolVariable.Value ? trueValue.Value : falseValue.Value;

            return Status.Success;
        }

    }
}