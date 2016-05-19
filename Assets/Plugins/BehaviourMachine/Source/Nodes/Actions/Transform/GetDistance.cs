//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the distance between "Game Object 1" and "Game Object 2".
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Gets the distance between \"Game Object 1\" and \"Game Object 2\"")]
    public class GetDistance : ActionNode {

        /// <summary>
        /// The first game object.
        /// </summary>
    	[VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The first game object")]
        public GameObjectVar gameObject1;

        /// <summary>
        /// The second game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The second game object")]
        public GameObjectVar gameObject2;

        /// <summary>
        /// Stores the distance between the game objects.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Stores the distance between the game objects")]
        public Vector3Var storeDistance;

        /// <summary>
        /// Store the distance.x.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the distance.x")]
        public FloatVar storeX;

        /// <summary>
        /// Store the distance.y.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the distance.y")]
        public FloatVar storeY;

        /// <summary>
        /// Store the distance.z.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the distance.z")]
        public FloatVar storeZ;

        /// <summary>
        /// Self, gets the distance relative to the parent's transform. World, gets the distance in world space.
        /// </summary>
        [Tooltip("Self, gets the distance relative to the parent transform. World, gets the distance in world space")]
        public Space relativeTo = Space.World;

        [System.NonSerialized]
        Transform m_Transform1 = null;
        [System.NonSerialized]
        Transform m_Transform2 = null;

        public override void Reset () {
            gameObject1 = new ConcreteGameObjectVar(this.self);
            gameObject2 = new ConcreteGameObjectVar(this.self);
            storeDistance = new ConcreteVector3Var();
            storeX = new ConcreteFloatVar();
            storeY = new ConcreteFloatVar();
            storeZ = new ConcreteFloatVar();
            relativeTo = Space.World;
        }

        public override Status Update () {
            // Get the transform1
            if (m_Transform1 == null || m_Transform1.gameObject != gameObject1.Value)
                m_Transform1 = gameObject1.Value != null ? gameObject1.Value.transform : null;

            // Get the transform2
            if (m_Transform2 == null || m_Transform2.gameObject != gameObject2.Value)
                m_Transform2 = gameObject2.Value != null ? gameObject2.Value.transform : null;

            // Validate members
            if (m_Transform1 == null || m_Transform2 == null)
                return Status.Error;

            // Store distance
            if (relativeTo == Space.World)
                storeDistance.Value = m_Transform1.position - m_Transform2.position;
            else
                storeDistance.Value = m_Transform1.localPosition - m_Transform2.localPosition;

            storeX.Value = storeDistance.Value.x;
            storeY.Value = storeDistance.Value.y;
            storeZ.Value = storeDistance.Value.z;

            return Status.Success;
        }
    }
}