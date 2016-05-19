//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary> 
    /// The AnyState is always called by the parent fsm during change state.
    /// Very useful if you want a transition to be triggered independent of the enabled state.
    /// </summary>
    [AddComponentMenu("")]
    public class InternalAnyState : InternalStateBehaviour {

        [HideInInspector]
        [SerializeField]
        bool m_CanTransitionToSelf;

        #region Properties
        /// <summary>
        /// Returns True if the AnyState can perform transitions to the enabled state; False otherwise.
        /// </summary>
        public bool canTransitionToSelf {get {return m_CanTransitionToSelf;}}
        #endregion Properties

        
        #region Unity Callbacks
        /// <summary> 
        /// Sets the fsm.anyState; if it's null.
        /// </summary>
        void Awake () {
            var fsm = this.fsm;
            if (fsm != null) {
                if (fsm.anyState == null)
                    fsm.anyState = this;
                else if (fsm.anyState != this)
                    Print.LogError("More than one AnyState in FSM.", this.gameObject);
            }
            else
                Print.LogError("AnyState without FSM.", this.gameObject);
        }

        /// <summary> 
        /// Unity callback called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            // Validates AnyState
            var fsm = this.fsm;
            if (fsm != null) {
                if (fsm.anyState == null)
                    fsm.anyState = this;
                else if (fsm.anyState != this) {
                    parent = null;
                    this.StateSetDirty();
                }
            }
        }
        #endregion Unity Callbacks


        /// <summary> 
        /// Workaround to change the enabled state even if this anyState is not enabled.
        /// <param name="eventID">The id of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public override bool ProcessEvent (int eventID) {
            // The fsm is not enabled?
            if (fsm == null || !fsm.enabled)
                return false;

            // Searches for the transition that has the eventID
            var cachedTransitions = transitions;
            for (int i = 0; i < cachedTransitions.Length; i++) {
                if (cachedTransitions[i].eventID == eventID) {
                    var destination = cachedTransitions[i].destination; // get the destination state
                    
                    // The destination is a valid state?
                    if (destination != null && (m_CanTransitionToSelf || !destination.enabled)) {
                        // Report transition
                        #if UNITY_EDITOR
                        if (Application.isEditor) {
                            OnTransition(cachedTransitions[i]);
                        }
                        #endif

                        // Change state
                        destination.enabled = true;
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

    }
}