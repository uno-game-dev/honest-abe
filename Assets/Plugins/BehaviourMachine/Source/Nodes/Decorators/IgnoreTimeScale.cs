//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;

namespace BehaviourMachine {
    /// <summary>
    /// All nodes bellow this one will ignore time scale if Ignore is true; otherwise the time scale will be used.
    /// </summary>
    [NodeInfo(  category = "Decorator/",
                icon = "UnityEditor.AnimationWindow",
                description = "All nodes bellow this one will ignore time scale if Ignore is true; otherwise the time scale will be used",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Time-timeScale.html")]
    public class IgnoreTimeScale : DecoratorNode {

        /// <summary>
        /// If True, ignores time scale.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "True", tooltip = "If True, ignores time scale")]
        public BoolVar ignore;

        public override void Reset () {
            ignore = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Validate members
            if (child == null)
                return Status.Error;

            if (ignore.isNone || ignore.Value) {
                var oldTreeIgnore = owner.ignoreTimeScale;
                owner.ignoreTimeScale = true;
                child.OnTick();
                owner.ignoreTimeScale = oldTreeIgnore;
            }
            else
                child.OnTick();

            return child.status;
        }
    }
}