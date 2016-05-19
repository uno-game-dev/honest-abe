//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Rotates the "Game Object" about "Axis" passing a point, or an object, in world coordinates by "Angle" degrees.
    /// This modifies both the position and the rotation of the "Game Object".
    /// Returns Error if the "Game Object" is null or if "Around Object" and "Around Point" are both None.
    /// <summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Rotates the \"Game Object\" about \"Axis\" passing a point, or an object, in world coordinates by \"Angle\" degrees. This modifies both the position and the rotation of the \"Game Object\". Returns Error if the \"Game Object\" is null or if \"Around Object\" and \"Around Point\" are both None",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform.RotateAround.html")]
    public class RotateAround : ActionNode {

        /// <summary>
        /// The game object to rotate and change the position.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to rotate and change the position")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The game object that the "Game Object" will rotate around.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use \"Around Point\" instead", tooltip = "The game object that the \"Game Object\" will rotate around")]
        public GameObjectVar aroundObject;

        /// <summary>
        /// The point in world coordinates that the "Game Object" will rotate around.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use \"Around Object\" instead", tooltip = "The point in world coordinates that the \"Game Object\" will rotate around")]
        public Vector3Var aroundPoint;

        /// <summary>
        /// The axis that the "Game Object" will rotate around.
        /// <summary>
        [VariableInfo(tooltip = "The axis that the \"Game Object\" will rotate around")]
        public Vector3Var axis;

        /// <summary>
        /// The amount to rotate in degrees.
        /// <summary>
        [VariableInfo(tooltip = "The amount to rotate in degrees")]
        public FloatVar angle;

        /// <summary>
        /// If True the rotation will be applied every second; otherwise the rotation will be applied instantaneously.
        /// <summary>
        [Tooltip("If True the rotation will be applied every second; otherwise the rotation will be applied instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            aroundObject = new ConcreteGameObjectVar(this.self);
            aroundPoint = new ConcreteVector3Var();
            axis = new ConcreteVector3Var();
            angle = new ConcreteFloatVar();
            perSecond = true;
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if (m_Transform == null ||  (aroundPoint.isNone && (aroundObject.isNone || aroundObject.Value == null)) || axis.isNone || angle.isNone)
                return Status.Error;

            // Get the point
            Vector3 point = aroundPoint.isNone ? aroundObject.Value.transform.position : aroundPoint.Value;

            // Rotate per second?
            if (perSecond)
                m_Transform.RotateAround(point, axis.Value, angle.Value * owner.deltaTime);
            else
                m_Transform.RotateAround(point, axis.Value, angle.Value);

            return Status.Success;
        }
    }
}
