//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnTriggerStay is called once per frame for every Collider other that is touching the trigger",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnTriggerStay.html")]
    public class OnTriggerStay : FunctionNode {

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
                this.blackboard.onTriggerStay += TriggerStay;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onTriggerStay -= TriggerStay;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get blackboard.onTriggerStay events.
        /// </summary>
        void TriggerStay (Collider collider) {
            other.Value = collider.gameObject;

            // Tick children
            base.OnTick();
        }
    }
}