//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnApplicationPause is called when the player pauses.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnApplicationPause is called when the player pauses",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnApplicationPause.html")]
    public class OnApplicationPause : FunctionNode {

        /// <summary>
        /// Stores the pause status.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Stores the pause status")]
        public BoolVar storePauseStatus;

        public override void Reset () {
            base.Reset();
            storePauseStatus = new ConcreteBoolVar();
        }

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onApplicationPause += OnPause;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onApplicationPause -= OnPause;
            m_Registered = false;
        }

        void OnPause (bool pauseStatus) {
            storePauseStatus.Value = pauseStatus;
            OnTick();
        }
    }
}