//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnApplicationQuit is called before the application quits.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnApplicationQuit is called before the application quits",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnApplicationQuit.html")]
    public class OnApplicationQuit : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onApplicationQuit += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onApplicationQuit -= OnTick;
            m_Registered = false;
        }
    }
}