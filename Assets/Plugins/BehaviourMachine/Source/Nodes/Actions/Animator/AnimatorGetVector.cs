//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if UNITY_4_0_0 || UNITY_4_1 || UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the value of a vector parameter.
    /// </summary>
    [NodeInfo ( category = "Action/Animator/",
                icon = "Animator",
                description = "Gets the value of a vector parameter")]
    public class AnimatorGetVector : ActionNode {

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
        /// Store the parameter value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the parameter value")]
        public Vector3Var store;

        [System.NonSerialized]
        Animator m_Animator = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            parameterName = new ConcreteStringVar();
            store = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Get the animator
            if (m_Animator == null || m_Animator.gameObject != gameObject.Value)
                m_Animator = gameObject.Value != null ? gameObject.Value.GetComponent<Animator>() : null;

            // Validate Members
            if (m_Animator == null || parameterName.isNone || store.isNone)
                return Status.Error;

            store.Value = m_Animator.GetVector(parameterName.Value);
            return Status.Success;
        }
    }
}
#endif