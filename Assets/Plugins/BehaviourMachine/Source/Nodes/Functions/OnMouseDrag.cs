//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnMouseDrag.html")]
    public class OnMouseDrag : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onMouseDrag += this.OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onMouseDrag -= this.OnTick;
            m_Registered = false;
        }
    }
}