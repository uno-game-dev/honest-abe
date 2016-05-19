//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnMouseDown.html")]
    public class OnMouseDown : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onMouseDown += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onMouseDown -= OnTick;
            m_Registered = false;
        }
    }
}