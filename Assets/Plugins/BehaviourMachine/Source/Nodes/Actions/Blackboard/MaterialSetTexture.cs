//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets a named texture of a material.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Sets a named texture of a material",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Material.SetTexture.html")]
    public class MaterialSetTexture : ActionNode {

        /// <summary>
        /// The material to change the texture. If "Use Self" then the material from the renderer component of the self game object will be used.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The material to change the texture. If \"Use Self\" then the material from the renderer component of the self game object will be used.")]
        public MaterialVar material;

        /// <summary>
        /// The property name of the texture to be changed.
        /// </summary>
        [VariableInfo(tooltip = "The property name of the texture to be changed")]
        public StringVar propertyName;

        /// <summary>
        /// The new texture value.
        /// </summary>
    	[VariableInfo(tooltip = "The new texture value")]
        public TextureVar newTexture;

        public override void Reset () {
            material = new ConcreteMaterialVar();
            propertyName = new ConcreteStringVar();
            newTexture = new ConcreteTextureVar();
        }

        public override Status Update () {
            // Get the target material
            Material targetMaterial = material.Value;
            if (material.isNone) {
                Renderer renderer = this.self.GetComponent<Renderer>();
                if (renderer != null) targetMaterial = renderer.material;
            }

            // Validate members
            if (targetMaterial == null || propertyName.isNone || newTexture.isNone)
                return Status.Error;

            // Set the material's texture
            targetMaterial.SetTexture(propertyName.Value, newTexture.Value);
            return Status.Success;
        }
    }
}