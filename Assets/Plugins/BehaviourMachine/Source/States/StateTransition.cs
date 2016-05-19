//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary> 
    /// A class that stores a transition between states.
    /// <seealso cref="BehaviourMachine.InternalStateBehaviour" />
    /// <seealso cref="BehaviourMachine.FsmEvent" />
    /// </summary>
    [System.Serializable]
    public class StateTransition {

    	#region Members
        [HideInInspector]
        [SerializeField]
        InternalStateBehaviour m_Destination = null;
        
        [HideInInspector]
        [SerializeField]
        int m_EventID;
        #endregion Members

        
        #region Properties
        /// <summary> 
        /// Returns the destination state of the transition.
        /// </summary>
        public InternalStateBehaviour destination {get {return m_Destination;} set {if (!(value is InternalAnyState)) m_Destination = value;}}

        /// <summary> 
        /// Returns the event id associated with the transition.
        /// </summary>
        public int eventID {get {return m_EventID;} set {m_EventID = value;}}
        #endregion Properties
    }
}
