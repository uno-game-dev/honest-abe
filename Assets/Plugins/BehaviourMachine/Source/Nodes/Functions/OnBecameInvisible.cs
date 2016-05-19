//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnBecameInvisible is called when the renderer is no longer visible by any camera.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnBecameInvisible is called when the renderer is no longer visible by any camera",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnBecameInvisible.html")]
    public class OnBecameInvisible : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onBecameInvisible += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onBecameInvisible -= OnTick;
            m_Registered = false;
        }
    }
}