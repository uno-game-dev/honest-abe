//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the shader of a material.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Sets the shader of a material",
                url = "http://docs.unity3d.com/ScriptReference/Material-shader.html")]
    public class MaterialSetShader : ActionNode {

        /// <summary>
        /// The material to change the shader. If "Use Self" then the material from the renderer component of the self game object will be used.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The material to change the shader. If \"Use Self\" then the material from the renderer component of the self game object will be used.")]
        public MaterialVar material;

        /// <summary>
        /// The name of the new shader.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use \"New Shader\" instead", tooltip = "The name of the new shader")]
        public StringVar newShaderName;

        /// <summary>
        /// The new shader.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use \"New Shader Name\" instead", tooltip = "The new shader")]
        public ObjectVar newShader;

        public override void Reset () {
            material = new ConcreteMaterialVar();
            newShaderName = "Diffuse";
            newShader = new ConcreteObjectVar();
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
            if (targetMaterial == null)
                return Status.Error;

            // Validate newShaderName
            if (!newShaderName.isNone) {
                targetMaterial.shader = Shader.Find(newShaderName.Value);
            }
            // Validate newShader
            else if (!newShader.isNone && newShader.Value != null) {
                targetMaterial.shader = newShader.Value as Shader;
            }
            else
                return Status.Error;

            return Status.Success;
        }
    }
}