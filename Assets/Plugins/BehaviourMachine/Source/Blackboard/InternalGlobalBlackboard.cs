//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
namespace BehaviourMachine {

	/// <summary>
	/// Stores global variables and FsmEvents.
	/// The GlobalBlackboard component is attached to a prefab in the Resources folder and is instantiated during playmode.
	/// You should only use one GlobalBlackboard component/prefab per project.
	/// Create one by selecting "Tools/BehaviourMahine/Global Blackboard" from the Unity toolbar.
	/// </summary>
    [AddComponentMenu("")]
	public class InternalGlobalBlackboard : InternalBlackboard {

		#region Singleton
        static InternalGlobalBlackboard s_Instance;
        #if UNITY_EDITOR
		static InternalGlobalBlackboard s_ResourceInstance;
        #endif

		/// <summary> 
		/// Returns the unique instance of the GlobalBlackboard component.
		/// </summary> 
		public static InternalGlobalBlackboard Instance { 
			get {
                #if UNITY_EDITOR
                if (Application.isPlaying){
                #endif
                    #if UNITY_EDITOR
                    if (InternalGlobalBlackboard.s_Instance == null || InternalGlobalBlackboard.s_Instance == InternalGlobalBlackboard.s_ResourceInstance) {
                    #else
                    if (InternalGlobalBlackboard.s_Instance == null) {
                    #endif
                        InternalGlobalBlackboard.s_Instance = Object.FindObjectOfType(typeof(InternalGlobalBlackboard)) as InternalGlobalBlackboard;
                        if (InternalGlobalBlackboard.s_Instance == null) {
                            var prefab = Resources.Load("GlobalBlackboard") as GameObject;
                            // Instantiate or create global object
                            if (prefab != null) {
                                prefab = Instantiate(prefab) as GameObject;
                                InternalGlobalBlackboard.s_Instance = prefab != null ? prefab.GetComponent<InternalGlobalBlackboard>() : null;
                            }
                            // else
                            //     prefab = new GameObject("GlobalBlackboard", typeof(InternalGlobalBlackboard));
                            // InternalGlobalBlackboard.s_Instance = prefab != null ? prefab.GetComponent<InternalGlobalBlackboard>() : null;
                        }
                    }
                    return InternalGlobalBlackboard.s_Instance;
                #if UNITY_EDITOR
                }
                else {
                    if (InternalGlobalBlackboard.s_ResourceInstance == null) {
                        var prefab = Resources.Load("GlobalBlackboard") as GameObject;
                        InternalGlobalBlackboard.s_ResourceInstance = prefab != null ? prefab.GetComponent<InternalGlobalBlackboard>() : null;
                    }
                    return InternalGlobalBlackboard.s_ResourceInstance;
                }
                #endif
			}
		}
		#endregion Singleton


        #region RealDeltaTime
        static float s_LastRealTime = 0f;

        /// <summary>
        /// The real delta time.
        /// </summary>
        public static float realDeltaTime {get {return Time.realtimeSinceStartup - s_LastRealTime;}}
        #endregion RealDeltaTime

        
        #region Create GUICallback
        static bool s_CreateGUICallback;

        /// <summary>
        /// Creates the GUICallback component if an instance of the GlobalBlackboard exists or wait an instance to be created and then creates the GUICallbakc.
        /// </summary>
        public static void CreateGUICallback () {
            // The GlobalBlackboard does not has a GUICallback?
            if (s_Instance != null && !s_Instance.hasGUICallback)
                s_Instance.CreateGUICallbackIfNotExist();
            else
                s_CreateGUICallback = true;
        }
        #endregion Create GUICallback


        #region Events
        /// <summary>
        /// Unity timed events.
        /// </summary>
        public static event SimpleCallback fixedUpdate, update, lateUpdate;
        #endregion Events

		
        #region Members
		[HideInInspector]
		[SerializeField]
		GUICallback m_GUICallback;
		#endregion Members

        
        #region Properties
        public bool hasGUICallback {get {return m_GUICallback != null;}}
        #endregion Properties
		
        
        #region Blackboard Variables
		/// <summary>
        /// Returns a unique id for a new variable.
		/// The ids of the global variables are always negative.
        /// <returns>A new unique id; used to create new variables.</returns>
        /// </summary>
        protected override int GetUniqueID () {
            // Get variables
            var variables = this.variables;

            // Get a list of ids
            var ids = new List<int>(); 
            for (int i = 0; i < variables.Length; i++)
                ids.Add(variables[i].id);

            // Get a new unique id in the list
            int newId;
            do {
                newId = -1 * Mathf.Abs(System.Guid.NewGuid().GetHashCode());
            } while (ids.Contains(newId));

            return newId;
        }

        /// <summary>
        /// Adds a new system FsmEvent to the blackboard.
        /// <param name="name">The new system event name.</param>
        /// <param name="id">The new system event id.</param>
        /// <returns>The new system event.</returns>
        /// </summary>
        FsmEvent AddSystemEvent (string name, int id) {
            // Creates the variable
            var fsmEvent = new ConcreteFsmEvent(name, this, id, true);
            // Create the list
            var fsmEventList = new List<ConcreteFsmEvent>(m_ConcreteFsmEvents);
            // Add variable to the list
            fsmEventList.Add(fsmEvent);
            // Create a new array
            m_ConcreteFsmEvents = fsmEventList.ToArray();
            // Return the new variable
            return fsmEvent;
        }
		#endregion Blackboard Variables

		
        #region Unity Callbacks
        /// <summary> 
        /// Unity callback called when the user hits the Reset button in the Inspector's context menu or when adding the component for the first time (Editor only).
        /// Add system events.
        /// </summary>
		protected virtual void Reset () {
            m_Namespace = "BehaviourMachine";

			AddSystemEvent("APPLICATION_FOCUS", -1);
			AddSystemEvent("APPLICATION_PAUSE", -2);
			AddSystemEvent("APPLICATION_QUIT", -3);
			AddSystemEvent("BECAME_INVISIBLE", -4);
			AddSystemEvent("BECAME_VISIBLE", -5);
			AddSystemEvent("COLLISION_ENTER", -6);
			AddSystemEvent("COLLISION_ENTER_2D", -7);
			AddSystemEvent("COLLISION_EXIT", -8);
			AddSystemEvent("COLLISION_EXIT_2D", -9);
			AddSystemEvent("FINISHED", -10);
			AddSystemEvent("JOINT_BREAK", -11);
			AddSystemEvent("MOUSE_ENTER", -12);
			AddSystemEvent("MOUSE_EXIT", -13);
			AddSystemEvent("MOUSE_DOWN", -14);
			AddSystemEvent("MOUSE_UP", -15);
			AddSystemEvent("MOUSE_UP_BUTTON", -16);
			AddSystemEvent("TRIGGER_ENTER", -17);
			AddSystemEvent("TRIGGER_ENTER_2D", -18);
			AddSystemEvent("TRIGGER_EXIT", -19);
            AddSystemEvent("TRIGGER_EXIT_2D", -20);
            AddSystemEvent("LEVEL_LOADED", -21);
		}

        /// <summary>
        /// Unity callback called when the script instance is being loaded.
        /// </summary>
		void Start () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

			DontDestroyOnLoad(gameObject);

            if (s_CreateGUICallback)
                CreateGUICallbackIfNotExist();
		}

