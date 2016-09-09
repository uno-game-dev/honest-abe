//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets a named float of a material.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Sets a named float of a material",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Material.SetFloat.html")]
    public class MaterialSetFloat : ActionNode {

        /// <summary>
        /// The material to change the float. If "Use Self" then the material from the renderer component of the self game object will be used.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The material to change the float. If \"Use Self\" then the material from the renderer component of the self game object will be used.")]
        public MaterialVar material;

        /// <summary>
        /// The property name of the float to be changed.
        /// </summary>
        [VariableInfo(tooltip = "The property name of the float to be changed")]
        public StringVar propertyName;

        /// <summary>
        /// The new float value.
        /// </summary>
    	[VariableInfo(tooltip = "The new float value")]
        public FloatVar newFloat;

        public override void Reset () {
            material = new ConcreteMaterialVar();
            propertyName = new ConcreteStringVar();
            newFloat = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the target material
            Material targetMaterial = material.Value;
            if (material.isNone) {
                Renderer renderer = this.self.GetComponent<Renderer>();
                if (renderer != null) targetMaterial = renderer.material;
            }

            // Validate members
            if (targetMaterial == null || propertyName.isNone || newFloat.isNone)
                return Status.Error;

            // Set the material's float
            targetMaterial.SetFloat(propertyName.Value, newFloat.Value);
            return Status.Success;
        }
    }
}