//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Rotates the "Object To Rotate" so the forward vector points at "Object To Be Looked"'s current position.
    /// Returns Error if "Object To Rotate" or "Object To Look At" has null game object.
    /// </summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Rotates the \"Object To Rotate\" so the forward vector points at \"Object To Looked At\"'s current position. Returns Error if \"Object To Rotate\" or \"Object To Look At\" has null game object",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform.LookAt.html")]
    public class LookAt : ActionNode {
        /// <summary>
        /// The game object to be rotated.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to be rotated")]
        public GameObjectVar objectToRotate;

        /// <summary>
        /// The game object to look at.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to look at")]
        public GameObjectVar objectToLookAt;

        /// <summary>
        /// Then it rotates the transform to point its up direction vector in the direction hinted at by the "World Up" vector.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Vector3.up", tooltip = "Then it rotates the transform to point its up direction vector in the direction hinted at by the \"World Up\" vector")]
        public Vector3Var worldUp;

        [System.NonSerialized]
        Transform m_TransformToRotate = null;
        [System.NonSerialized]
        Transform m_TransformToLookAt = null;

        public override void Reset () {
            objectToRotate = new ConcreteGameObjectVar(this.self);
            objectToLookAt = new ConcreteGameObjectVar(this.self);
            worldUp = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Get the transformToRotate
            if (m_TransformToRotate == null || m_TransformToRotate.gameObject != objectToRotate.Value)
                m_TransformToRotate = objectToRotate.Value != null ? objectToRotate.Value.transform : null;

            // Get the transformToBeLooked
            if (m_TransformToLookAt == null || m_TransformToLookAt.gameObject != objectToLookAt.Value)
                m_TransformToLookAt = objectToLookAt.Value != null ? objectToLookAt.Value.transform : null;

            // Check for errors
            if (m_TransformToRotate == null || m_TransformToLookAt == null)
                return Status.Error;

            // Look at
            m_TransformToRotate.LookAt(m_TransformToLookAt, (worldUp.isNone) ? Vector3.up : worldUp.Value);

            return Status.Success;
        }
    }
}