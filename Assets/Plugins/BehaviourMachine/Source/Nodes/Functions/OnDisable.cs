//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnDisable is called when the tree becomes disabled or inactive.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnDisable is called when the tree becomes disabled or inactive",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnDisable.html")]
    public class OnDisable : OnEnableDisableOwner {

        public override void OnEnableOwner () {
            if (this.enabled) {
                owner.onDisable += OnTick;
                m_Registered = true;
            }
        }

        public override void OnDisableOwner () {
            owner.onDisable -= OnTick;
            m_Registered = false;
        }
    }
}