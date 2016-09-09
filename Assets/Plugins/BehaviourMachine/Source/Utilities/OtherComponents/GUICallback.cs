//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary> 
    /// Call OnGUI functions.
    /// A game object with this component is created automatically when you call GlobalBlackboard.Instance.CreateGUICallbackIfNotExist().
    /// <seealso cref="BehaviourMachine.OnGUI" />
    /// <seealso cref="BehaviourMachine.InternalGlobalBlackboard" />
    /// </summary> 
    [ExecuteInEditMode]
    [AddComponentMenu("")]
    public class GUICallback : MonoBehaviour {

        #region Singleton
        static GUICallback s_Instance;

        /// <summary> 
        /// Returns the unique instance of the GUICallback component.
        /// </summary> 
        static public GUICallback Instance {
            get {return s_Instance;}
        }
        #endregion Singleton

        #region Events
        /// <summary> 
        /// OnGUI callback called in runtime and in the editor mode.
        /// </summary> 
        public static event SimpleCallback onGUI;

        /// <summary> 
        /// Reset all callbacks registered in this component (editor only).
        /// </summary>
        #if UNITY_EDITOR
        public static void ResetCallbacks () {
            if (!Application.isPlaying)
                onGUI = null;
        }
        #endif
        #endregion Events

        #region Statick Methods
        /// <summary> 
        /// Returns true if the GUICallback has registered callbacks.
        /// <returns>True if the GUICallback has registered callbacks; false otherwise.</returns>
        /// </summary> 
        public static bool HasCallbacks () {
            return onGUI != null;
        }
        #endregion Statick Methods

        #region Unity Callbacks
        /// <summary> 
        /// A Unity callback called when the script instance is being loaded.
        /// </summary>
        void Awake () {
            DontDestroyOnLoad(gameObject);

            // Destroy old gui callbacks
            UnityEngine.Object[] objs = Object.FindObjectsOfType(typeof(GUICallback));
            for (int i = 0; i < objs.Length; i++) {
                var guiCallback = objs[i] as GUICallback; 
                if (guiCallback != this) {
                    if (Application.isEditor) {
                        if (!Application.isPlaying)
                            DestroyImmediate(guiCallback.gameObject, true);
                        else
                            Destroy(guiCallback.gameObject);
                    }
                    else
                        Destroy(guiCallback.gameObject);
                }
            }

        }

        /// <summary> 
        /// A Unity callback called when the object is loaded.
        /// </summary>
        void OnEnable () {
            // Destroys the new gui callback
            if (s_Instance != null && s_Instance != this) {
                #if UNITY_EDITOR
                if (!Application.isPlaying)
                    DestroyImmediate(s_Instance.gameObject, true);
                else
                    Destroy(s_Instance.gameObject);
                #else
                Destroy(s_Instance.gameObject);
                #endif
            }
            else {
                // Set instance
                s_Instance = this;
            }
        }

        /// <summary> 
        /// A Unity callback called for rendering and handling GUI events.
        /// </summary>
    	void OnGUI () {
            #if UNITY_EDITOR
            // Ignore events in editor mode
            bool eventIgnored = false;
            EventType type = Event.current.type;
            if (!Application.isPlaying && type != EventType.Repaint && type != EventType.Layout) {
                eventIgnored = true;
                Event.current.type = EventType.Ignore;
            }
            #endif

            // Call OnGUI callbacks
            if (onGUI != null) {
                onGUI();
            }
            
            #if UNITY_EDITOR
            // Restore events
            if (eventIgnored)
                Event.current.type = type;
            #endif
        }
        #endregion Unity Callbacks
    }
}