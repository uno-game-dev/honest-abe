//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Has the 'Game Object' rotated?
    /// Returns Success if the 'Game Object' has rotated; otherwise returns Failure.
    /// </summary>
    [NodeInfo(  category = "Condition/Transform/",
                icon = "Transform",
                description = "Has the \'Game Object\' rotated? Returns Success if the \'Game Object\' has rotated; otherwise returns Failure")]
    public class IsRotating : ConditionNode {

    	/// <summary>
        /// The game object to check hasChanged.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to check hasChanged")]
        public GameObjectVar gameObject;

        /// <summary>
        /// A tolerance value used to compare the rotation.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A tolerance value used to compare the rotation")]
        public FloatVar threshold;

        [System.NonSerialized]
        Quaternion m_LastRotation;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
            threshold = 0.000001f;
        }

        public override void OnEnable () {
            m_LastRotation = gameObject.Value != null ? gameObject.transform.rotation : Quaternion.identity;
        }

        public override Status Update () {
            // Validate members?
            if  (gameObject.Value == null || threshold.isNone)
                return Status.Error;

            // Get the current rotation
            Quaternion rotation = gameObject.transform.rotation;
            // Get the angle between the current and the last frame
            float angle = Quaternion.Angle(rotation, m_LastRotation);

            if (angle > threshold.Value / owner.deltaTime) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                // Store the last Rotation
                m_LastRotation = rotation;

                return Status.Success;
            }
            else {
                // Store the last Rotation
                m_LastRotation = rotation;
                return Status.Failure;
            }
        }
    }
}
#endif