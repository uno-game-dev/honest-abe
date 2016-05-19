//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Has the transform changed since the last time the flag was set to false?
    /// Returns Success if the transform has changed; otherwise returns Failure.
    /// </summary>
    [NodeInfo(  category = "Condition/Transform/",
                icon = "Transform",
                description = "Has the transform changed since the last time the flag was set to False? Returns Success if the transform has changed; otherwise returns Failure",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform-hasChanged.html")]
    public class HasChanged : ConditionNode {

    	/// <summary>
        /// The game object to check hasChanged.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to check hasChanged")]
        public GameObjectVar gameObject;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members?
            if  (m_Transform == null)
                return Status.Error;

            if (m_Transform.hasChanged) {
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
#endif