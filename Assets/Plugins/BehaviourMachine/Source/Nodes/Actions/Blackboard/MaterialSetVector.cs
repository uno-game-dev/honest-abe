//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets a named vector of a material.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Sets a named vector of a material",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Material.SetVector.html")]
    public class MaterialSetVector : ActionNode {

        /// <summary>
        /// The material to change the vector. If "Use Self" then the material from the renderer component of the self game object will be used.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The material to change the vector. If \"Use Self\" then the material from the renderer component of the self game object will be used.")]
        public MaterialVar material;

        /// <summary>
        /// The property name of the vector to be changed.
        /// </summary>
        [VariableInfo(tooltip = "The property name of the vector to be changed")]
        public StringVar propertyName;

        /// <summary>
        /// The new vector value.
        /// </summary>
    	[VariableInfo(tooltip = "The new vector value")]
        public Vector3Var newVector;

        public override void Reset () {
            material = new ConcreteMaterialVar();
            propertyName = new ConcreteStringVar();
            newVector = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Get the target material
            Material targetMaterial = material.Value;
            if (material.isNone) {
                Renderer renderer = this.self.GetComponent<Renderer>();
                if (renderer != null) targetMaterial = renderer.material;
            }

            // Validate members
            if (targetMaterial == null || propertyName.isNone || newVector.isNone)
                return Status.Error;

            // Set the material's vector
            targetMaterial.SetVector(propertyName.Value, newVector.Value);
            return Status.Success;
        }
    }
}