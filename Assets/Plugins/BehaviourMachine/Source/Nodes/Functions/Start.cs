//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "Start is called just before any of the Update methods is called the first time",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.Start.html")]
    public class Start : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                owner.start += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            owner.start -= OnTick;
            m_Registered = false;
        }
    }
}
