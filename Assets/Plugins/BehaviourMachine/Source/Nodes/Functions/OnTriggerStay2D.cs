//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnTriggerStay2D is called once per frame for every Collider other that is touching the trigger.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnTriggerStay2D is called once per frame for every Collider other that is touching the trigger",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnTriggerStay2D.html")]
    public class OnTriggerStay2D : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        public override void Reset () {
            base.Reset();
            other = new ConcreteGameObjectVar();
        }

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onTriggerStay2D += TriggerStay2D;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onTriggerStay2D -= TriggerStay2D;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get blackboard.onTriggerStay2D events.
        /// </summary>
        void TriggerStay2D (Collider2D collider2D) {
            other.Value = collider2D.gameObject;

            // Tick children
            base.OnTick();
        }
    }
}
#endif