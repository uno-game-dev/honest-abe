//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Move the rigidbody using "Direction", "X", "Y" and "Z".
    /// <seealso cref="Action.Transform.Translate" />
    /// </summary>
    [NodeInfo ( category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Move the rigidbody using \"Direction\", \"X\", \"Y\" and \"Z\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.MovePosition.html")]
    public class MovePosition : ActionNode, IFixedUpdateNode {

    	/// <summary>
        /// The game object that has a rigidbody in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody in it")]
        public GameObjectVar gameObject;

        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The direction to move the rigidbody")]
        public Vector3Var direction;

        /// <summary>
        /// Direction in the x axis (overrides direction.x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Direction in the x axis (overrides direction.x)")]
        public FloatVar x;

        /// <summary>
        /// Direction in the y axis (overrides direction.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Direction in the y axis (overrides direction.y)")]
        public FloatVar y;

        /// <summary>
        /// Direction in the z axis (overrides direction.z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Direction in the z axis (overrides direction.z)")]
        public FloatVar z;

        /// <summary>
        /// Self, applies the movement around the transform's local axes. World, applies the movement around the global axes.
        /// </summary>
        [Tooltip("Self, applies the movement around the transform's local axes. World, applies the movement around the global axes")]
        public Space relativeTo = Space.Self;

        /// <summary>
        /// If True the movement will be applied every second; otherwise the movement will be applied instantaneously.
        /// </summary>
        [Tooltip("If True the movement will be applied every second; otherwise the movement will be applied instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Rigidbody m_Rigidbody = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            direction = new ConcreteVector3Var();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            z = new ConcreteFloatVar();
            perSecond = true;
            relativeTo = Space.Self;
        }

        public override Status Update () {
            // Get the rigidbody
            if (m_Rigidbody == null || m_Rigidbody.gameObject != gameObject.Value)
                m_Rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members
            if (m_Rigidbody == null)
                return Status.Error;

            // Get the velocity
            Vector3 desiredDirection =  !direction.isNone ? direction.Value : Vector3.zero;

            // Override values?
            if (!x.isNone) desiredDirection.x = x.Value;
            if (!y.isNone) desiredDirection.y = y.Value;
            if (!z.isNone) desiredDirection.z = z.Value;

            // Get relativeDirection
            Vector3 relativeDirection = relativeTo == Space.Self ? m_Rigidbody.rotation * desiredDirection : desiredDirection;

            // Move per second?
            if (perSecond)
                m_Rigidbody.MovePosition (m_Rigidbody.position + relativeDirection * this.owner.deltaTime);
            else
                m_Rigidbody.MovePosition (m_Rigidbody.position + relativeDirection);

            return Status.Success;
        }
    }
}