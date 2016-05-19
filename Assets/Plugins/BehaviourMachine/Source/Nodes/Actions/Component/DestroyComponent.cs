//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;

namespace BehaviourMachine {

    /// <summary>
    /// Removes a component from the "Game Object".
    /// Returns Error if the "Game Object" does not have the supplied component.
    /// </summary>
    [NodeInfo(  category = "Action/Component/",
                icon = "FilterByType",
                description = "Removes a component from the \"Game Object\" game object. Returns Error if \"Game Object\" does not have the supplied component",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Object.Destroy.html")]
    public class DestroyComponent : ActionNode {

        /// <summary>
        /// The game object that owns the component to be destroyed.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that owns the component to be destroyed")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The type of the component to be destroyed.
        /// </summary>
        [VariableInfo(tooltip = "The type of the component to be destroyed")]
        public StringVar componentType;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            componentType = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null || componentType.isNone)
                return Status.Error;

            #if UNITY_4 || UNITY_4_1 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
            var component = gameObject.Value.GetComponent(componentType.Value);
            #else
            // Validate type
            System.Type type = TypeUtility.GetType(componentType.Value);
            if (type == null)
                return Status.Error;
            var component = gameObject.Value.GetComponent(type);
            #endif

            if (component == null)
                return Status.Error;
            else
                Object.Destroy(component);

            return Status.Success;
        }
    }
}