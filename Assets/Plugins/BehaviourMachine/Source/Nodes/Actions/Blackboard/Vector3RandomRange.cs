//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a value in a set of Vector3s.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a value in a set of Vector3s")]
    public class Vector3RandomRange : ActionNode {

        /// <summary>
        /// The minimum value of x.
        /// </summary>
        [VariableInfo(tooltip = "The minimum value of x")]
        public FloatVar xMin;

        /// <summary>
        /// The maximun value of x.
        /// </summary>
        [VariableInfo(tooltip = "The maximun value of x")]
        public FloatVar xMax;

        /// <summary>
        /// The minimum value of y.
        /// </summary>
        [VariableInfo(tooltip = "The minimum value of y")]
        public FloatVar yMin;

        /// <summary>
        /// The maximun value of y.
        /// </summary>
        [VariableInfo(tooltip = "The maximun value of y")]
        public FloatVar yMax;

        /// <summary>
        /// The minimum value of z.
        /// </summary>
        [VariableInfo(tooltip = "The minimum value of z")]
        public FloatVar zMin;

        /// <summary>
        /// The maximun value of z.
        /// </summary>
        [VariableInfo(tooltip = "The maximun value of z")]
        public FloatVar zMax;

        /// <summary>
        /// Store the random selected Vector3.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected Vector3")]
        public Vector3Var storeVector3;

        public override void Reset () {
            xMin = 0f;
            xMax = 0f;
            yMin = 0f;
            yMax = 0f;
            zMin = 0f;
            zMax = 0f;
            storeVector3 = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Validate members
            if (storeVector3.isNone)
                return Status.Error;

            // Randomly selects a Vector3
            storeVector3.Value = new Vector3(Random.Range(xMin.Value, xMax.Value), Random.Range(yMin.Value, yMax.Value), Random.Range(zMin.Value, zMax.Value));
            return Status.Success;
        }

    }
}