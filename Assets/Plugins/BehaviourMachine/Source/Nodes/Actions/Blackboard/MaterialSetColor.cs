//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets a named color of a material.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Sets a named color of a material",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Material.SetColor.html")]
    public class MaterialSetColor : ActionNode {

        /// <summary>
        /// The material to change the color. If "Use Self" then the material from the renderer component of the self game object will be used.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The material to change the color. If \"Use Self\" then the material from the renderer component of the self game object will be used.")]
        public MaterialVar material;


        /// <summary>
        /// The property name of the color to be changed (eg. _Color, _SpecColor, _Emission, _ReflectColor).
        /// </summary>
        [VariableInfo(tooltip = "The property name of the color to be changed (eg. _Color, _SpecColor, _Emission, _ReflectColor)")]
        public StringVar propertyName;

        /// <summary>
        /// The new color of the material.
        /// </summary>
    	[VariableInfo(tooltip = "The new color of the material")]
        public ColorVar newColor;

        public override void Reset () {
            material = new ConcreteMaterialVar();
            propertyName = new ConcreteStringVar();
            newColor = new ConcreteColorVar();
        }

        public override Status Update () {
            // Get the target material
            Material targetMaterial = material.Value;
            if (material.isNone) {
                Renderer renderer = this.self.GetComponent<Renderer>();
                if (renderer != null) targetMaterial = renderer.material;
            }

            // Validate members
            if (targetMaterial == null || propertyName.isNone || newColor.isNone)
                return Status.Error;

            // Set the material's color
            targetMaterial.SetColor(propertyName.Value, newColor.Value);
            return Status.Success;
        }
    }
}