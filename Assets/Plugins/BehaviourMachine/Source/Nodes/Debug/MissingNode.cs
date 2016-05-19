//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Used in the place of a missing node.
    /// A node is missing usually when the script is deleted or the type has changed.
    /// Never delete this script.
    /// </summary>
    [NodeInfo ( category = "Debug/",
                icon = "console.warnicon.sml",
                description = "Used in the place of a missing node. A node is missing usually when the script is deleted or the type has changed")]
    public sealed class MissingNode : CompositeNode {

        public string missingNodeType = string.Empty;

        public override void Awake () {
        	Print.LogWarning("Missing Node Script: " + missingNodeType, owner as UnityEngine.Object);
        }
    }
}