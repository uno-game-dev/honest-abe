//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// The target Animator is playing the supplied state?
    /// <summary>
    [NodeInfo ( category = "Condition/Animator/",
                icon = "Animator",
                description = "The target Animator is playing the supplied state?")]
    public class IsAnimatorPlayingState : ConditionNode {

        /// <summary>
        /// The game object that has an Animator component.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has an Animator component")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The layer to check.
        /// </summary>
        [VariableInfo(tooltip = "The layer to check")]
        public IntVar layer;

        /// <summary>
        /// The name of the state to test.
        /// </summary>
        [VariableInfo(tooltip = "The name of the state to test")]
        public StringVar stateName;

        /// <summary>
        /// If True then it will return Success if the Animator is in transition.
        /// </summary>
        [Tooltip("If True then it will return Success if the Animator is in transition")]
        public bool isInTransition = true;

        [System.NonSerialized]
        Animator m_Animator = null;

        public override void Reset () {
            base.Reset ();

            gameObject = this.self;
            layer = 0;
            stateName = "State Name";
        }

        public override Status Update () {
            // Get the animator
            if (m_Animator == null || m_Animator.gameObject != gameObject.Value)
                m_Animator = gameObject.Value != null ? gameObject.Value.GetComponent<Animator>() : null;

            if (m_Animator == null || layer.isNone || stateName.isNone)
                return Status.Error;

            if ((isInTransition && m_Animator.IsInTransition(layer.Value)) || m_Animator.GetCurrentAnimatorStateInfo(layer.Value).IsName(stateName.Value)) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }
    }
}