//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnTriggerEnter2D is called when another object enters a trigger collider attached to this object (2D physics only).
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnTriggerEnter2D is called when another object enters a trigger collider attached to this object (2D physics only)",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html")]
    public class OnTriggerEnter2D : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onTriggerEnter2D += TriggerEnter2D;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onTriggerEnter2D -= TriggerEnter2D;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get blackboard.onTriggerEnter2D events.
        /// </summary>
        void TriggerEnter2D (Collider2D collider2D) {
            other.Value = collider2D.gameObject;

            // Tick children
            this.OnTick();
        }

        public override void Reset () {
            base.Reset();
            other = new ConcreteGameObjectVar();
        }
    }
}
#endif