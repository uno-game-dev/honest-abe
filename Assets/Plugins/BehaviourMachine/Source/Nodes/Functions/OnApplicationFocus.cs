//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnApplicationFocus is called when the player gets or loses focus.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnApplicationFocus is called when the player gets or loses focus",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnApplicationFocus.html")]
    public class OnApplicationFocus : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onApplicationFocus += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onApplicationFocus -= OnTick;
            m_Registered = false;
        }
    }
}