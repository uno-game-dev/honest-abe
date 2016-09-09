//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the value of an integer parameter.
    /// </summary>
    [NodeInfo ( category = "Action/Animator/",
                icon = "Animator",
                description = "Gets the value of an integer parameter")]
    public class AnimatorGetInteger : ActionNode {

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
        public IntVar store;

        [System.NonSerialized]
        Animator m_Animator = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            parameterName = new ConcreteStringVar();
            store = new ConcreteIntVar();
        }

        public override Status Update () {
            // Get the animator
            if (m_Animator == null || m_Animator.gameObject != gameObject.Value)
                m_Animator = gameObject.Value != null ? gameObject.Value.GetComponent<Animator>() : null;

            // Validate Members
            if (m_Animator == null || parameterName.isNone || store.isNone)
                return Status.Error;

            store.Value = m_Animator.GetInteger(parameterName.Value);
            return Status.Success;
        }
    }
}