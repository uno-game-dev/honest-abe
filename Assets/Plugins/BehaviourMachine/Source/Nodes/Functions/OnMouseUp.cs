//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnMouseUp is called when the user has released the mouse button",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnMouseUp.html")]
    public class OnMouseUp : FunctionNode {
        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onMouseUp += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onMouseUp -= OnTick;
            m_Registered = false;
        }
    }
}