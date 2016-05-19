//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a value in a set of materials.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a value in a set of materials")]
    public class MaterialRandom : ActionNode {

        /// <summary>
        /// The possible material values.
        /// </summary>
        [VariableInfo(tooltip = "The possible material values")]
        public MaterialVar[] materials;

        /// <summary>
        /// Store the random selected material.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected material")]
        public MaterialVar storeMaterial;

        public override void Reset () {
            materials = new MaterialVar[0];
            storeMaterial = new ConcreteMaterialVar();
        }

        public override Status Update () {
            // Validate members
            if (materials.Length == 0 || storeMaterial.isNone)
                return Status.Error;

            // Randomly selects a material
            storeMaterial.Value = materials[Random.Range(0, materials.Length)].Value;
            return Status.Success;
        }

    }
}