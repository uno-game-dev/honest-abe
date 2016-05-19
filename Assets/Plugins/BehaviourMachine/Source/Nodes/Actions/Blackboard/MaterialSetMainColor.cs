//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the main color of a material.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Sets the main color of a material",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Material-color.html")]
    public class MaterialSetMainColor : ActionNode {

        /// <summary>
        /// The material to change the color. If "Use Self" then the material from the renderer component of the self game object will be used.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The material to change the color. If \"Use Self\" then the material from the renderer component of the self game object will be used.")]
        public MaterialVar material;

        /// <summary>
        /// The new main color of the material.
        /// </summary>
    	[VariableInfo(tooltip = "The new main color of the material")]
        public ColorVar newMainColor;

        public override void Reset () {
            material = new ConcreteMaterialVar();
            newMainColor = new ConcreteColorVar();
        }

        public override Status Update () {
            // Get the target material
            Material targetMaterial = material.Value;
            if (material.isNone) {
                Renderer renderer = this.self.GetComponent<Renderer>();
                if (renderer != null) targetMaterial = renderer.material;
            }

            // Validate members
            if (targetMaterial == null || newMainColor.isNone)
                return Status.Error;

            // Set the material's main color
            targetMaterial.color = newMainColor.Value;
            return Status.Success;
        }
    }
}