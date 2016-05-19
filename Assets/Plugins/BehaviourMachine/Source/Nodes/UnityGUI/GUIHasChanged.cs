//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Returns Success if any control changed the value of the input data.
    /// <summary>
    [NodeInfo ( category = "UnityGUI/",
                icon = "Condition",
                description = "Returns Success if any control changed the value of the input data")]
    public class GUIHasChanged : ConditionNode, IGUINode {

        public override Status Update () {
            // Is OnGUI?
            if (Event.current == null)
                return Status.Error;

            if (GUI.changed)
                return Status.Success;
            else
                return Status.Failure;
        }
    }
}