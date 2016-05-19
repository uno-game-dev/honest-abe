//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets a new value to the "Game Object" hasChanged flag.
    /// </summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Sets a new value to the \"Game Object\" hasChanged flag",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform-hasChanged.html")]
    public class SetHasChanged : ActionNode {

    	/// <summary>
        /// The game object to update hasChanged.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to update hasChanged")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new hasChanged value.
        /// </summary>
        [VariableInfo(tooltip = "The new hasChanged value")]
        public BoolVar newHasChanged;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newHasChanged = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Get the transform
            var transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if (transform == null)
                return Status.Error;

            transform.hasChanged = newHasChanged.Value;

            return Status.Success;
        }
    }
}
#endif