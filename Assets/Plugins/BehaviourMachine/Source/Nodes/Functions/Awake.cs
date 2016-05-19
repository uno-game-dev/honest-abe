//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "Awake is called when the script instance is being loaded",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.Awake.html")]
    public class Awake : OnAwake {

        public override void OnAwakeTree () {
            if (this.enabled) {
                tree.awake += OnTick;
            }
        }
    }
}

