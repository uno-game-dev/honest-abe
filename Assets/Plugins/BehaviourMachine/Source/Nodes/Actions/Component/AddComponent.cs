//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;

namespace BehaviourMachine {

    /// <summary>
    /// Adds a component to the "Game Object" game object. Returns Error if there is no game object in "Game Object".
    /// </summary>
    [NodeInfo(  category = "Action/Component/",
                icon = "FilterByType",
                description = "Adds a component to the \"Game Object\". Returns Error if \"Game Object\" is null",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject.AddComponent.html")]
    public class AddComponent : ActionNode {

        /// <summary>
        /// The game object to add a component.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to add a component.")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new component type.
        /// </summary>
        [VariableInfo(tooltip = "The new component type")]
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
            gameObject.Value.AddComponent(componentType.Value);
            #else
            // Validate type
            System.Type type = TypeUtility.GetType(componentType.Value);
            if (type == null)
                return Status.Error;

            gameObject.Value.AddComponent(type);
            #endif


            return Status.Success;
        }
    }
}