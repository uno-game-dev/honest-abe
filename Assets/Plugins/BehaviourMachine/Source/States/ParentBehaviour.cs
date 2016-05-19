//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
    /// <summary> 
    /// Base class for a state that manages other states.
    /// <seealso cref="BehaviourMachine.StateMachine" />
    /// <seealso cref="BehaviourMachine.InternalBehaviourTree" />
    /// </summary>
    [ExecuteInEditMode]
    public abstract class ParentBehaviour : InternalStateBehaviour {

        #region Members
        [TextAreaAttribute("Description...", 3)]
        [SerializeField]
        protected string m_Description;
        #endregion Members

        
        #region Properties
        /// <summary> 
        /// Returns the states in this parent.
        /// </summary>
        public List<InternalStateBehaviour> states {
            get {
                var states = new List<InternalStateBehaviour>();
                var allStates = GetComponents<InternalStateBehaviour>();
                for (int i = 0; i < allStates.Length; i++) {
                    if (allStates[i].parent == this)
                        states.Add(allStates[i]);
                }
                return states;
            }
        }

        /// <summary>
        /// Returns the state description.
        /// </summary>
        public string description {get {return m_Description;}}
        #endregion Properties


        #region Enabled State
        /// <summary>
        /// Callback called by a child InternalStateBehaviour to enabled it.
        /// </summary>
        public void EnableState (InternalStateBehaviour childState) {
            if (childState != null && childState.parent == this && !(childState is InternalAnyState)) {
                // Call the Add callback
                OnEnableState(childState);

                // Call the OnEnable callback on the new Enabled state
                if (!childState.enabled)
                    ((MonoBehaviour)childState).enabled = true;

                // If this state is disabled then enable it
                if (!this.enabled)
                    this.enabled = true;
            }
        }

        /// <summary>
        /// Callback called by a child InternalStateBehaviour to disabled it.
        /// </summary>
        public void DisableState (InternalStateBehaviour childState) {
            if (childState != null && this.IsEnabled(childState) && !(childState is InternalAnyState)) {
                // Call the Remove callback
                this.OnDisableState(childState);

                // Call the OnDisable callback on the childState
                if (childState.enabled)
                    ((MonoBehaviour)childState).enabled = false;
            }
        }

        /// <summary>
        /// Callback called when a new child state is enabled.
        /// </summary>
        protected abstract void OnEnableState (InternalStateBehaviour childState);

        /// <summary>
        /// Callback called when a child enabled state is removed.
        /// </summary>
        protected abstract void OnDisableState (InternalStateBehaviour childState);

        /// <summary>
        /// Returns true if the supplied child state is enabled; false otherwise.
        /// <param name="childState">A child state of the ParentBehaviour.</param>
        /// <returns>.</returns>
        /// </summary>
        public abstract bool IsEnabled (InternalStateBehaviour childState);
        #endregion Enabled State

        
        #region Add State
        #if UNITY_4 || UNITY_4_1 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
        /// <summary> 
        /// Creates and adds a new state to the fsm.
        /// <param name = "className">The class name of the new state.</param>
        /// <returns>The newly created state; or null if the state could not be created.</returns>
        /// </summary>
        public InternalStateBehaviour AddState (string className) {
            var newState = gameObject.AddComponent(className) as InternalStateBehaviour;
            if (newState != null)
                newState.parent = this;
            return newState;
        }
        #endif

        /// <summary>
        /// Creates and adds a new state to the fsm.
        /// <param name = "type">The class type of the new state.</param>
        /// <returns>The newly created state; or null if the state could not be created.</returns>
        /// </summary>
        public InternalStateBehaviour AddState (System.Type type) {
            var newState = gameObject.AddComponent(type) as InternalStateBehaviour;
            if (newState != null)
                newState.parent = this;
            return newState;
        }

        /// <summary> 
        /// Creates and adds a new state to the fsm.
        /// <returns>The newly created state; or null if the state could not be created.</returns>
        /// </summary>
        public T AddState<T>() where T : BehaviourMachine.InternalStateBehaviour {
            var newState = gameObject.AddComponent<T>();
            if (newState != null)
                newState.parent = this;
            return newState;
        }
        #endregion Add State

        
        #region Unity Callbacks
        /// <summary> 
        /// Unity callback called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// </summary>
        public override void OnValidate () {
            // Validate parent
            if (parent == this || (parent != null && parent.IsAncestor(this)))
                parent = null;

            base.OnValidate();
        }

        /// <summary>
        /// Unity callback called when the parent will be destroyed.
        /// Workaround to call UpdateHideFlags in states.
        /// </summary>
        public virtual void OnDestroy () {
            #if UNITY_EDITOR
            if (Application.isEditor) {
                List<InternalStateBehaviour> states = this.states;
                for (int i = 0; i < states.Count; i++) {
                    if (states[i] != null) {
                        states[i].parent = null;
                        states[i].StateSetDirty();
                    }
                }
                s_Refresh = true;
            }
            #endif
        }
        #endregion Unity Callbacks

        /// <summary>
        /// Returns the enabled state name in this parent.
        /// <returns>The enabled state name.</returns>
        /// </summary>
        public abstract string GetEnabledStateName ();
    }
}