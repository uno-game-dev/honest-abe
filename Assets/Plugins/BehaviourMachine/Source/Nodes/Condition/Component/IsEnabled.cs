//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Returns Success if the component is enabled, Failure otherwise.
    /// Returns Error if there is no game object in "Game Object" or if there is no supplied component in "Game Object".
    /// </summary>
    [NodeInfo(  category = "Condition/Component/",
                icon = "FilterByType",
                description = "Returns Success if the component is enabled, Failure otherwise. Returns Error if there is no game object in \"Game Object\" or if there is no supplied component in \"Game Object\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Behaviour-enabled.html")]
    public class IsEnabled : ActionNode {

        /// <summary>
        /// The component to be enabled.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The component to be enabled")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new value of the component.enabled property. If Toggle is selected the value of component.enabled is flipped.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "The new value of the component.enabled property. If Toggle is selected the value of component.enabled is flipped")]
        public BoolVar Value;

        /// <summary>
        /// The component type be enabled or disabled.
        /// </summary>
        [VariableInfo(tooltip = "The component type be enabled or disabled")]
        public StringVar componentType;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            Value = new ConcreteBoolVar();
            componentType = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null || componentType.isNone)
                return Status.Error;

            // Get the component
            #if UNITY_4 || UNITY_4_1 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
            var component = gameObject.Value.GetComponent(componentType.Value);
            #else
            // Validate type
            System.Type type = TypeUtility.GetType(componentType.Value);
            if (type == null)
                return Status.Error;
            var component = gameObject.Value.GetComponent(type);
            #endif


            // There is no component in the gameObject?
            if (component == null)
                return Status.Error;

            // Set enabled property
            if (component is InternalStateBehaviour) {
                // Get the value
                var state = component as InternalStateBehaviour;
                // Return enabled
                return state.enabled ? Status.Success : Status.Failure;
            }
            else if (component is Behaviour) {
                // Get the value
                var behaviour = component as Behaviour;
                // Return enabled
                return behaviour.enabled ? Status.Success : Status.Failure;
            }
            else if (component is Collider) {
                // Get the value
                var collider = component as Collider;
                // Return enabled
                return collider.enabled ? Status.Success : Status.Failure;
            }
            else if (component is Renderer) {
                // Get the value
                var renderer = component as Renderer;
                return renderer.enabled ? Status.Success : Status.Failure;
            }
            else if (component is ParticleEmitter) {
                // Get the value
                var particleEmitter = component as ParticleEmitter;
                // Return enabled
                return particleEmitter.enabled ? Status.Success : Status.Failure;
            }
            else
                return Status.Error;
        }
    }
}