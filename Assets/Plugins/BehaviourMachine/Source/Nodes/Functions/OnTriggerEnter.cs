//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnTriggerEnter is called when the game object other enters the root trigger.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnTriggerEnter is called when the game object other enters the root trigger",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnTriggerEnter.html")]
    public class OnTriggerEnter : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onTriggerEnter += TriggerEnter;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onTriggerEnter -= TriggerEnter;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get root.OnTriggerEnter events.
        /// </summary>
        void TriggerEnter (Collider collider) {
            other.Value = collider.gameObject;

            // Tick children
            this.OnTick();
        }

        public override void Reset () {
            base.Reset();
            other = new ConcreteGameObjectVar();
        }
    }
}