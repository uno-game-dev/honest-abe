//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets a named int of a material.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Sets a named int of a material",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Material.SetInt.html")]
    public class MaterialSetInt : ActionNode {

        /// <summary>
        /// The material to change the int. If "Use Self" then the material from the renderer component of the self game object will be used.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The material to change the int. If \"Use Self\" then the material from the renderer component of the self game object will be used.")]
        public MaterialVar material;

        /// <summary>
        /// The property name of the int to be changed.
        /// </summary>
        [VariableInfo(tooltip = "The property name of the int to be changed")]
        public StringVar propertyName;

        /// <summary>
        /// The new int value.
        /// </summary>
    	[VariableInfo(tooltip = "The new int value")]
        public IntVar newInt;

        public override void Reset () {
            material = new ConcreteMaterialVar();
            propertyName = new ConcreteStringVar();
            newInt = new ConcreteIntVar();
        }

        public override Status Update () {
            // Get the target material
            Material targetMaterial = material.Value;
            if (material.isNone) {
                Renderer renderer = this.self.GetComponent<Renderer>();
                if (renderer != null) 
                    targetMaterial = renderer.material;
            }

            // Validate members
            if (targetMaterial == null || propertyName.isNone || newInt.isNone)
                return Status.Error;

            // Set the material's int
            targetMaterial.SetInt(propertyName.Value, newInt.Value);
            return Status.Success;
        }
    }
}
#endif