        /// <summary>
        /// Update is called every frame.
        /// Call Update nodes.
        /// </summary>
        void Update () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            if (update != null)
                update();
        }

        /// <summary>
        /// FixedUpdate is called every fixed framerate.
        /// Call FixedUpdate nodes.
        /// </summary>
        void FixedUpdate () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            if (fixedUpdate != null)
                fixedUpdate();
        }

        /// <summary>
        /// FixedUpdate is called every frame just before Update.
        /// Call LateUpdate nodes.
        /// </summary>
        void LateUpdate () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            if (lateUpdate != null)
                lateUpdate();

            s_LastRealTime = Time.realtimeSinceStartup;
        }
		#endregion Unity Callbacks
		
		/// <summary>
        /// Creates a GUICallback component if it not exists.
        /// </summary> 
		void CreateGUICallbackIfNotExist () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            if (m_GUICallback == null) {
                #if UNITY_EDITOR
            	if (Application.isEditor) {
            	   // Delay the GUICallback creation in editor to avoid the Unity GUISkin bug.
            	   StartCoroutine(DelayedCreateGUICallback());
                }
            	else {
                    m_GUICallback = gameObject.AddComponent<GUICallback>();
                    s_CreateGUICallback = false;
                }
                #else
                m_GUICallback = gameObject.AddComponent<GUICallback>();
                s_CreateGUICallback = false;
                #endif
            }
		}

        /// <summary>
        /// Called only in the editor to create a GUICallback in the next frame.
        /// </summary> 
        IEnumerator DelayedCreateGUICallback () {
            yield return null;
            if (m_GUICallback == null) {
            	m_GUICallback = gameObject.AddComponent<GUICallback>();
                s_CreateGUICallback = false;
            }
        }
	}
}