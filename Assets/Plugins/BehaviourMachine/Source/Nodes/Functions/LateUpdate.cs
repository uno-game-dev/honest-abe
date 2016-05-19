//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// LateUpdate is called every frame, if the Tree is enabled.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "LateUpdate is called every frame, if the Tree is enabled",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.LateUpdate.html")]
    public class LateUpdate : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                InternalGlobalBlackboard.lateUpdate += OnTick;

                m_Registered = true;
            }
        }

        public override void OnDisable () {
            InternalGlobalBlackboard.lateUpdate -= OnTick;

            m_Registered = false;
        }
    }
}
