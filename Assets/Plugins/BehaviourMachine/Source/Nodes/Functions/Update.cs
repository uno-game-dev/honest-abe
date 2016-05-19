//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Update is called every frame, if the tree is enabled.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "Update is called every frame, if the tree is enabled",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.Update.html")]
    public class Update : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                InternalGlobalBlackboard.update += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            InternalGlobalBlackboard.update -= OnTick;
            m_Registered = false;
        }
    }
}
