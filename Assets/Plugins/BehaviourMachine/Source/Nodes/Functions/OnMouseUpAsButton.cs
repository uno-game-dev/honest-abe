//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnMouseUpAsButton is only called when the mouse is released over the same GUIElement or Collider as it was pressed.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnMouseUpAsButton is only called when the mouse is released over the same GUIElement or Collider as it was pressed",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnMouseUpAsButton.html")]
    public class OnMouseUpAsButton : FunctionNode {
        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onMouseUpAsButton += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onMouseUpAsButton -= OnTick;
            m_Registered = false;
        }
    }
}