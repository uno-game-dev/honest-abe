//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Rotates the "Game Object" by "Angles" passing a point, or an object, in world coordinates.
    /// <summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Rotates the \"Game Object\" by \"Angle\" passing a point, or an object, in world coordinates. ")]
    public class RotateAroundSmooth : ActionNode {

        /// <summary>
        /// The game object to rotate and change the position.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to rotate and change the position")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The object that the "Game Object" will rotate around.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use \"Around Point\" instead", tooltip = "The game object that the \"Game Object\" will rotate around")]
        public GameObjectVar aroundObject;

        /// <summary>
        /// The point in world coordinates that the "Game Object" will rotate around.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use \"Around Object\" instead", tooltip = "The point in world coordinates that the \"Game Object\" will rotate around")]
        public Vector3Var aroundPoint;

        /// <summary>
        /// The radius of the rotation.
        /// <summary>
        [VariableInfo(tooltip = "The radius of the rotation")]
        public FloatVar radius;

        /// <summary>
        /// The amount to rotate in degrees.
        /// <summary>
        [VariableInfo(tooltip = "The amount to rotate in degrees")]
        public Vector3Var angle;

        /// <summary>
        /// Approximately the time it will take to reach the target. A smaller value will reach the target faster.
        /// <summary>
        [VariableInfo(tooltip = "Approximately the time it will take to reach the target. A smaller value will reach the target faster")]
        public FloatVar smoothTime;

        /// <summary>
        /// Stores the move diretion.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", tooltip = "Stores the move diretion")]
        public Vector3Var storeDiretion;

        [System.NonSerialized]
        Transform m_Transform = null;
        [System.NonSerialized]
        Vector3 m_Velocity;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            aroundObject = new ConcreteGameObjectVar(this.self);
            aroundPoint = new ConcreteVector3Var();
            radius = new ConcreteFloatVar();
            angle = new ConcreteVector3Var();
            smoothTime = new ConcreteFloatVar();
            storeDiretion = new ConcreteVector3Var();

            m_Velocity = Vector3.zero;
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Is there a valid transform, a valid aroundPoint or a aroundObject and an axis and an angle?
            if (m_Transform == null ||  (aroundPoint.isNone && (aroundObject.isNone || aroundObject.Value == null)) || angle.isNone)
                return Status.Error;

            // Get the pivot
            Vector3 pivot = aroundPoint.isNone ? aroundObject.Value.transform.position : aroundPoint.Value;
            // Get point direction relative to pivot
            storeDiretion.Value = m_Transform.position - pivot;
            // Rotate it
            storeDiretion.Value = Quaternion.Euler(angle.Value) * storeDiretion.Value;
            // Normalize
            storeDiretion.Value.Normalize();
            // Set radius
            storeDiretion.Value *= radius.Value;
            // Get target point
            Vector3 targetPoint = storeDiretion.Value + pivot;

            // Rotate
            m_Transform.position = Vector3.SmoothDamp(m_Transform.position, targetPoint, ref m_Velocity, smoothTime.Value);

            return Status.Success;
        }
    }
}
