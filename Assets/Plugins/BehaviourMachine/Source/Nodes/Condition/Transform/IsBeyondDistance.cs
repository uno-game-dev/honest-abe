//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Succeeds if "Game Object 1" and "Game Object 2" are beyond "Distance". Returns Error if the game object in "Game Object 1" or "Game Object 2" are null.
    /// </summary>
    [NodeInfo ( category = "Condition/Transform/",
                icon = "Transform",
                description = "Succeeds if \"Game Object 1\" and \"Game Object 2\" are beyond \"Distance\". Returns Error if the game object in \"Game Object 1\" or \"Game Object 2\" are null")]
    public class IsBeyondDistance : ConditionNode {

        /// <summary>
        /// The first game object to test the distance.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The first game object to test the distance")]
        public GameObjectVar gameObject1;

        /// <summary>
        /// The second game object to test the distance.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The second game object to test the distance")]
        public GameObjectVar gameObject2;

        /// <summary>
        /// The distance to test.
        /// </summary>
        [VariableInfo(tooltip = "The distance to test")]
        public FloatVar distance;

        [System.NonSerialized]
        Transform m_Transform1 = null;
        [System.NonSerialized]
        Transform m_Transform2 = null;

        public override void Reset () {
            base.Reset();

            gameObject1 = new ConcreteGameObjectVar(this.self);
            gameObject2 = new ConcreteGameObjectVar(this.self);
            distance = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the transform1
            if (m_Transform1 == null || m_Transform1.gameObject != gameObject1.Value)
                m_Transform1 = gameObject1.Value != null ? gameObject1.Value.transform : null;

            // Get the transform2
            if (m_Transform2 == null || m_Transform2.gameObject != gameObject2.Value)
                m_Transform2 = gameObject2.Value != null ? gameObject2.Value.transform : null;

            // Validate members
            if (m_Transform1 == null || m_Transform2 == null || distance.isNone)
                return Status.Error;

            if (Vector3.Distance(m_Transform1.position, m_Transform2.position) > distance.Value) {
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