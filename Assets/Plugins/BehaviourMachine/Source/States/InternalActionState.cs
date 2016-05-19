//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {

    /// <summary>
    /// Easy visual scripting component.
    /// Add action and condition nodes to be executed when the state is enabled. 
    /// Every node before the EveryFrame runs once, every node after the EveryFrame runs every frame.
    /// </summary>
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class InternalActionState : InternalStateBehaviour, INodeOwner {

        #region Members
        [HideInInspector]
        [SerializeField]
        NodeSerialization m_NodeSerialization;

        [TextAreaAttribute("Description...", 3)]
        [SerializeField]
        protected string m_Description;

        [SerializeField]
        bool m_IgnoreTimeScale = false;

        [SerializeField]
        bool m_ResetStatusOnDisable = true;

        [System.NonSerialized]
        ActionNode[] m_Nodes;

        [System.NonSerialized]
        float m_DeltaTimeAmount = 0f;

        [System.NonSerialized]
        OnEnable m_OnEnable;

        [System.NonSerialized]
        Update m_Update;

        [System.NonSerialized]
        FixedUpdate m_FixedUpdate;

        [System.NonSerialized]
        OnGUI m_OnGUI;

        [System.NonSerialized]
        bool m_IsDirty = false;

        bool m_NodesWaked = false;

        bool m_NodesEnabled = false;
        #endregion Members
    	

        #region Properties
        /// <summary>
        /// The state is ignoring time scale?
        /// </summary>
        public bool ignoreTimeScale {get {return m_IgnoreTimeScale;} set {m_IgnoreTimeScale = value;}}

        /// <summary>
        /// If False then the ActionState will keep the execution state when disabled; otherwise the nodes's execution will be reset when the ActionState becomes disabled.
        /// </summary>
        public bool resetStatusOnDisable {get {return m_ResetStatusOnDisable;} set {m_ResetStatusOnDisable = value;}}

        /// <summary>
        /// If ignoreTimeScale is true then returns the real delta time; otherwise returns Time.deltaTime.
        /// </summary>
        public float deltaTime {get {return m_IgnoreTimeScale ? InternalGlobalBlackboard.realDeltaTime + m_DeltaTimeAmount : Time.deltaTime + m_DeltaTimeAmount;}}

        /// <summary>
        /// Amount of time to add to deltaTime, usefull for Coroutines.
        /// </summary>
        public float deltaTimeAmount {get {return m_DeltaTimeAmount;} set {m_DeltaTimeAmount = value;}}

        /// <summary>
        /// Returns the state description.
        /// </summary>
        public string description {get {return m_Description;}}

        /// <summary>
        /// Returns the OnGUI node; if it exists.
        /// </summary>
        public OnGUI onGUINode {get {return m_OnGUI;}}

        /// <summary>
        /// Returns true if the ActionState's nodes need to be reloaded (Editor Only).
        /// </summary>
        public bool isDirty {get {return m_IsDirty;}}
        #endregion Properties


        #region Events
        /// <summary>
        /// Unity timed events.
        /// </summary>
        public event SimpleCallback onEnable, onDisable, start, onDestroy;
        #endregion Events

        
        #region Private Methods
        /// <summary>
        /// Create the runtime lists of nodes.
        /// </summary>
        void CreateRuntimeListsOfNodes () {
            // iterator
            int i;

            // OnEnable Nodes
            var onEnableList = new List<ActionNode>();
            
            for (i = 0; i < m_Nodes.Length; i++) {
                if (!(m_Nodes[i] is Update))
                    onEnableList.Add(m_Nodes[i]);
                else
                    break;
            }

            if (onEnableList.Count > 0) {
                m_OnEnable = ActionNode.CreateInstance(typeof(OnEnable), gameObject, this) as OnEnable;
                m_OnEnable.SetChildren(onEnableList.ToArray());
            }
            else
                m_OnEnable = null;

            // Update Nodes
            var updateList = new List<ActionNode>();
            // FixedUpdate Nodes
            var fixedUpdateList = new List<ActionNode>();
            // GUI Nodes
            var onGuiList = new List<ActionNode>();

            for (int j = i + 1; j < m_Nodes.Length; j++) {

                // OnGUI
                if (m_Nodes[j] is IGUINode) {
                    onGuiList.Add(m_Nodes[j]);
                }
                // FixedUpdate
                else if (m_Nodes[j] is IFixedUpdateNode) {
                    fixedUpdateList.Add(m_Nodes[j]);
                }
                // Update
                else {
                    updateList.Add(m_Nodes[j]);
                }
            }

            // FixedUpdate
            if (fixedUpdateList.Count > 0) {
                m_FixedUpdate = ActionNode.CreateInstance(typeof(FixedUpdate), gameObject, this) as FixedUpdate;
                // Call Reset if it's not in editor
                if (!Application.isEditor)
                    m_FixedUpdate.Reset();

                m_FixedUpdate.SetChildren(fixedUpdateList.ToArray());
            }
            else
                m_FixedUpdate = null;

            // Update
            if (updateList.Count > 0) {
                m_Update = ActionNode.CreateInstance(typeof(Update), gameObject, this) as Update;
                // Call Reset if it's not in editor
                if (!Application.isEditor)
                    m_Update.Reset();

                m_Update.SetChildren(updateList.ToArray());
            }
            else
                m_Update = null;

            // OnGUI
            if (onGuiList.Count > 0) {
                m_OnGUI = ActionNode.CreateInstance(typeof(OnGUI), gameObject, this) as OnGUI;
                // Call Reset if it's not in editor
                if (!Application.isEditor)
                    m_OnGUI.Reset();
                    
                m_OnGUI.SetChildren(onGuiList.ToArray());
            }
            else
                m_OnGUI = null;
        }

        /// <summary>
        /// Returns a unique node id.
        /// <returns>Unique node id.<returns>
        /// </summary>
        int GetNewUniqueID () {
            // Create the list of node id
            var ids = new List<int>();
            foreach (ActionNode node in GetNodes()) {
                ids.Add(node.instanceID);
            }

            // The new unique id
            int id = 1;

            while (true) {
                if (!ids.Contains(id))
                    return id;
                id++;
            }
        }


        #region Node Callbacks
        /// <summary>
        /// Call the Awake method in nodes.
        /// </summary>
        void WakeNodes () {
            m_NodesWaked = true;

            if (m_OnEnable != null)
                m_OnEnable.Awake();
            if (m_Update != null)
                m_Update.Awake();
            if (m_FixedUpdate != null)
                m_FixedUpdate.Awake();
            if (m_OnGUI != null)
                m_OnGUI.Awake();

            for (int i = 0; i < m_Nodes.Length; i++)
                m_Nodes[i].Awake();
        }

        /// <summary>
        /// Call the OnEnable method in nodes.
        /// </summary>
        void OnEnableNodes () {
            m_NodesEnabled = true;

            // Call OnEnable in function nodes
            if (m_OnEnable != null)
                m_OnEnable.OnEnable();
            if (m_Update != null)
                m_Update.OnEnable();
            if (m_FixedUpdate != null)
                m_FixedUpdate.OnEnable();
            if (m_OnGUI != null)
                m_OnGUI.OnEnable();

            // Call OnEnable in nodes
            if (m_Nodes != null) {
                for (int i = 0; i < m_Nodes.Length; i++)
                    m_Nodes[i].OnEnable();
            }
        }

        /// <summary>
        /// Restart nodes if needed and call the OnDisable method.
        /// </summary>
        void OnDisableNodes () {
            // Restart nodes?
            if (m_ResetStatusOnDisable) {
                if (m_OnEnable != null)
                    m_OnEnable.ResetStatus();
                if (m_Update != null)
                    m_Update.ResetStatus();
                if (m_FixedUpdate != null)
                    m_FixedUpdate.ResetStatus();
                if (m_OnGUI != null)
                    m_OnGUI.ResetStatus();
            }

            // Disable function nodes
            if (m_OnEnable != null)
                m_OnEnable.OnDisable();
            if (m_Update != null)
                m_Update.OnDisable();
            if (m_FixedUpdate != null)
                m_FixedUpdate.OnDisable();
            if (m_OnGUI != null)
                m_OnGUI.OnDisable();

            // Disable nodes
            if (m_Nodes != null) {
                for (int i = 0; i < m_Nodes.Length; i++)
                    m_Nodes[i].OnDisable();
            }

            m_NodesEnabled = false;
        }

        /// <summary>
        /// Force Restart nodes and call the OnDisable method.
        /// </summary>
        void ForceOnDisableNodes () {
            // Reset status and call OnDisable in function nodes
            if (this.enabled && Application.isPlaying) {
                if (m_OnEnable != null) {
                    m_OnEnable.ResetStatus();
                    m_OnEnable.OnDisable();
                }
                if (m_Update != null) {
                    m_Update.ResetStatus();
                    m_Update.OnDisable();
                }
                if (m_FixedUpdate != null) {
                    m_FixedUpdate.ResetStatus();
                    m_FixedUpdate.OnDisable();
                }
                if (m_OnGUI != null) {
                    m_OnGUI.ResetStatus();
                    m_OnGUI.OnDisable();
                }
            }

            // Call OnDisable in nodes
            if (m_Nodes != null) {
                for (int i = 0; i < m_Nodes.Length; i++)
                    m_Nodes[i].OnEnable();
            }
        }
        #endregion Node Callbacks
        #endregion Private Methods


        #region Unity Callbacks
        /// <summary>
        /// Unity callback called when the script instance is being loaded.
        /// Check link hideFlags. Only used in editor.
        /// </summary>
        public virtual void Awake () {
            // Load Nodes
            if (m_Nodes == null)
                LoadNodes();

            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            // Call Awake in nodes
            this.WakeNodes();
        }

        /// <summary>
        /// OnEnable is called when the tree becomes enabled and active.
        /// Call OnEnable Nodes.
        /// </summary>
        public virtual void OnEnable () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            // Load Nodes?
            if (m_Nodes == null)
                LoadNodes();

            // Call OnEnable in nodes
            if (!m_NodesEnabled)
                this.OnEnableNodes();

            // Call onEnable event
            if (onEnable != null)
                onEnable();
        }

        /// <summary>
        /// OnDisable is called when the state becomes disabled or inactive.
        /// Call OnDisable Nodes.
        /// </summary>
        public virtual void OnDisable () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            // Call onDisable event
            if (onDisable != null)
                onDisable();

            // Disable nodes
            this.OnDisableNodes();
        }

        // /// <summary>
        // /// Update is called every frame, if the tree is enabled.
        // /// Call Update nodes.
        // /// </summary>
        // void Update () {
        //     #if UNITY_EDITOR
        //     if (!Application.isPlaying)
        //         return;
        //     #endif

        //     // Call update event
        //     if (update != null)
        //         update();
        // }

        // /// <summary>
        // /// FixedUpdate is called every fixed framerate frame, if the tree is enabled.
        // /// Call FixedUpdate nodes.
        // /// </summary>
        // void FixedUpdate () {
        //     #if UNITY_EDITOR
        //     if (!Application.isPlaying)
        //         return;
        //     #endif

        //     // Call fixedUpdate event
        //     if (fixedUpdate != null)
        //         fixedUpdate();
        // }

        // /// <summary>
        // /// FixedUpdate is called every frame just before Update, if the tree is enabled.
        // /// </summary>
        // void LateUpdate () {
        //     #if UNITY_EDITOR
        //     if (!Application.isPlaying)
        //         return;
        //     #endif

        //     // Call lateUpdate event
        //     if (lateUpdate != null)
        //         lateUpdate();

        //     m_LastRealTime = Time.realtimeSinceStartup;
        // }

        /// <summary>
        /// Unity callback called when the user hits the Reset button in the Inspector's context menu or when adding the component the first time (Editor only).
        /// Create OnEnable and Update nodes.
        /// </summary>
        public override void Reset () {
            base.Reset();

            LoadNodes();

            if (m_Nodes.Length == 0) {
                // Create update
                m_Nodes = new ActionNode[0];
                // Add update
                this.AddNode(typeof(EveryFrame));
                // Save nodes
                SaveNodes();
            }
        }
        #endregion Unity Callbacks

        
        #region Public Methods
        /// <summary> 
        /// BehaviourMachine callback to update the members.
        /// </summary>
        public override void UpdateLogic () {
            base.UpdateLogic();
            
            m_IsDirty = true;
        }

        /// <summary>
        /// Returns all nodes.
        /// <returns>All nodes in this ActionState.</returns>
        /// </summary>
        public ActionNode[] GetNodes () {
            if (m_Nodes == null)
                LoadNodes();
            return m_Nodes;
        }

        /// <summary>
        /// Marks the state as changed.
        /// This method needs to be called whenever the nodes hierarchy has changed.
        /// </summary>
        public void HierarchyChanged () {
            this.ForceOnDisableNodes();
            this.CreateRuntimeListsOfNodes();

            if (Application.isPlaying && this.enabled)
                this.OnEnableNodes();
        }

        /// <summary>
        /// Returns the node that has the supplied id.
        /// <param name="nodeID">The taget node id.</param>
        /// <returns>The node with id equals to nodeID.</returns>
        /// </summary>
        public ActionNode GetNode (int nodeID) {
            var nodes = GetNodes ();
            for (int i = 0; i < nodes.Length; i++)  {
                if (nodes[i].instanceID == nodeID)
                    return nodes[i];
            }
            return null;
        }

        /// <summary>
        /// Returns the node index.
        /// <param name="node">The taget node.</param>
        /// <returns>The index of the supplied node.</returns>
        /// </summary>
        public int GetIndex (ActionNode node) {
            var nodes = GetNodes();
            return nodes != null ? System.Array.IndexOf(nodes, node) : -1;
        }

        /// <summary>
        /// Adds a new node to the tree.
        /// <param name="type">The type of the new node.</param>
        /// <returns>The new created node.</returns>
        /// </summary>
        public ActionNode AddNode (System.Type type) {
            var node = ActionNode.CreateInstance(type, gameObject, this);

            if (node != null) {
                NodeSerialization.idField.SetValue(node, GetNewUniqueID());
                GetNodes();

                // Add node
                var nodes = new List<ActionNode>(m_Nodes);
                nodes.Add(node);
                m_Nodes = nodes.ToArray();

                this.HierarchyChanged();
            }

            return node;
        }

        /// <summary>
        /// Removes node from tree.
        /// <param name="node">The node to be removed.</param>
        /// </summary>
        public void RemoveNode (ActionNode node) {
            // It's not the Update node?
            if (!(node is Update)) {
                GetNodes();

                // Remove node
                var nodes = new List<ActionNode>(m_Nodes);
                nodes.Remove(node);
                m_Nodes = nodes.ToArray();

                this.HierarchyChanged();
            }
        }

        /// <summary>
        /// Saves nodes in m_NodeSerialization.
        /// </summary>
        public void SaveNodes () {
            // Debug.Log("Saving Nodes " + name);
            GetNodes();
            m_NodeSerialization.SaveNodes(m_Nodes, gameObject, this);
        }

        /// <summary>
        /// Loads nodes from m_NodeSerialization.
        /// </summary>
        public void LoadNodes () {
            // Debug.Log("Loading Nodes " + name);

            // Disable nodes?
            if (m_NodesEnabled)
                this.ForceOnDisableNodes();

            if (m_NodeSerialization == null)
                m_NodeSerialization = new NodeSerialization();

            m_Nodes = m_NodeSerialization.LoadNodes(gameObject, this);

            if (m_NodeSerialization.resaveNodes) {
                SaveNodes();
            }

            this.CreateRuntimeListsOfNodes();

            m_IsDirty = false;

            // Awake nodes?
            if (m_NodesWaked)
                this.WakeNodes();

            // Reenabled nodes?
            if (m_NodesEnabled)
                this.OnEnableNodes();
        }

        /// <summary>
        /// Clears all node data.
        /// </summary>
        public void Clear () {
            m_NodeSerialization.Clear();
        }
        #endregion Public Methods
    }
}