//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
    
    /// <summary> 
    /// A Finite State Machine (FSM) has a set of states; only one of these states can be enabled at a time.
    /// </summary>
    [AddComponentMenu("")]
    public class InternalStateMachine : ParentBehaviour {

        #region Members
        [HideInInspector]
        [SerializeField]
        InternalStateBehaviour m_EnabledState;

        [HideInInspector]
        [SerializeField]
        InternalStateBehaviour m_LastEnabledState;

        [HideInInspector]
        [SerializeField]
        InternalStateBehaviour m_StartState;

        [HideInInspector]
        [SerializeField]
        InternalAnyState m_AnyState;

        [HideInInspector]
        [SerializeField]
        InternalStateBehaviour m_ConcurrentState;
        #endregion Members

        
        #region Properties
        /// <summary> 
        /// The enabled state in this fsm.
        /// </summary>
        public InternalStateBehaviour enabledState {get {return m_EnabledState;}}

        /// <summary> 
        /// The last enabled state in this parent.
        /// </summary>
        public InternalStateBehaviour lastEnabledState {get {return m_LastEnabledState;}}

        /// <summary> 
        /// Returns the start state.
        /// </summary>
        public InternalStateBehaviour startState {get {return m_StartState;} 
            set {
                if ((value == null || value.parent == this) && !(value is InternalAnyState))
                    m_StartState = value;
            }
        }

        /// <summary> 
        /// Returns the any state.
        /// <seealso cref="BehaviourMachine.InternalAnyState" /> 
        /// </summary>
        public InternalAnyState anyState {get {return m_AnyState;} set {m_AnyState = value;}}

        /// <summary> 
        /// Returns the concurrent state.
        /// </summary>
        public InternalStateBehaviour concurrentState {get {return m_ConcurrentState;} 
            set {
                if ((value == null || value.parent == this) && !(value is InternalAnyState)) {
                    // Disable the current concurrent state
                    if (Application.isPlaying && m_ConcurrentState != null && m_ConcurrentState.enabled)
                        ((MonoBehaviour)m_ConcurrentState).enabled = false;
                    
                    // Set the new concurrent state
                    m_ConcurrentState = value;

                    // Enable the new concurrent state
                    if (Application.isPlaying && m_ConcurrentState != null)
                        ((MonoBehaviour)m_ConcurrentState).enabled = true;
                }
            }
        }
        #endregion Properties


        #region Enabled State
        /// <summary>
        /// Callback called by a child InternalStateBehaviour when it's enabled.
        /// </summary>
        protected override void OnEnableState (InternalStateBehaviour childState) {
            // Disable the enabled state
            if (m_EnabledState != null && m_EnabledState.enabled)
                m_EnabledState.enabled = false;

            // Update the the enabled member
            m_EnabledState = childState;
        }

        /// <summary>
        /// Callback called by a child InternalStateBehaviour when it's disabled.
        /// </summary>
        protected override void OnDisableState (InternalStateBehaviour childState) {
            // Store the last enabled state
            if (m_EnabledState.parent == this)
                m_LastEnabledState = m_EnabledState;

            // Update the enabled member
            m_EnabledState = null;
        }

        /// <summary>
        /// Returns true if the supplied child state is enabled; false otherwise.
        /// <param name="childState">A child state of the ParentBehaviour.</param>
        /// <returns>.</returns>
        /// </summary>
        public override bool IsEnabled (InternalStateBehaviour childState) {
            return m_EnabledState == childState;
        }

        /// <summary>
        /// Returns the enabled state name in this parent.
        /// <returns>The enabled state name.</returns>
        /// </summary>
        public override string GetEnabledStateName () {
            if (enabled)
                return m_EnabledState != null ? m_EnabledState.stateName : this.stateName;
            else
                return "No State";
        }
        #endregion Enabled State

        
        #region Public Methods
        /// <summary> 
        /// Try to change the enabled state.
        /// Uses the supplied event to change the enabled state.
        /// <param name="eventID">The id of the event. The FsmEvent's id in the Blackboard.</param>
        /// <returns>Returns True if the event was used; false otherwise.</returns>
        /// </summary>
        public override bool ProcessEvent (int eventID) {
            // Try itself
            if (base.ProcessEvent(eventID))
                return true;
            // Try the AnyState
            else if (m_AnyState != null && m_AnyState.ProcessEvent(eventID))
                return true;
            // Try the ConcurrentState
            else if (m_ConcurrentState != null && m_ConcurrentState.ProcessEvent(eventID))
                return true;
            // Try the enabled state
            else if (m_EnabledState != null)
                return m_EnabledState.ProcessEvent(eventID);
            return false; 
        }
        #endregion Public Methods


        #region Unity Callbacks
        /// <summary> 
        /// Unity callback called when the object becomes enabled and active.
        /// Enables the start state, if the start state is null then tries to enable the first one.
        /// </summary>
        public virtual void OnEnable () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            // Add this parent to the blackboard to receive system events
            if (isRoot)
                blackboard.AddRootParent(this);

            // Enable the concurrent state
            if (m_ConcurrentState != null && !m_ConcurrentState.enabled)
                ((MonoBehaviour)m_ConcurrentState).enabled = true;

            // There is not an enabled state?
            if (m_EnabledState == null || !m_EnabledState.enabled || m_EnabledState.parent != this) {
                // Enable the start state
                if (m_StartState != null && m_StartState.parent == this && !m_StartState.enabled) {
                        m_StartState.enabled = true;
                }
                // Try to get the first state
                else {
                    var states = this.states;
                    var firstState = states.Count > 0 ? states[0] : null;
                    if (firstState != null && !(firstState is InternalAnyState)) {
                        m_StartState = firstState;
                        m_StartState.enabled = true;
                        Print.LogWarning("Start State not set in \'" + stateName + "\', getting the first one \'" + m_StartState.name  + "\' (" + m_StartState.GetType().Name + ").", this);
                    }
                    else {
                        Print.LogError("No state in \'" + stateName + "\' (" + GetType().Name + ").", this);
                    }
                }
            }
        }

        /// <summary> 
        /// Unity callback called when the behaviour becomes disabled or inactive.
        /// Disables the start state.
        /// </summary>
        public virtual void OnDisable () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            // Remove this parent from blackboard to not receive system events
            if (isRoot)
                blackboard.RemoveRootParent(this);

            // Disable the current enabled state
            if (m_EnabledState != null)
                m_EnabledState.enabled = false;

            // Disable the concurrent state
            if (m_ConcurrentState != null && m_ConcurrentState.enabled)
                ((MonoBehaviour)m_ConcurrentState).enabled = false;
        }

        /// <summary> 
        /// Unity callback called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validates the start, the AnyState and the enabled states.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            // Validate the enabled state.
            if (m_EnabledState != null && m_EnabledState.parent != this)
                m_EnabledState = null;

            // Validate the start state and the currently enabled state
            if (m_StartState != null && m_StartState.parent != this)
                m_StartState = null;

            // Validate the any state
            if (m_AnyState != null && m_AnyState.parent != this)
                m_AnyState = null;

            // Validate the concurrent state
            if (m_ConcurrentState != null && m_ConcurrentState.parent != this)
                m_ConcurrentState = null;

        }
        #endregion Unity Callbacks


        #region BehaviourMachine Callbacks
        /// <summary>
        /// Calls the enabledState's OnTick function.
        /// <returns>The execution status of the enabledState or Status.Error if there is no enabledState.</returns>
        /// </summary>
        public override Status OnTick () {
            if (m_EnabledState != null)
                return m_EnabledState.OnTick();
            else
                return Status.Error;
        }

        /// <summary> 
        /// BehaviourMachine Callback used to enable/disable the state.
        /// Enables the root. The root should always be enabled.
        /// </summary>
        public override void UpdateLogic () {
            base.UpdateLogic();
            
            // This is a root parent?
            if (isRoot) {
                if (Application.isPlaying)
                    enabled = true;                     // The root should always be enabled.
                else
                    ((Behaviour)this).enabled = true;   // Disable this state as a Behaviour to avoid set parent.enabledState as null.
            }
        }
        #endregion BehaviourMachine Callbacks
    }
}