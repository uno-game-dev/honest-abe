//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnEnable is called when the tree becomes enabled and active.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnEnable is called when the tree becomes enabled and active",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnEnable.html")]
    public class OnEnable : OnEnableDisableOwner {

        public override void OnEnableOwner () {
            if (this.enabled) {
                owner.onEnable += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisableOwner () {
            owner.onEnable -= OnTick;
            m_Registered = false;
        }
    }
}