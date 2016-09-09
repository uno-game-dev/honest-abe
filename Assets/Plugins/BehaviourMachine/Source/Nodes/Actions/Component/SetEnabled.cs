//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Enable or disable a component.
    /// Returns Error if there is no game object in "Game Object" or if there is no supplied component in "Game Object".
    /// </summary>
    [NodeInfo(  category = "Action/Component/",
                icon = "FilterByType",
                description = "Enable or disable a component. Returns Error if there is no game object in \"Game Object\" or if there is no supplied component in \"Game Object\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Behaviour-enabled.html")]
    public class SetEnabled : ActionNode {

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
                // Change enabled
                state.enabled = (Value.isNone) ? !state.enabled : Value.Value;
            }
            else if (component is Behaviour) {
                // Get the value
                var behaviour = component as Behaviour;
                // Change enabled
                behaviour.enabled = (Value.isNone) ? !behaviour.enabled : Value.Value;
            }
            else if (component is Collider) {
                // Get the value
                var collider = component as Collider;
                // Change enabled
                collider.enabled = (Value.isNone) ? !collider.enabled : Value.Value;
            }
            else if (component is Renderer) {
                // Get the value
                var renderer = component as Renderer;
                renderer.enabled = (Value.isNone) ? !renderer.enabled : Value.Value;
            }
            else if (component is ParticleEmitter) {
                // Get the value
                var particleEmitter = component as ParticleEmitter;
                // Change enabled
                particleEmitter.enabled = (Value.isNone) ? !particleEmitter.enabled : Value.Value;
            }
            else
                return Status.Error;

            return Status.Success;
        }
    }
}