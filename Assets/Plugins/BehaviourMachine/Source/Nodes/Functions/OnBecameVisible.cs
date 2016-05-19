//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnBecameVisible is called when the renderer became visible by any camera.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnBecameVisible is called when the renderer became visible by any camera",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnBecameVisible.html")]
    public class OnBecameVisible : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onBecameVisible += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onBecameVisible -= OnTick;
            m_Registered = false;
        }
    }
}