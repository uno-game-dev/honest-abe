//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnMouseOver is called every frame while the mouse is over the GUIElement or Collider.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnMouseOver is called every frame while the mouse is over the GUIElement or Collider",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnMouseOver.html")]
    public class OnMouseOver : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onMouseOver += this.OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onMouseOver -= this.OnTick;
            m_Registered = false;
        }
    }
}