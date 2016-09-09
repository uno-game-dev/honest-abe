//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Returns Success if some level is being loaded; otherwise returns Failure.
    /// </summary>
    [NodeInfo ( category = "Condition/Application/",
                icon = "GameManager",
                description = "Returns Success if some level is being loaded; otherwise returns Failure",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Application-isLoadingLevel.html")]
    public class IsLoadingLevel : ConditionNode {

        public override Status Update () {
            if (Application.isLoadingLevel) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }
    }
}