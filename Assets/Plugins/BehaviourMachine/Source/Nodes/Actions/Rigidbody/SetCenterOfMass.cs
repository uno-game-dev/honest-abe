//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the rigidbody centerOfMass.
    /// </summary>
    [NodeInfo ( category = "Action/Rigidbody/",
                icon = "Rigidbody",
                description = "Sets the rigidbody centerOfMass",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody-centerOfMass.html")]
    public class SetCenterOfMass : ActionNode {

        /// <summary>
        /// The game object that has a rigidbody in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a rigidbody in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new centerOfMass.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The new centerOfMass")]
        public Vector3Var newCenterOfMass;

        /// <summary>
        /// The centerOfMass in the x axis (overrides "New Center Of Mass".x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Center Of Mass in the x axis (overrides \"New Center Of Mass\".x)")]
        public FloatVar x;

        /// <summary>
        /// The centerOfMass in the y axis (overrides "New Center Of Mass".y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The centerOfMass in the y axis (overrides \"New Center Of Mass\".y)")]
        public FloatVar y;

        /// <summary>
        /// The centerOfMass in the z axis (overrides "New Center Of Mass".z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The centerOfMass in the z axis (overrides \"New Center Of Mass\".z)")]
        public FloatVar z;

        [System.NonSerialized]
        Rigidbody m_Rigidbody = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newCenterOfMass = new ConcreteVector3Var();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            z = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the rigidbody
            if (m_Rigidbody == null || m_Rigidbody.gameObject != gameObject.Value)
                m_Rigidbody = gameObject.Value != null ? gameObject.Value.GetComponent<Rigidbody>() : null;

            // Validate members?
            if  (m_Rigidbody == null)
                return Status.Error;

            // Get centerOfMass
            Vector3 centerOfMass = newCenterOfMass.isNone ? Vector3.zero : newCenterOfMass.Value;

            // Override values?
            if (!x.isNone) centerOfMass.x = x.Value;
            if (!y.isNone) centerOfMass.y = y.Value;
            if (!z.isNone) centerOfMass.z = z.Value;

            // Set centerOfMass
            m_Rigidbody.centerOfMass = centerOfMass;

            return Status.Success;
        }
    }
}