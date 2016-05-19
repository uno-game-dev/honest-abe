//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnMouseExit is called when the mouse is not any longer over the GUIElement or Collider.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnMouseExit is called when the mouse is not any longer over the GUIElement or Collider",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnMouseExit.html")]
    public class OnMouseExit : FunctionNode {
        
        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onMouseExit += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onMouseExit -= OnTick;
            m_Registered = false;
        }
    }
}