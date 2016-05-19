//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the value of the "Apply Root Motion" property of an Animator component.
    /// </summary>
    [NodeInfo ( category = "Action/Animator/",
                icon = "Animator",
                description = "Gets the value of the \"Apply Root Motion\" property of an Animator component")]
    public class AnimatorGetApplyRootMotion : ActionNode {

        /// <summary>
        /// A game object with an Animator in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object with an Animator in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Stores the value of the "Apply Root Motion" property.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Stores the value of the \"Apply Root Motion\" property")]
        public BoolVar storeApplyRootMotion;

        [System.NonSerialized]
        Animator m_Animator = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            storeApplyRootMotion = new ConcreteBoolVar();
        }

        public override Status Update () {
            // Get the animator
            if (m_Animator == null || m_Animator.gameObject != gameObject.Value)
                m_Animator = gameObject.Value != null ? gameObject.Value.GetComponent<Animator>() : null;

            // Validate Members
            if (m_Animator == null || storeApplyRootMotion.isNone)
                return Status.Error;

            storeApplyRootMotion.Value = m_Animator.applyRootMotion;
            return Status.Success;
        }
    }
}