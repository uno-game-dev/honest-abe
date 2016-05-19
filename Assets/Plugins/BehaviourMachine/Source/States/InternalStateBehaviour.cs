//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {

    /// <summary> 
    /// The available colors for a state.
    /// </summary>
    public enum StateColor {
        Grey,
        Blue,
        // Cyan,
        Green,    
        Yellow,
        // Orange,
        Red
    }

    /// <summary> 
    /// Base class for states. You can extend this class to create your own custom state.
    /// </summary>
    [AddComponentMenu("")]
    public class InternalStateBehaviour : MonoBehaviour {

        #region Static
        protected static bool s_Refresh = false;

        public static bool refresh {get {return InternalStateBehaviour.s_Refresh;}}

        public static void ResetRefresh () {InternalStateBehaviour.s_Refresh = false;}

        public static event StateCallback onUpdateHideFlag;
        #endregion Static

        
        #region Members
        [HideInInspector]
        [SerializeField]
        InternalBlackboard m_Blackboard;

        [ParentProperty]
        [SerializeField]
        protected ParentBehaviour m_Parent;
        
        [System.NonSerialized]
        ParentBehaviour m_LastParent;
        
        [SerializeField]
        string m_StateName;
        
        [StateColor]
        [SerializeField]
        protected StateColor m_Color;
        
        [HideScript]
        [SerializeField]
        protected bool m_HideScript = true;
        
        [HideInInspector]
        [SerializeField]
        Vector2 m_Position = Vector2.zero;
        
        [HideInInspector]
        [SerializeField]
        StateTransition[] m_Transitions = new StateTransition[0];
        
        [HideInInspector]
        [SerializeField]
        int m_CachedInstanceID = 0;
        #endregion Members

        
        #region Visual Debugging
        /// <summary> 
        /// Event fired when a transition is performed.
        /// </summary>
        #if UNITY_EDITOR
        public event TransitionCallback onTransitionPerformed;
        #endif

        /// <summary> 
        /// Function to fire onTransitionPerformed event in subclasses.
        /// <param name="transition">The transition that was performed.</param>
        /// </summary>
        #if UNITY_EDITOR
        protected void OnTransition (StateTransition transition) {
            if (onTransitionPerformed != null)
                onTransitionPerformed(transition);
        }
        #endif
        #endregion Visual Debugging

        
        #region Properties
        /// <summary> 
        /// The parent behaviour.
        /// </summary>
        public ParentBehaviour parent {get {return m_Parent;} 
            set {
                if (value != this && (value == null || (value.gameObject == this.gameObject && !value.IsAncestor(this as ParentBehaviour)))) {
                    base.enabled = value == null;

                    ParentBehaviour oldParent = m_Parent;
                    m_Parent = value;

                    // The parent changed?
                    if (oldParent != value && oldParent != null) {
                        // Call UpdateLogic in old siblings
                        foreach (InternalStateBehaviour state in oldParent.states)  
                            state.UpdateLogic();

                        // Update logic in this state
                        this.UpdateLogic();
                    }

                    StateSetDirty();
                    InternalStateBehaviour.s_Refresh = true;
                }
            }
        }

        /// <summary> 
        /// The color of the state in the BehaviourWindow. Editor only.
        /// </summary>
        public StateColor color {get {return m_Color;} set {m_Color = value;}}

        /// <summary> 
        /// Returns the hide flag.
        /// </summary>
        public bool hideFlag {get {return m_HideScript;}}

        /// <summary> 
        /// Returns the state position in the fsm grid.
        /// </summary>
        public Vector2 position {get {return m_Position;} set {m_Position = value;}}

        /// <summary> 
        /// Returns the topmost parent behaviour.
        /// </summary>
        public ParentBehaviour root {get {return m_Parent == null ? (this as ParentBehaviour) : m_Parent.root;}}

        /// <summary> 
        /// Returns the topmost StateMachine.
        /// </summary>
        public InternalStateMachine rootStateMachine {get {return fsm == null ? (this as InternalStateMachine) : m_Parent.rootStateMachine;}}

        /// <summary> 
        /// Returns the topmost BehaviourTree.
        /// </summary>
        public InternalBehaviourTree rootBehaviourTree {get {return tree == null ? (this as InternalBehaviourTree) : m_Parent.rootBehaviourTree;}}

        /// <summary> 
        /// Returns true if the object is the topmost state in the hierarchy; otherwise returns false.
        /// </summary>
        public bool isRoot {get {return m_Parent == null;}}

        /// <summary> 
        /// Returns the blackboard.
        /// </summary>
        public InternalBlackboard blackboard {
            get {
                if (m_Blackboard == null)
                    m_Blackboard = GetComponent<InternalBlackboard>();
                return m_Blackboard;
            }
        }

        /// <summary> 
        /// The name of the state.
        /// </summary>
        public string stateName {get {return m_StateName;} set {m_StateName = value;}}

        /// <summary> 
        /// Returns the full state name, includes its parent hierarchy.
        /// </summary>
        public string fullStateName {get {return m_Parent != null ? (m_Parent.fullStateName + "/" + m_StateName) : m_StateName;}}

        /// <summary> 
        /// Returns the finite state machine that has this state.
        /// </summary>
        public InternalStateMachine fsm {get {return m_Parent as InternalStateMachine;}}

        /// <summary> 
        /// Returns the tree that has this state.
        /// </summary>
        public InternalBehaviourTree tree {get {return m_Parent as InternalBehaviourTree;}}

        /// <summary> 
        /// Hides the Behaviour.enabled property.
        /// You can cast a InternalStateBehaviour to a MonoBehaviour/Behaviour to bypass this property and access the default enabled property.
        /// Be advised that bypass this property can lead to strange behaviours.
        /// </summary>
        public new bool enabled {get {return base.enabled;} 
            set {
                if (parent == null)
                    base.enabled = value;
                else {
                    if (value)
                        parent.EnableState(this);
                    else
                        parent.DisableState(this);
                }
            }
        }

        /// <summary> 
        /// Returns the state transitions. 
        /// Never returns null.
        /// </summary>
        public  StateTransition[] transitions {
            get {
                if (m_Transitions == null)
                    m_Transitions = new StateTransition[0];
                return m_Transitions;
            }
        }
        #endregion Properties

        
        #region Private Methods
        /// <summary>
        /// Saves the last parent.
        /// </summary> 
        void SaveLastParent () {
            m_LastParent = m_Parent;
        }
        #endregion Private Methods
        
        
        #region Public Methods
        /// <summary>
        /// Returns the full state name relative to the parent parent.
        /// <param name="parent"></param>
        /// <returns>the full state name relative to supplied parent.</returns>
        /// </summary>
        public string GetFullStateNameRelativeTo (ParentBehaviour parent) {
            return parent != this && m_Parent != null ? (m_Parent.GetFullStateNameRelativeTo(parent) + "/" + m_StateName) : m_StateName;
        }

        /// <summary> 
        /// Returns True if the supplied parent is an ancestor of the state.
        /// <param name="parent">The parent to test.</param>
        /// <returns>True if the node is in the supplied parent hierarchy; False otherwise.</returns>
        /// </summary>
        public bool IsAncestor (ParentBehaviour parent) {
            for  (var grandfather = m_Parent; grandfather != null; grandfather = grandfather.parent) {
                if (grandfather == parent)
                    return true;
            }
            return false;
        }

        /// <summary> 
        /// Searches for the supplied eventName in the state transitions; if found a transition between states will be performed.
        /// <param name="eventName">The name of the event.</param>
        /// <returns>True if the event was used; False otherwise.</returns>
        /// </summary>
        public bool SendEvent (string eventName) {
            if (enabled) {
                // The eventName is a valid string and the state has a valid blackboard?
                if (!string.IsNullOrEmpty(eventName) && blackboard != null) {
                    FsmEvent fsmEvent = null;

                    // Get the fsmEvent
                    InternalBlackboard myBlackboard = blackboard;
                    if (myBlackboard != null)
                        fsmEvent = myBlackboard.GetFsmEvent(eventName);
                    // Try to get the fsmEvent in the GlobalBlackboard
                    if (fsmEvent == null && InternalGlobalBlackboard.Instance != null)
                        fsmEvent = InternalGlobalBlackboard.Instance.GetFsmEvent(eventName);
                    
                    // The fsmEvent is valid?
                    if (fsmEvent != null)
                        return SendEvent(fsmEvent.id);
                }
                else
                    throw new System.ArgumentException("Parameter cannot be null or empty", "eventName");
            }
            return false;
        }

        /// <summary> 
        /// Use the event to change the enabled state in the StateMachine.
        /// The event is processed top-down, starting from the root InternalStateBehaviour. 
        /// <param name="eventID">The id of the event.</param>
        /// <returns>Returns True if the event was used; False otherwise.</returns>
        /// </summary>
        public bool SendEvent (int eventID) {
            // Try to call ProcessEvent on the root parent
            ParentBehaviour rootParent = this.root;
            if (rootParent != null)
                return rootParent.ProcessEvent(eventID);

            // Process event
            return ProcessEvent(eventID);
        }

        /// <summary> 
        /// Searches for the supplied eventID in the transitions; if found a transition between states will be performed. 
        /// <param name="eventID">The id of the event.</param>
        /// <returns>Returns True if the event was used; False otherwise.</returns>
        /// </summary>
        public virtual bool ProcessEvent (int eventID) {
            // The state is enabled?
            if (enabled) {
                // State transitions
                for (int i = 0; i < m_Transitions.Length; i++) {
                    // Searches for the transition that has the supplied eventID
                    if (m_Transitions[i].eventID == eventID) {
                        // Get the destination state
                        var destination = m_Transitions[i].destination;
                        
                        // The destination state is a valid state?
                        if (destination != null) {

                            #if UNITY_EDITOR
                            // Reports transition
                            if (Application.isEditor && onTransitionPerformed != null)
                                onTransitionPerformed(m_Transitions[i]);
                            #endif

                            // Change state
                            destination.enabled = true;
                            return true;
                        }
                        // EventID already "used"; finishing loop.
                        break;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Call SendEvent on all enabled root FSM in the scene.
        /// Please note that this function is very slow. It is not recommended to use this function every frame.
        /// <param name="eventName">The name of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public static bool SendEventToAll (string eventName) {
            return InternalBlackboard.SendEventToAll(eventName);
        }

        /// <summary>
        /// Call SendEvent on all enabled root FSM in the scene.
        /// Please note that this function is very slow. It is not recommended to use this function every frame.
        /// <param name="eventID">The id of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public static bool SendEventToAll (int eventID) {
            return InternalBlackboard.SendEventToAll(eventID);
        }

        /// <summary>
        /// Call SendEvent on all enabled root FSM in this game object or any of its children.
        /// <param name="eventName">The name of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool BroadcastEvent (string eventName) {
            return blackboard.BroadcastEvent(eventName);
        }

        /// <summary>
        /// Call SendEvent on all enabled root FSM in this game object or any of its children.
        /// <param name="eventID">The id of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool BroadcastEvent (int eventID) {
            return blackboard.BroadcastEvent(eventID);
        }

        /// <summary>
        /// Call SendEvent on all enabled root FSM in this game object and its ancestor.
        /// <param name="eventName">The name of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool SendEventUpwards (string eventName) {
            return blackboard.SendEventUpwards(eventName);
        }

        /// <summary>
        /// Call SendEvent on all enabled root FSM in this game object and its ancestor.
        /// <param name="eventID">The id of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool SendEventUpwards (int eventID) {
            return blackboard.SendEventUpwards(eventID);
        }

        /// <summary>
        /// Called by a StateNode inside a BehaviourTree or ActionNode.
        /// This let you use a InternalStateBehaviour as a task node.
        /// <returns>The execution status.</returns>
        /// <seealso cref="BehaviourMachine.StateNode" />
        /// </summary>
        public virtual Status OnTick () {
            return Status.Running;
        }
        
        #region Transitions
        /// <summary> 
        /// Adds a new transition.
        /// <param name="eventID">The event id of the new transition.</param>
        /// <returns>The created transition.</returns>
        /// </summary>
        public StateTransition AddTransition (int eventID) {
            var transitionList = new List<StateTransition>(m_Transitions);
            var transition = new StateTransition();
            transition.eventID = eventID;
            transitionList.Add(transition);
            m_Transitions = transitionList.ToArray();
            return transition;
        }

        /// <summary> 
        /// Removes a transition.
        /// <param name="transition">The transition to be removed.</param>
        /// </summary>
        public void RemoveTransition (StateTransition transition) {
            var transitionList = new List<StateTransition>(m_Transitions);
            if (transitionList.Contains(transition)) {
                transitionList.Remove(transition);
                m_Transitions = transitionList.ToArray();
            }

        }

        /// <summary> 
        /// Removes the transition in the supplied index from the state.
        /// <param name="transitionIndex">The index of the transition to be removed.</param>
        /// </summary>
        public void RemoveTransitionAt (int transitionIndex) {
            if (transitionIndex >= 0 && transitionIndex < m_Transitions.Length) {
                var transitionList = new List<StateTransition>(m_Transitions);
                transitionList.RemoveAt(transitionIndex);
                m_Transitions = transitionList.ToArray();
            }
        }
        #endregion Transitions

        
        #region Unity Callbacks
        /// <summary> 
        /// Unity callback called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Workaround to copy component values in editor.
        /// </summary>
        public virtual void OnValidate () {
            // Validate Blackboard
            if (m_Blackboard == null || m_Blackboard.gameObject != this.gameObject)
                m_Blackboard = GetComponent<InternalBlackboard>();
                
            // The component has been copied to a new game object?
            if (m_Parent == null || m_Parent.gameObject != gameObject) {
                UpdateLogic();
            }

            // Workaround to not disable the state in the playmode when editing the inspector
            if (Application.isPlaying && m_Parent != null && !m_Parent.IsEnabled(this))
                base.enabled = m_Parent == null;  // enables/disables the state.

            // The user has reverted to prefab values or pasted component values?
            if (m_CachedInstanceID != this.GetInstanceID()) {
                m_CachedInstanceID = this.GetInstanceID();
                UpdateLogic();
                InternalStateBehaviour.s_Refresh = true;
            }

            // The user has changed the parent property?
            if (m_LastParent != m_Parent) {
                InternalStateBehaviour.s_Refresh = true;
            }

            // Set Dirty
            StateSetDirty();
        }

        /// <summary> 
        /// Unity callback called when the user hits the Reset button in the Inspector's context menu or when adding the component for the first time (Editor only).
        /// Updates the logic parameters.
        /// </summary>
        public virtual void Reset () {
            UpdateLogic();

            // Try to restore last parent
            parent = m_LastParent;
        }

        /// <summary> 
        /// BehaviourMachine callback to update the members.
        /// </summary>
        public virtual void UpdateLogic () {
            // Validate Blackboard
            if (m_Blackboard == null)
                m_Blackboard = GetComponent<InternalBlackboard>();

            // Validate name
            if (string.IsNullOrEmpty(m_StateName)) {
                m_StateName = GetType().Name;
            }

            // Validate parent
            if (m_Parent != null && m_Parent.gameObject != gameObject) {
                m_Parent = m_LastParent != null && m_LastParent.gameObject == gameObject ? m_LastParent : null;
                this.enabled = m_Parent == null;
            }

            // Check for invalid events destinations
            for (int i = 0; i < m_Transitions.Length; i++) {
                // The destination state is not in this game object?
                var destination = m_Transitions[i].destination;
                if (destination != null && destination.parent != m_Parent)
                    m_Transitions[i].destination = null;
            }
        }
        #endregion Unity Callbacks
        
        /// <summary>
        /// Sets the state as dirty (editor only).
        /// </summary>
        public void StateSetDirty () {
            if (Application.isEditor) {
                if (InternalStateBehaviour.onUpdateHideFlag != null)
                    InternalStateBehaviour.onUpdateHideFlag(this);
                this.SaveLastParent();
            }
        }
        #endregion Public Methods
    }
}