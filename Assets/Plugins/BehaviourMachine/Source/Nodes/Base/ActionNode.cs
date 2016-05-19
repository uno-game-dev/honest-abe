//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

// Comment the line bellow to remove the node status visual debugging
#define VISUAL_DEBUGGING

using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace BehaviourMachine {

    /// <summary>
    /// The running status of a node.
    /// </summary>
    public enum Status {
        /// <summary>
        /// The node is waiting to be ticked.
        /// </summary>
        Ready,

        /// <summary>
        /// Returned when a node is successfully executed.
        /// </summary>
        Success,

        /// <summary>
        /// Returned when a node execution fails.
        /// </summary>
        Failure,

        /// <summary>
        /// Returned when a node execution has error(s).
        /// </summary>
        Error,

        /// <summary>
        /// Returned when a node needs more than one frame to finish.
        /// </summary>
        Running
    }


    /// <summary>
    /// Base class for tree nodes.
    /// <seealso cref="BehaviourMachine.BranchNode" />
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    [Serializable]
    public abstract class ActionNode {

        #region Enums
        /// <summary>
        /// The components of a Vector3.
        /// <summary>
        public enum Vector3Component {
            x,
            y,
            z
        }

        /// <summary>
        /// Used in math operations.
        /// </summary>
        public enum Operation {
            Add,
            Subtract,
            Multiply,
            Divide,
            Min,
            Max
        }
        #endregion Enums


        #region Static Mehods
        /// <summary>
        /// Creates and returns a node of the supplied type.
        /// <param name="type">The node type to be created.</param>
        /// <param name="gameObject">The game object that owns the node.</param>
        /// <param name="nodeOwner">The node owner (usually a InternalStateBehaviour).</param>
        /// <returns>Returns new created node.</returns>
        /// </summary>
        public static ActionNode CreateInstance (Type type, GameObject gameObject, INodeOwner nodeOwner) {
            var newNode = Activator.CreateInstance(type) as ActionNode;
            if (newNode != null) {
                newNode.name = type.Name;
                newNode.Init(gameObject, nodeOwner);
                if (Application.isEditor)
                    newNode.Reset();
            }
            return newNode;
        }
        #endregion Static Mehods


        #region Visual Debuging
        public static event Action<ActionNode> onNodeTick;
        #endregion Visual Debuging


        #region Members
        Status m_Status = Status.Ready;
        BranchNode m_Branch = null;
        GameObject m_Self;
        INodeOwner m_Owner;

        /// <summary>
        /// The unique id of the node; this id is unique between nodes on the same tree.
        /// </summary>
        [HideInInspector]
        [Tooltip("The unique id of the node; this id is unique between nodes on the same tree")]
        public readonly int instanceID = 0;
        
        /// <summary>
        /// The name of the node.
        /// </summary>
        [Tooltip("The name of the node")]
        public string name = string.Empty;
        #endregion Members


        #region Properties
        /// <summary>
        /// The last status of the node.
        /// </summary>
        public Status status {
            get {return m_Status;}

            // protected set {
            //     m_Status = value;

            //     // #if UNITY_EDITOR && VISUAL_DEBUGGING
            //     // if (onNodeTick != null)
            //     //     onNodeTick(this);
            //     // #endif
            //     this.OnNodeTick();
            // }
        }

        /// <summary>
        /// Returns the game object that the owner is attached to.
        /// </summary>
        public GameObject self {get {return m_Self;}}

        /// <summary>
        /// Returns the owner of the node.
        /// </summary>
        public INodeOwner owner {get {return m_Owner;}}

        /// <summary>
        /// Returns the BehaviourTree that owns the node or null if it is an ActionState.
        /// </summary>
        public InternalBehaviourTree tree {get {return m_Owner as InternalBehaviourTree;}}

        /// <summary>
        /// Returns the ActionState that owns the node or null if it is a BehaviourTree.
        /// </summary>
        public InternalActionState actionState {get {return m_Owner as InternalActionState;}}

        /// <summary>
        /// The blackboard component used by this node.
        /// </summary>
        public InternalBlackboard blackboard {get {return m_Self != null ? m_Self.GetComponent<InternalBlackboard>() : null;}}

        /// <summary>
        /// Returns true if this is a rootNode; otherwise false.
        /// </summary>
        public bool isRoot {get {return m_Branch == null;}}

        /// <summary>
        /// Returns the topmost node.
        /// </summary>
        public ActionNode root {
            get {
                ActionNode root = this;
                while (root.branch != null)
                    root = root.branch;
                return root;
            }
        }

        /// <summary>
        /// The parent branch that owns this node.
        /// </summary>
        public BranchNode branch {get {return m_Branch;} set {m_Branch = value;}}
        #endregion Properties


        #region Private Methods
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        void OnNodeTick () {
            if (onNodeTick != null)
                onNodeTick(this);
        }
        #endregion Private Methods


        #region Public Methods
        /// <summary>
        /// Node initialization.
        /// Set the game object and the owner of the node.
        /// <param name="gameObject">The game object that owns the node.</param>
        /// <param name="nodeOwner">The state that owns the node.</param>
        /// </summary>
        public void Init (GameObject gameObject, INodeOwner nodeOwner) {
            m_Self = gameObject;
            m_Owner = nodeOwner;
        }

        /// <summary>
        /// Returns the node index in the tree.
        /// <returns>The node index; if the tree is null returns -1.</returns>
        /// </summary>
        public int GetIndex () {
            return m_Owner != null ? m_Owner.GetIndex(this) : -1;
        }

        /// <summary>
        /// Returns a copy of this node.
        /// <param name = "newOwner">The owner of the new node.</param>
        /// <returns>The copy of the node.</returns>
        /// </summary>
        public virtual ActionNode Copy (INodeOwner newOwner) {
            // Copy node
            var copy = newOwner.AddNode(GetType());

            // Copy SerializedFields
            var fields = NodeSerialization.GetSerializedFields(copy.GetType());
            for (int i = 0; i < fields.Length; i++) {
                // Do not copy the instanceID field
                if (fields[i].Name != "instanceID")
                    fields[i].SetValue(copy, fields[i].GetValue(this));
            }

            return copy;
        }


        #region BehaviourMachine Callbacks
        /// <summary>
        /// The main entry of the node.
        /// This function is called when the node should be executed.
        /// Override this function if you want full control over the node execution.
        /// Overriding this function will prevent the functions Start, Update and End from being called.
        /// </summary>
        public void OnTick () {
            if (m_Status != Status.Running)
                this.Start();

            m_Status = this.Update();

            if (m_Status != Status.Running)
                this.End();

            this.OnNodeTick();
        }

        /// <summary>
        /// This function is called by the editor.
        /// Used by GUI nodes to draw controls in the editor.
        /// </summary>
        public virtual void EditorOnTick () {}

        /// <summary>
        /// Called when the owner (BehaviourTree or ActionState) is Awaked.
        /// </summary>
        public virtual void Awake () {}

        /// <summary>
        /// Called when the owner (BehaviourTree or ActionState) is enabled.
        /// </summary>
        public virtual void OnEnable () {}

        /// <summary>
        /// This function is called when the node starts its execution.
        /// Only called if the OnTick method is not overridden.
        /// </summary>
        public virtual void Start () {}

        /// <summary>
        /// This function is called when the node is in execution.
        /// Only called if the OnTick method is not overridden.
        /// </summary>
        public abstract Status Update ();

        /// <summary>
        /// This function is called when the node ends its execution.
        /// Only called if the OnTick method is not overridden.
        /// </summary>
        public virtual void End () {}

        /// <summary>
        /// Called when the owner (BehaviourTree or ActionState) is disabled.
        /// </summary>
        public virtual void OnDisable () {}

        /// <summary>
        /// This function is called to reset the default values of the node.
        /// </summary>
        public virtual void Reset () {}

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// </summary>
        public virtual void OnValidate () {}

        /// <summary>
        /// Reset the node status. 
        /// This function is called by Priority nodes to reset the status of their children.
        /// Calls End().
        /// <seealso cref="BehaviourMachine.PrioritySelector" />
        /// <seealso cref="BehaviourMachine.PrioritySequence" />
        /// <summary>
        public virtual void ResetStatus () {
            if (m_Status == Status.Running) {
                this.End();
                m_Status = Status.Ready;
            }
        }
        #endregion BehaviourMachine Callbacks


        /// <summary>
        /// Prints a message to the Unity console.
        /// <param name="message">The message to be printed.</param>
        /// </summary>
        public void print (object message) {
            Debug.Log(message, this.self);
        }
        #endregion Public Methods
    }
}
