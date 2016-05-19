//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnTriggerExit is called when the game object other has stopped touching the root trigger.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnTriggerExit is called when the game object other has stopped touching the root trigger",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnTriggerExit.html")]
    public class OnTriggerExit : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onTriggerExit += TriggerExit;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onTriggerExit -= TriggerExit;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get root.OnTriggerExit events.
        /// </summary>
        void TriggerExit (Collider collider) {
            other.Value = collider.gameObject;

            // Tick children
            base.OnTick();
        }

        public override void Reset () {
            base.Reset();
            other = new ConcreteGameObjectVar();
        }
    }
}