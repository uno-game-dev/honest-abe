//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnMouseEnter is called when the mouse entered the GUIElement or Collider.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnMouseEnter is called when the mouse entered the GUIElement or Collider",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnMouseEnter.html")]
    public class OnMouseEnter : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onMouseEnter += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onMouseEnter -= OnTick;
            m_Registered = false;
        }
    }
}