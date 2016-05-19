//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace  BehaviourMachine {

    /// <summary>
    /// Interface used by the node to read and set values from the state owner.
    /// States that manage nodes should implement this interface.
    /// </summary>
    public interface INodeOwner {

        #region Properties
        /// <summary> 
        /// The owner is enabled?
        /// </summary>
        bool enabled {get; set;}

    	/// <summary>
        /// The tree is ignoring time scale?
        /// </summary>
        bool ignoreTimeScale {get; set;}

        /// <summary>
        /// If ignoreTimeScale is true then returns the real delta time; otherwise returns Time.deltaTime.
        /// </summary>
        float deltaTime {get;}

        /// <summary>
        /// Amount of time to add to deltaTime, usefull for Coroutines.
        /// </summary>
        float deltaTimeAmount {get; set;}

        /// <summary> 
        /// The parent behaviour.
        /// </summary>
        ParentBehaviour parent {get;}

        /// <summary> 
        /// Returns the topmost parent behaviour.
        /// </summary>
        ParentBehaviour root {get;}
        #endregion Properties


        #region Events
        /// <summary>
        /// Unity timed events.
        /// </summary>
        event SimpleCallback onEnable, onDisable, start, /*fixedUpdate, update, lateUpdate,*/ onDestroy;
        #endregion Events

        
        #region Methods
        /// <summary>
        /// Marks the state as changed.
        /// This method needs to be called whenever the nodes hierarchy has changed.
        /// </summary>
        void HierarchyChanged ();

        /// <summary>
        /// Returns the node index.
        /// <param name="node">The taget node.</param>
        /// <returns>The index of the supplied node.</returns>
        /// </summary>
        int GetIndex (ActionNode node);

        /// <summary> 
        /// Use the event to change the enabled state in the fsm.
        /// Searches for the supplied eventID in the transitions; if found a transition between states will be performed. 
        /// <param name="eventID">The id of the event.</param>
        /// <returns>Returns True if the event was used; False otherwise.</returns>
        /// </summary>
        bool SendEvent (int eventID);

        /// <summary>
        /// Adds a new node.
        /// <param name="type">The type of the new node.</param>
        /// <returns>The new created node.</returns>
        /// </summary>
        ActionNode AddNode (System.Type type);

        /// <summary>
        /// Saves nodes.
        /// </summary>
        void SaveNodes ();

        /// <summary>
        /// Loads nodes.
        /// </summary>
        void LoadNodes ();

        /// <summary>
        /// Clears all node data.
        /// </summary>
        void Clear ();
        #endregion Methods
    }
}