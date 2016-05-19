//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the shader of a material.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Gets the shader of a material",
                url = "http://docs.unity3d.com/ScriptReference/Material-shader.html")]
    public class MaterialGetShader : ActionNode {

        /// <summary>
        /// The material to get the shader. If "Use Self" then the material from the renderer component of the self game object will be used.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The material to get the shader. If \"Use Self\" then the material from the renderer component of the self game object will be used.")]
        public MaterialVar material;

        /// <summary>
        /// Store the shader.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the shader")]
        public ObjectVar storeShader;

        public override void Reset () {
            material = new ConcreteMaterialVar();
            storeShader = new ConcreteObjectVar();
        }

        public override Status Update () {
            // Get the target material
            Material targetMaterial = material.Value;

            // Validate members
            if (material.isNone) {
                Renderer renderer = this.self.GetComponent<Renderer>();
                if (renderer != null) 
                    targetMaterial = renderer.material;
            }

            // Validate members
            if (targetMaterial == null || storeShader.isNone)
                return Status.Error;

            // Get the shader
            storeShader.Value = targetMaterial.shader;

            return Status.Success;
        }
    }
}