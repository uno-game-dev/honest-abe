//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnJointBreak is called when a joint attached to the same game object broke.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnJointBreak is called when a joint attached to the same game object broke",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnJointBreak.html")]
    public class OnJointBreak : FunctionNode {

        /// <summary>
        /// Stores the break force.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Stores the break force")]
        public FloatVar storeBreakForce;

        public override void Reset () {
            base.Reset();
            storeBreakForce = new ConcreteFloatVar();
        }

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onJointBreak += OnBreak;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onJointBreak -= OnBreak;
            m_Registered = false;
        }

        void OnBreak (float breakForce) {
            storeBreakForce.Value = breakForce;
            OnTick();
        }
    }
}