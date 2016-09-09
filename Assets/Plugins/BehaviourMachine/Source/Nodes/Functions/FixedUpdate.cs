//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------s

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame, if the Tree is enabled.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "FixedUpdate is called every fixed framerate frame, if the Tree is enabled",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.FixedUpdate.html")]
    public class FixedUpdate : FunctionNode {

        public override void OnEnable () {
            if (this.enabled) {
                InternalGlobalBlackboard.fixedUpdate += OnTick;

                m_Registered = true;
            }
        }

        public override void OnDisable () {
            InternalGlobalBlackboard.fixedUpdate -= OnTick;

            m_Registered = false;
        }
    }
}
