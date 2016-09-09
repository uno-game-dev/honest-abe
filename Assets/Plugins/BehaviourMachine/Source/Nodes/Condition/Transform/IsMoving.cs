//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Has the 'Game Object' moved?
    /// Returns Success if the 'Game Object' has moved; otherwise returns Failure.
    /// </summary>
    [NodeInfo(  category = "Condition/Transform/",
                icon = "Transform",
                description = "Has the \'Game Object\' moved? Returns Success if the \'Game Object\' has moved; otherwise returns Failure")]
    public class IsMoving : ConditionNode {

    	/// <summary>
        /// The game object to check hasChanged.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to check hasChanged")]
        public GameObjectVar gameObject;

        /// <summary>
        /// A tolerance value used to compare the position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A tolerance value used to compare the position")]
        public FloatVar threshold;

        [System.NonSerialized]
        Vector3 m_LastPosition;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
            threshold = 0.000001f;
        }

        public override void OnEnable () {
            m_LastPosition = gameObject.Value != null ? gameObject.transform.position : Vector3.zero;
        }

        public override Status Update () {
            // Validate members?
            if  (gameObject.Value == null || threshold.isNone)
                return Status.Error;

            // Get the current position
            Vector3 position = gameObject.transform.position;
            // Get the distance from the last frame
            Vector3 distance = position - m_LastPosition;

            if (distance.sqrMagnitude > threshold.Value / owner.deltaTime) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                // Store the last Position
                m_LastPosition = position;

                return Status.Success;
            }
            else {
                // Store the last Position
                m_LastPosition = position;
                return Status.Failure;
            }
        }
    }
}
#endif