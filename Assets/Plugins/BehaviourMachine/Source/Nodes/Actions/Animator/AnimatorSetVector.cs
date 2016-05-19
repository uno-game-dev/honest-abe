//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if UNITY_4_0_0 || UNITY_4_1 || UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the value of a vector parameter.
    /// </summary>
    [NodeInfo ( category = "Action/Animator/",
                icon = "Animator",
                description = "Sets the value of a vector parameter")]
    public class AnimatorSetVector : ActionNode {

        /// <summary>
        /// A game object with an Animator in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object with an Animator in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Parameter name.
        /// </summary>
        [VariableInfo (tooltip = "Parameter name")]
        public StringVar parameterName;

        /// <summary>
        /// The new value for the parameter.
        /// </summary>
        [VariableInfo(tooltip = "The new value for the parameter")]
        public Vector3Var newValue;

        [System.NonSerialized]
        Animator m_Animator = null;

        public override void Reset () {
            parameterName = new ConcreteStringVar();
            newValue = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Get the animator
            if (m_Animator == null || m_Animator.gameObject != gameObject.Value)
                m_Animator = gameObject.Value != null ? gameObject.Value.GetComponent<Animator>() : null;

            // Validate Members
            if (m_Animator == null || parameterName.isNone || newValue.isNone)
                return Status.Error;

            m_Animator.SetVector(parameterName.Value, newValue.Value);
            return Status.Success;
        }
    }
}
#endif