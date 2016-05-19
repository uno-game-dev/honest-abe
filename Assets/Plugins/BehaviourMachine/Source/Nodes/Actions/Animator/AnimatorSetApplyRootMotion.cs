//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the value of the "Apply Root Motion" property of an Animator component.
    /// </summary>
    [NodeInfo ( category = "Action/Animator/",
                icon = "Animator",
                description = "Sets the value of the \"Apply Root Motion\" property of an Animator component")]
    public class AnimatorSetApplyRootMotion : ActionNode {

        /// <summary>
        /// A game object with an Animator in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object with an Animator in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new value for the "Apply Root Motion" property.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "The new value for the \"Apply Root Motion\" property")]
        public BoolVar newValue;

        [System.NonSerialized]
        Animator m_Animator = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newValue = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Get the animator
            if (m_Animator == null || m_Animator.gameObject != gameObject.Value)
                m_Animator = gameObject.Value != null ? gameObject.Value.GetComponent<Animator>() : null;

            // Validate Members
            if (m_Animator == null)
                return Status.Error;

            m_Animator.applyRootMotion = newValue.isNone ? !m_Animator.applyRootMotion : newValue.Value;
            return Status.Success;
        }
    }
}