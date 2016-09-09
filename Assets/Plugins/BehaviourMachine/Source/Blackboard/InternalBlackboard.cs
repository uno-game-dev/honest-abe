//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {

    #region Delegates
    public delegate void SimpleCallback();
    public delegate void BlackboardCallback(InternalBlackboard b);
    public delegate void StateCallback(InternalStateBehaviour s);
    public delegate void TransitionCallback(StateTransition t);
    public delegate void BooleanCallback(bool b);
    public delegate void IntegerCallback(int i);
    public delegate void CollisionCallback(UnityEngine.Collision c);
    #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
    public delegate void Collision2DCallback(UnityEngine.Collision2D c);
    #endif
    public delegate void ControllerColliderHitCallback(UnityEngine.ControllerColliderHit c);
    public delegate void FloatCallback(float f);
    public delegate void TouchCallback(UnityEngine.Touch t);
    public delegate void ColliderCallback(UnityEngine.Collider c);
    #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
    public delegate void Collider2DCallback(UnityEngine.Collider2D c);
    #endif
    #endregion Delegates

    /// <summary>
    /// Store variables and has unity event callbacks.
    /// </summary>
    [ExecuteInEditMode]
    [AddComponentMenu("")]
    public class InternalBlackboard : MonoBehaviour {


        #region Static
        #if UNITY_EDITOR
        public static event BlackboardCallback onUpdateHideFlag;
        #endif
        #endregion Static


        #region Members
        [HideInInspector]
        [SerializeField]
        ConcreteFloatVar[] m_FloatVars = new ConcreteFloatVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteIntVar[] m_IntVars = new ConcreteIntVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteBoolVar[] m_BoolVars = new ConcreteBoolVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteStringVar[] m_StringVars = new ConcreteStringVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteVector3Var[] m_Vector3Vars = new ConcreteVector3Var[0];
        [HideInInspector]
        [SerializeField]
        ConcreteRectVar[] m_RectVars = new ConcreteRectVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteColorVar[] m_ColorVars = new ConcreteColorVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteQuaternionVar[] m_QuaternionVars = new ConcreteQuaternionVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteGameObjectVar[] m_GameObjectVars = new ConcreteGameObjectVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteTextureVar[] m_TextureVars = new ConcreteTextureVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteMaterialVar[] m_MaterialVars = new ConcreteMaterialVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteObjectVar[] m_ObjectVars = new ConcreteObjectVar[0];
        [HideInInspector]
        [SerializeField]
        ConcreteDynamicList[] m_DynamicLists = new ConcreteDynamicList[0];
        [HideInInspector]
        [SerializeField]
        protected FsmEvent[] m_FsmEvents = new FsmEvent[0];
        [HideInInspector]
        [SerializeField]
        protected ConcreteFsmEvent[] m_ConcreteFsmEvents = new ConcreteFsmEvent[0];
        [SerializeField]
        protected string m_Namespace;

        [System.NonSerialized]
        List<ParentBehaviour> m_Parents = new List<ParentBehaviour>();
        #endregion Members

        #region Events
        /// <summary>
        /// Callback for setting up animation IK (inverse kinematics). 
        /// </summary>
        public event IntegerCallback onAnimatorIK;

        /// <summary>
        /// Sent when the player gets or loses focus. 
        /// </summary>
        public event SimpleCallback onApplicationFocus;

        /// <summary>
        /// Sent when the player pauses. 
        /// </summary>
        public event BooleanCallback onApplicationPause;

        /// <summary>
        /// Sent before the application quits.
        /// </summary>
        public event SimpleCallback onApplicationQuit;

        /// <summary>
        /// Sent when the renderer is no longer visible by any camera. 
        /// </summary>
        public event SimpleCallback onBecameInvisible;

        /// <summary>
        /// Sent when the renderer became visible by any camera. 
        /// </summary>
        public event SimpleCallback onBecameVisible;

        /// <summary>
        /// Sent when this collider/rigidbody has begun touching another rigidbody/collider. 
        /// </summary>
        public event CollisionCallback onCollisionEnter;
       
        /// <summary>
        /// Sent when this collider/rigidbody has stopped touching another rigidbody/collider. 
        /// </summary>
        public event CollisionCallback onCollisionExit;
        
        /// <summary>
        /// Sent once per frame for every collider/rigidbody that is touching rigidbody/collider. 
        /// </summary>
        public event CollisionCallback onCollisionStay;
        
        #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
        /// <summary>
        /// Sent when an incoming collider makes contact with this object's collider (2D physics only). 
        /// </summary>
        public event Collision2DCallback onCollisionEnter2D;

        /// <summary>
        /// Sent when a collider on another object stops touching this object's collider (2D physics only). 
        /// </summary>
        public event Collision2DCallback onCollisionExit2D;

        /// <summary>
        /// Sent each frame while a collider on another object is touching this object's collider (2D physics only). 
        /// </summary>
        public event Collision2DCallback onCollisionStay2D;
        #endif

        /// <summary>
        /// OnControllerColliderHit is called when the controller hits a collider while performing a Move. 
        /// </summary>
        public event ControllerColliderHitCallback onControllerColliderHit;

        /// <summary>
        /// OnDrawGizmos is used to draw gizmos in the scene. 
        /// </summary>
        public event SimpleCallback onDrawGizmos;

        /// <summary>
        /// Called when a joint attached to the same game object broke. 
        /// </summary>
        public event FloatCallback onJointBreak;

        /// <summary>
        /// Called when the user has clicked on a GUIElement or Collider and is still holding down the mouse. 
        /// </summary>
        public event SimpleCallback onMouseDrag;

        /// <summary>
        /// Called when the mouse entered the GUIElement or Collider. 
        /// </summary>
        public event SimpleCallback onMouseEnter;

        /// <summary>
        /// Called when the mouse is not any longer over the GUIElement or Collider.
        /// </summary>
        public event SimpleCallback onMouseExit;

        /// <summary>
        /// Called every frame while the mouse is over the GUIElement or Collider.
        /// </summary>
        public event SimpleCallback onMouseOver;

        /// <summary>
        /// Called when the user has pressed the mouse button while over the GUIElement or Collider. 
        /// </summary>
        public event SimpleCallback onMouseDown;

        /// <summary>
        /// Called when the user has released the mouse button. 
        /// </summary>
        public event SimpleCallback onMouseUp;

        /// <summary>
        /// OnMouseUpAsButton is only called when the mouse is released over the same GUIElement or Collider as it was pressed. 
        /// </summary>
        public event SimpleCallback onMouseUpAsButton;

        /// <summary>
        /// Called when the Collider other enters the trigger. 
        /// </summary>
        public event ColliderCallback onTriggerEnter; 

        /// <summary>
        /// Called when the Collider other has stopped touching the trigger. 
        /// </summary>
        public event ColliderCallback onTriggerExit; 

        /// <summary>
        /// Called once per frame for every Collider other that is touching the trigger.
        /// </summary>
        public event ColliderCallback onTriggerStay; 

        #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
        /// <summary>
        /// Sent when another object enters a trigger collider attached to this object (2D physics only).
        /// </summary>
        public event Collider2DCallback onTriggerEnter2D; 

        /// <summary>
        /// Sent when another object leaves a trigger collider attached to this object (2D physics only).
        /// </summary>
        public event Collider2DCallback onTriggerExit2D;

        /// <summary>
        /// Sent each frame while another object is within a trigger collider attached to this object (2D physics only).
        /// </summary>
        public event Collider2DCallback onTriggerStay2D; 
        #endif

        /// <summary>
        /// Sent after a new level was loaded.
        /// </summary>
        public event IntegerCallback onLevelWasLoaded;
        #endregion Events

        #region Properties
        /// <summary>
        /// Returns the float variables.
        /// </summary>
        public FloatVar[] floatVars {get {return m_FloatVars;}}

        /// <summary>
        /// Returns the int variables.
        /// </summary>
        public IntVar[] intVars {get {return m_IntVars;}}

        /// <summary>
        /// Returns the bool variables.
        /// </summary>
        public BoolVar[] boolVars {get {return m_BoolVars;}}

        /// <summary>
        /// Returns the string variables.
        /// </summary>
        public StringVar[] stringVars {get {return m_StringVars;}}

        /// <summary>
        /// Returns the Vector3 variables.
        /// </summary>
        public Vector3Var[] vector3Vars {get {return m_Vector3Vars;}}

        /// <summary>
        /// Returns the Rect variables.
        /// </summary>
        public RectVar[] rectVars {get {return m_RectVars;}}

        /// <summary>
        /// Returns the Color variables.
        /// </summary>
        public ColorVar[] colorVars {get {return m_ColorVars;}}

        /// <summary>
        /// Returns the Quaternion variables.
        /// </summary>
        public QuaternionVar[] quaternionVars {get {return m_QuaternionVars;}}

        /// <summary>
        /// Returns the Game Object variables.
        /// </summary>
        public GameObjectVar[] gameObjectVars {get {return m_GameObjectVars;}}

        /// <summary>
        /// Returns the Texture variables.
        /// </summary>
        public TextureVar[] textureVars {get {return m_TextureVars;}}

        /// <summary>
        /// Returns the Material variables.
        /// </summary>
        public MaterialVar[] materialVars {get {return m_MaterialVars;}}

        /// <summary>
        /// Returns the Object variables.
        /// </summary>
        public ObjectVar[] objectVars {get {return m_ObjectVars;}}

        /// <summary>
        /// Returns the DynamicList variables.
        /// </summary>
        public DynamicList[] dynamicLists {get {return m_DynamicLists;}}

        /// <summary>
        /// Returns the FsmEvents.
        /// </summary>
        public FsmEvent[] fsmEvents {get {return m_ConcreteFsmEvents;}}

        /// <summary>
        /// Returns all variables on the blackboard.
        /// </summary>
        public Variable[] variables {
            get {
                var variables = new List<Variable>(this.fsmEvents);
                variables.AddRange(this.intVars);
                variables.AddRange(this.boolVars);
                variables.AddRange(this.stringVars);
                variables.AddRange(this.vector3Vars);
                variables.AddRange(this.rectVars);
                variables.AddRange(this.colorVars);
                variables.AddRange(this.quaternionVars);
                variables.AddRange(this.gameObjectVars);
                variables.AddRange(this.textureVars);
                variables.AddRange(this.materialVars);
                variables.AddRange(this.objectVars);
                variables.AddRange(this.dynamicLists);
                variables.AddRange(this.fsmEvents);
                return variables.ToArray();
            }
        }

        /// <summary>
        /// Returns true if there is no variable; false otherwise.
        /// </summary>
        public bool isEmpty {
            get {
                return  m_FloatVars.Length == 0 && 
                        m_IntVars.Length == 0 &&
                        m_BoolVars.Length == 0 &&
                        m_StringVars.Length == 0 &&
                        m_Vector3Vars.Length == 0 &&
                        m_RectVars.Length == 0 &&
                        m_ColorVars.Length == 0 &&
                        m_QuaternionVars.Length == 0 &&
                        m_GameObjectVars.Length == 0 &&
                        m_TextureVars.Length == 0 &&
                        m_MaterialVars.Length == 0 &&
                        m_ObjectVars.Length == 0 &&
                        m_DynamicLists.Length == 0 &&
                        m_ConcreteFsmEvents.Length == 0;
            }
        }


        /// <summary>
        /// Number of FloatVars in the blackboard. 
        /// </summary>
        public int GetFloatsSize () {return m_FloatVars.Length;}

        /// <summary>
        /// Number of IntVars in the blackboard. 
        /// </summary>
        public int GetIntsSize () {return m_IntVars.Length;}

        /// <summary>
        /// Number of BoolVars in the blackboard. 
        /// </summary>
        public int GetBoolsSize () {return m_BoolVars.Length;}

        /// <summary>
        /// Number of StringVars in the blackboard. 
        /// </summary>
        public int GetStringsSize () {return m_StringVars.Length;}

        /// <summary>
        /// Number of Vector3Vars in the blackboard. 
        /// </summary>
        public int GetVector3sSize () {return m_Vector3Vars.Length;}

        /// <summary>
        /// Number of RectVars in the blackboard. 
        /// </summary>
        public int GetRectsSize () {return m_RectVars.Length;}

        /// <summary>
        /// Number of ColorVars in the blackboard. 
        /// </summary>
        public int GetColorsSize () {return m_ColorVars.Length;}

        /// <summary>
        /// Number of QuaternionVars in the blackboard. 
        /// </summary>
        public int GetQuaternionsSize () {return m_QuaternionVars.Length;}

        /// <summary>
        /// Number of GameObjectVars in the blackboard. 
        /// </summary>
        public int GetGameObjectsSize () {return m_GameObjectVars.Length;}

        /// <summary>
        /// Number of MaterialVars in the blackboard. 
        /// </summary>
        public int GetMaterialsSize () {return m_MaterialVars.Length;}

        /// <summary>
        /// Number of TextureVars in the blackboard. 
        /// </summary>
        public int GetTexturesSize () {return m_TextureVars.Length;}

        /// <summary>
        /// Number of ObjectVars in the blackboard. 
        /// </summary>
        public int GetObjectsSize () {return m_ObjectVars.Length;}

        /// <summary>
        /// Number of ObjectVars in the blackboard. 
        /// </summary>
        public int GetDynamicListsSize () {return m_DynamicLists.Length;}

        /// <summary>
        /// Number of FsmEvents in the blackboard. 
        /// </summary>
        public int GetFsmEventsSize () {return m_ConcreteFsmEvents.Length;}

        /// <summary>
        /// Returns the total number of variables stored by the blackboard. 
        /// <returns>The total number of variables in the blackboard.</returns>
        /// </summary>
        public int GetSize () {
            return m_FloatVars.Length + 
                    m_IntVars.Length +
                    m_BoolVars.Length +
                    m_StringVars.Length +
                    m_Vector3Vars.Length +
                    m_RectVars.Length +
                    m_ColorVars.Length +
                    m_QuaternionVars.Length +
                    m_GameObjectVars.Length +
                    m_TextureVars.Length +
                    m_MaterialVars.Length +
                    m_ObjectVars.Length +
                    m_DynamicLists.Length +
                    m_ConcreteFsmEvents.Length;
        }

        /// <summary>
        /// Returns the namespace to save the custom Blackboard.
        /// </summary>
        public string Namespace {get {return m_Namespace;}}
        #endregion Properties


        #region Unity Callbacks
        /// <summary>
        /// Unity callback called when the script instance is being loaded.
        /// Call UpdateHideFlags and SaveLastParent in prefab instances or missing prefabs.
        /// </summary>
        public virtual void Awake () {
            #if UNITY_EDITOR
            if (InternalBlackboard.onUpdateHideFlag != null)
                InternalBlackboard.onUpdateHideFlag(this);
            #endif

            if (InternalGlobalBlackboard.Instance == null)
                Print.LogWarning("You should create a GlobalBlackboard by selecting \"Tools/BehaviourMachine/Global Blackboard\" from the Unity toolbar.");
        }

        /// <summary>
        /// Unity callback for setting up animation IK (inverse kinematics).
        /// </summary>
        public virtual void OnAnimatorIK (int layerIndex) {
            if (onAnimatorIK != null)
                onAnimatorIK(layerIndex);
        }

        /// <summary>
        /// Unity callback sent to all game objects when the player gets or loses focus.
        /// </summary>
        public virtual void OnApplicationFocus () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            SendEvent(-1);

            if (onApplicationFocus != null)
                onApplicationFocus();
        }

        /// <summary>
        /// Unity callback sent to all game objects when the player pauses.
        /// </summary>
        public virtual void OnApplicationPause (bool pauseStatus) {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            SendEvent(-2);

            if (onApplicationPause != null)
                onApplicationPause(pauseStatus);
        }

        /// <summary>
        /// Unity callback sent to all game objects before the application quits.
        /// </summary>
        public virtual void OnApplicationQuit () {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            #endif

            SendEvent(-3);

            if (onApplicationQuit != null)
                onApplicationQuit();
        }

        /// <summary>
        /// Unity callback called when the renderer is no longer visible by any camera.
        /// </summary>
        public virtual void OnBecameInvisible () {
            SendEvent(-4);

            if (onBecameInvisible != null)
                onBecameInvisible();
        }

        /// <summary>
        /// Unity callback called when the renderer became visible by any camera.
        /// </summary>
        public virtual void OnBecameVisible () {
            SendEvent(-5);

            if (onBecameVisible != null)
                onBecameVisible();
        }

        /// <summary>
        /// Unity callback called when this collider/rigidbody has begun touching another rigidbody/collider.
        /// </summary>
        public virtual void OnCollisionEnter (Collision collision) {
            SendEvent(-6);

            if (onCollisionEnter != null)
                onCollisionEnter(collision);
        }

        #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
        /// <summary>
        /// Unity callback called when this collider/rigidbody has begun touching another rigidbody/collider (2D physics only).
        /// </summary>
        public virtual void OnCollisionEnter2D (Collision2D collision) {
            SendEvent(-7);

            if (onCollisionEnter2D != null)
                onCollisionEnter2D(collision);
        }
        #endif

        /// <summary>
        /// Unity callback called when this collider/rigidbody has stopped touching another rigidbody/collider.
        /// </summary>
        public virtual void OnCollisionExit (Collision collision) {
            SendEvent(-8);

            if (onCollisionExit != null)
                onCollisionExit(collision);
        }

        #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
        /// <summary>
        /// Unity callback called when this collider/rigidbody has stopped touching another rigidbody/collider (2D physics only).
        /// </summary>
        public virtual void OnCollisionExit2D (Collision2D collision) {
            SendEvent(-9);

            if (onCollisionExit2D != null)
                onCollisionExit2D(collision);
        }
        #endif

        /// <summary>
        /// Unity callback called once per frame for every collider/rigidbody that is touching rigidbody/collider.
        /// </summary>
        public virtual void OnCollisionStay (Collision collision) {
            if (onCollisionStay != null)
                onCollisionStay(collision);
        }

        #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
        /// <summary>
        /// Unity callback called once per frame for every collider/rigidbody that is touching rigidbody/collider (2D physics only).
        /// </summary>
        public virtual void OnCollisionStay2D (Collision2D collision) {
            if (onCollisionStay2D != null)
                onCollisionStay2D(collision);
        }
        #endif

        /// <summary>
        /// Unity callback called when the controller hits a collider while performing a Move.
        /// </summary>
        public virtual void OnControllerColliderHit (ControllerColliderHit hit) {            
            if (onControllerColliderHit != null)
                onControllerColliderHit(hit);
        }

        /// <summary>
        /// Unity callback called to draw gizmos.
        /// </summary>
        public virtual void OnDrawGizmos () {
            if (onDrawGizmos != null)
                onDrawGizmos();
        }

        /// <summary>
        /// Unity callback called when a joint attached to the same game object broke.
        /// </summary>
        public virtual void OnJointBreak (float breakForce) {
            SendEvent(-11);

            if (onJointBreak != null)
                onJointBreak(breakForce);
        }

        /// <summary>
        /// Unity callback called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
        /// </summary>
        public virtual void OnMouseDrag () {
            if (onMouseDrag != null)
                onMouseDrag();
        }

        /// <summary>
        /// Unity callback called when the mouse entered the GUIElement or Collider.
        /// </summary>
        public virtual void OnMouseEnter () {
            SendEvent(-12);

            if (onMouseEnter != null)
                onMouseEnter();
        }

        /// <summary>
        /// Unity callback called when the mouse is not any longer over the GUIElement or Collider.
        /// </summary>
        public virtual void OnMouseExit () {
            SendEvent(-13);

            if (onMouseExit != null)
                onMouseExit();
        }

        /// <summary>
        /// Unity callback called when the user has pressed the mouse button while over the GUIElement or Collider.
        /// </summary>
        public virtual void OnMouseDown () {
            SendEvent(-14);

            if (onMouseDown != null)
                onMouseDown();
        }

        /// <summary>
        /// Unity callback called every frame while the mouse is over the GUIElement or Collider.
        /// </summary>
        public virtual void OnMouseOver () {
            if (onMouseOver != null)
                onMouseOver();
        }

        /// <summary>
        /// Unity callback called when the user has released the mouse button.
        /// </summary>
        public virtual void OnMouseUp () {
            SendEvent(-15);

            if (onMouseUp != null)
                onMouseUp();
        }

        /// <summary>
        /// Unity callback called when the mouse is released over the same GUIElement or Collider as it was pressed.
        /// </summary>
        public virtual void OnMouseUpAsButton () {
            SendEvent(-16);

            if (onMouseUpAsButton != null)
                onMouseUpAsButton();
        }

        /// <summary>
        /// Unity callback called when the Collider other enters the trigger.
        /// </summary>
        public virtual void OnTriggerEnter (Collider other) {
            SendEvent(-17);

            if (onTriggerEnter != null)
                onTriggerEnter(other);
        }

        #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
        /// <summary>
        /// Unity callback called when the Collider other enters the trigger (2D physics only).
        /// </summary>
        public virtual void OnTriggerEnter2D (Collider2D other) {
            SendEvent(-18);

            if (onTriggerEnter2D != null)
                onTriggerEnter2D(other);
        }
        #endif

        /// <summary>
        /// Unity callback called when the Collider other has stopped touching the trigger.
        /// </summary>
        public virtual void OnTriggerExit (Collider other) {
            SendEvent(-19);

            if (onTriggerExit != null)
                onTriggerExit(other);
        }

        #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
        /// <summary>
        /// Unity callback called when the Collider other has stopped touching the trigger (2D physics only).
        /// </summary>
        public virtual void OnTriggerExit2D (Collider2D other) {
            SendEvent(-20);
            
            if (onTriggerExit2D != null)
                onTriggerExit2D(other);
        }
        #endif

        /// <summary>
        /// Unity callback called once per frame for every Collider other that is touching the trigger.
        /// </summary>
        public virtual void OnTriggerStay (Collider other) {          
            if (onTriggerStay != null)
                onTriggerStay(other);
        }

        #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
        /// <summary>
        /// Unity callback called once per frame for every Collider other that is touching the trigger (2D physics only).
        /// </summary>
        public virtual void OnTriggerStay2D (Collider2D other) {          
            if (onTriggerStay2D != null)
                onTriggerStay2D(other);
        }
        #endif

        /// <summary>
        /// Unity callback called after a new level was loaded.
        /// <param name="level">The index of the level that was loaded.</param>
        /// </summary>
        public virtual void OnLevelWasLoaded (int level) {
            SendEvent(-21);

            if (onLevelWasLoaded != null)
                onLevelWasLoaded(level);
        }
        #endregion Unity Callbacks

        
        #region Root Fsm
        /// <summary>
        /// Returns the enabled root parents (StateMachines/BehaviourTrees) in this game object. 
        /// <returns>The enabled root StateMachines and BehaviourTrees in this game object.</returns> 
        /// </summary>
        public ParentBehaviour[] GetEnabledRootParents () {
            return m_Parents.ToArray();
        }

        /// <summary>
        /// Returns the root FSM in this game object that has the supplied name.
        /// <param name="parentName">The FSM name to search for.</param> 
        /// <returns>The target FSM.</returns> 
        /// </summary>
        public ParentBehaviour GetRootParent (string parentName) {
            for (int i = 0; i < m_Parents.Count; i++) {
                if (m_Parents[i].stateName == parentName)
                    return m_Parents[i];
            }
            return null;
        }

        /// <summary>
        /// Call SendEvent on all enabled root StateMachines/BehaviourTrees in the scene.
        /// Please note that this function is very slow. It is not recommended to use this function every frame.
        /// <param name="eventName">The name of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public static bool SendEventToAll (string eventName) {
            bool eventUsed = false;

            foreach (InternalBlackboard blackboard in  UnityEngine.Object.FindObjectsOfType(typeof(InternalBlackboard)))
                eventUsed = blackboard.SendEvent(eventName) || eventUsed;

            return eventUsed;
        }

        /// <summary>
        /// Call SendEvent on all enabled root StateMachines/BehaviourTrees in the scene.
        /// Please note that this function is very slow. It is not recommended to use this function every frame.
        /// <param name="eventID">The id of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public static bool SendEventToAll (int eventID) {
            bool eventUsed = false;

            foreach (InternalBlackboard blackboard in  UnityEngine.Object.FindObjectsOfType(typeof(InternalBlackboard)))
                eventUsed = blackboard.SendEvent(eventID) || eventUsed;

            return eventUsed;
        }

        /// <summary>
        /// Call SendEvent on all enabled root StateMachines/BehaviourTrees in this game object or any of its children.
        /// <param name="eventName">The name of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool BroadcastEvent (string eventName) {
            bool eventUsed = SendEvent(eventName);

            foreach (Transform child in transform) {
                var blackboard = child.GetComponent<InternalBlackboard>();
                if (blackboard != null) 
                    eventUsed = blackboard.BroadcastEvent(eventName) || eventUsed;
            }

            return eventUsed;
        }

        /// <summary>
        /// Call SendEvent on all enabled root StateMachines/BehaviourTrees in this game object or any of its children.
        /// <param name="eventID">The id of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool BroadcastEvent (int eventID) {
            bool eventUsed = SendEvent(eventID);

            foreach (Transform child in transform) {
                var blackboard = child.GetComponent<InternalBlackboard>();
                if (blackboard != null) 
                    eventUsed = blackboard.BroadcastEvent(eventID) || eventUsed;
            }

            return eventUsed;
        }

        /// <summary>
        /// Call SendEvent on all enabled root StateMachines/BehaviourTrees in this game object and its ancestor.
        /// <param name="eventName">The name of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool SendEventUpwards (string eventName) {
            bool eventUsed = SendEvent(eventName);

            Transform parent = transform.parent;
            if (parent != null) {
                var blackboard = parent.GetComponent<InternalBlackboard>();
                if (blackboard != null)
                    eventUsed = blackboard.SendEventUpwards(eventName) || eventUsed;
            }

            return eventUsed;
        }

        /// <summary>
        /// Call SendEvent on all enabled root StateMachines/BehaviourTrees in this game object and its ancestor.
        /// <param name="eventID">The id of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool SendEventUpwards (int eventID) {
            bool eventUsed = SendEvent(eventID);

            Transform parent = transform.parent;
            if (parent != null) {
                var blackboard = parent.GetComponent<InternalBlackboard>();
                if (blackboard != null)
                    eventUsed = blackboard.SendEventUpwards(eventID) || eventUsed;
            }

            return eventUsed;
        }
 
        /// <summary>
        /// Call SendEvent in the root StateMachines/BehaviourTrees that has the supplied name.
        /// <param name="parentName">The parent name to change state.</param> 
        /// <param name="eventName">The name of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool SendEvent (string parentName, string eventName) {
            // Get the fsmEvent
            FsmEvent fsmEvent = this.GetFsmEvent(eventName);
            if (fsmEvent == null && InternalGlobalBlackboard.Instance != null && InternalGlobalBlackboard.Instance != this)
                fsmEvent = InternalGlobalBlackboard.Instance.GetFsmEvent(eventName);

            return fsmEvent != null && SendEvent(parentName, fsmEvent.id);
        }

        /// <summary>
        /// Call SendEvent in the root StateMachines/BehaviourTrees that has the supplied name.
        /// <param name="parentName">The target parent name.</param>
        /// <param name="eventID">The id of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool SendEvent (string parentName, int eventID) {
            ParentBehaviour parent = GetRootParent(parentName);
            return parent != null && parent.SendEvent(eventID);
        }

        /// <summary>
        /// Call SendEvent on all enabled root StateMachines/BehaviourTrees.
        /// <param name="eventName">The name of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool SendEvent (string eventName) {
            // Get the fsmEvent
            FsmEvent fsmEvent = this.GetFsmEvent(eventName);
            if (fsmEvent == null && InternalGlobalBlackboard.Instance != null && InternalGlobalBlackboard.Instance != this)
                fsmEvent = InternalGlobalBlackboard.Instance.GetFsmEvent(eventName);

            return fsmEvent != null && SendEvent(fsmEvent.id);
        }

        /// <summary>
        /// Call SendEvent on all enabled root StateMachines/BehaviourTrees.
        /// <param name="eventName">The name of the event.</param>
        /// </summary>
        public void SendEventTrigger (string eventName) {
            this.SendEvent(eventName);
        }

        /// <summary>
        /// Call SendEvent on all enabled root FSMs in this game object.
        /// <param name="eventID">The id of the event.</param>
        /// <returns>True if the event was used; false otherwise.</returns>
        /// </summary>
        public bool SendEvent (int eventID) {
            bool eventUsed = false;
            for (int i = 0; i < m_Parents.Count; i++)
                eventUsed = m_Parents[i].SendEvent(eventID) || eventUsed;
            return eventUsed;
        }

        /// <summary>
        /// Call SendEvent on all enabled root FSMs in this game object.
        /// <param name="eventID">The id of the event.</param>
        /// </summary>
        public void SendEventTrigger (int eventID) {
            this.SendEvent(eventID);
        }

        /// <summary>
        /// Adds a new root fsm to this blackboard.
        /// Automatically called by StateMachines and BehaviourTrees.
        /// The parent should be a root parent, enabled and in the same game object as this blackboard.
        /// The parents will receive all system events in this game object.
        /// <param name="parent">The parent to be added to the blackboard.</param>
        /// </summary>
        public void AddRootParent (ParentBehaviour parent) {
            if (parent != null && parent.isRoot && parent.enabled && parent.gameObject == this.gameObject && !m_Parents.Contains(parent))
                m_Parents.Add(parent);
        }

        /// <summary>
        /// Removes the supplied fsm from this blackboard.
        /// Automatically called by StateMachines and BehaviourTrees.
        /// <param name="parent">The parent to be added to the blackboard.</param>
        /// </summary>
        public void RemoveRootParent (ParentBehaviour parent) {
            if (m_Parents.Contains(parent))
                m_Parents.Remove(parent);
        }
        #endregion Root Fsm

        
        /// <summary>
        /// Returns a unique variable name in this Blackboard.
        /// Automatically called by variables.
        /// <param name = "name">The desired name.</param>
        /// <returns>A unique variable name.</returns>
        /// </summary>
        public string GetUniqueVariableName (string name) {
            var names = new List<string>();

            // Float
            for (int i = 0; i < m_FloatVars.Length; i++)
                names.Add(m_FloatVars[i].name);
            // Int
            for (int i = 0; i < m_IntVars.Length; i++)
                names.Add(m_IntVars[i].name);
            // Bool
            for (int i = 0; i < m_BoolVars.Length; i++)
                names.Add(m_BoolVars[i].name);
            // String
            for (int i = 0; i < m_StringVars.Length; i++)
                names.Add(m_StringVars[i].name);
            // Vector3
            for (int i = 0; i < m_Vector3Vars.Length; i++)
                names.Add(m_Vector3Vars[i].name);
            // Rect
            for (int i = 0; i < m_RectVars.Length; i++)
                names.Add(m_RectVars[i].name);
            // Color
            for (int i = 0; i < m_ColorVars.Length; i++)
                names.Add(m_ColorVars[i].name);
            // Quaternion
            for (int i = 0; i < m_QuaternionVars.Length; i++)
                names.Add(m_QuaternionVars[i].name);
            // GameObject
            for (int i = 0; i < m_GameObjectVars.Length; i++)
                names.Add(m_GameObjectVars[i].name);
            // Texture
            for (int i = 0; i < m_TextureVars.Length; i++)
                names.Add(m_TextureVars[i].name);
            // Material
            for (int i = 0; i < m_MaterialVars.Length; i++)
                names.Add(m_MaterialVars[i].name);
            // Object
            for (int i = 0; i < m_ObjectVars.Length; i++)
                names.Add(m_ObjectVars[i].name);
            // DynamicList
            for (int i = 0; i < m_DynamicLists.Length; i++)
                names.Add(m_DynamicLists[i].name);
            // FsmEvent
            for (int i = 0; i < m_ConcreteFsmEvents.Length; i++)
                names.Add(m_ConcreteFsmEvents[i].name);

            return StringHelper.GetUniqueNameInList(names, name);
        }

        
        #region Get Variables
        /// <summary> 
        /// Returns a float variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The float variable that has the supplied name.</returns>
        /// </summary>
        public FloatVar GetFloatVar (string name) {
            for (int i = 0; i < m_FloatVars.Length; i++) {
                if (m_FloatVars[i].name == name)
                    return m_FloatVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns an int variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The int variable that has the supplied name.</returns>
        /// </summary>
        public IntVar GetIntVar (string name) {
            for (int i = 0; i < m_IntVars.Length; i++) {
                if (m_IntVars[i].name == name)
                    return m_IntVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a bool variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The bool variable that has the supplied name.</returns>
        /// </summary>
        public BoolVar GetBoolVar (string name) {
            for (int i = 0; i < m_BoolVars.Length; i++) {
                if (m_BoolVars[i].name == name)
                    return m_BoolVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a string variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The string variable that has the supplied name.</returns>
        /// </summary>
        public StringVar GetStringVar (string name) {
            for (int i = 0; i < m_StringVars.Length; i++) {
                if (m_StringVars[i].name == name)
                    return m_StringVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a vector3 variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The Vector3 variable that has the supplied name.</returns>
        /// </summary>
        public Vector3Var GetVector3Var (string name) {
            for (int i = 0; i < m_Vector3Vars.Length; i++) {
                if (m_Vector3Vars[i].name == name)
                    return m_Vector3Vars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a rect variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The Rect variable that has the supplied name.</returns>
        /// </summary>
        public RectVar GetRectVar (string name) {
            for (int i = 0; i < m_RectVars.Length; i++) {
                if (m_RectVars[i].name == name)
                    return m_RectVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a color variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The Color variable that has the supplied name.</returns>
        /// </summary>
        public ColorVar GetColorVar (string name) {
            for (int i = 0; i < m_ColorVars.Length; i++) {
                if (m_ColorVars[i].name == name)
                    return m_ColorVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a quaternion variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The Quaternion variable that has the supplied name.</returns>
        /// </summary>
        public QuaternionVar GetQuaternionVar (string name) {
            for (int i = 0; i < m_QuaternionVars.Length; i++) {
                if (m_QuaternionVars[i].name == name)
                    return m_QuaternionVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a GameObject variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The Game Object variable that has the supplied name.</returns>
        /// </summary>
        public GameObjectVar GetGameObjectVar (string name) {
            for (int i = 0; i < m_GameObjectVars.Length; i++) {
                if (m_GameObjectVars[i].name == name)
                    return m_GameObjectVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a Texture variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The Texture variable that has the supplied name.</returns>
        /// </summary>
        public TextureVar GetTextureVar (string name) {
            for (int i = 0; i < m_TextureVars.Length; i++) {
                if (m_TextureVars[i].name == name)
                    return m_TextureVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a Material variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The Material variable that has the supplied name.</returns>
        /// </summary>
        public MaterialVar GetMaterialVar (string name) {
            for (int i = 0; i < m_MaterialVars.Length; i++) {
                if (m_MaterialVars[i].name == name)
                    return m_MaterialVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a UnityEngine.Object variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The Object variable that has the supplied name.</returns>
        /// </summary>
        public ObjectVar GetObjectVar (string name) {
            for (int i = 0; i < m_ObjectVars.Length; i++) {
                if (m_ObjectVars[i].name == name)
                    return m_ObjectVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a DynamicLists variable.
        /// <param name="name">The name of the variable.</param>
        /// <returns>The DynamicList variable that has the supplied name.</returns>
        /// </summary>
        public DynamicList GetDynamicList (string name) {
            for (int i = 0; i < m_DynamicLists.Length; i++) {
                if (m_DynamicLists[i].name == name)
                    return m_DynamicLists[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a FsmEvent.
        /// <param name="name">The name of the fsmEvent.</param>
        /// <returns>The FsmEvent variable that has the supplied name.</returns>
        /// </summary>
        public FsmEvent GetFsmEvent (string name) {
            for (int i = 0; i < m_ConcreteFsmEvents.Length; i++) {
                if (m_ConcreteFsmEvents[i].name == name)
                    return m_ConcreteFsmEvents[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a float variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The float variable that has the supplied id.</returns>
        /// </summary>
        public FloatVar GetFloatVar (int id) {
            for (int i = 0; i < m_FloatVars.Length; i++) {
                if (m_FloatVars[i].id == id)
                    return m_FloatVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns an int variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The int variable that has the supplied id.</returns>
        /// </summary>
        public IntVar GetIntVar (int id) {
            for (int i = 0; i < m_IntVars.Length; i++) {
                if (m_IntVars[i].id == id)
                    return m_IntVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a bool variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The bool variable that has the supplied id.</returns>
        /// </summary>
        public BoolVar GetBoolVar (int id) {
            for (int i = 0; i < m_BoolVars.Length; i++) {
                if (m_BoolVars[i].id == id)
                    return m_BoolVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a string variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The string variable that has the supplied id.</returns>
        /// </summary>
        public StringVar GetStringVar (int id) {
            for (int i = 0; i < m_StringVars.Length; i++) {
                if (m_StringVars[i].id == id)
                    return m_StringVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a vector3 variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The Vector3 variable that has the supplied id.</returns>
        /// </summary>
        public Vector3Var GetVector3Var (int id) {
            for (int i = 0; i < m_Vector3Vars.Length; i++) {
                if (m_Vector3Vars[i].id == id)
                    return m_Vector3Vars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a rect variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The Rect variable that has the supplied id.</returns>
        /// </summary>
        public RectVar GetRectVar (int id) {
            for (int i = 0; i < m_RectVars.Length; i++) {
                if (m_RectVars[i].id == id)
                    return m_RectVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a color variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The Color variable that has the supplied id.</returns>
        /// </summary>
        public ColorVar GetColorVar (int id) {
            for (int i = 0; i < m_ColorVars.Length; i++) {
                if (m_ColorVars[i].id == id)
                    return m_ColorVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a quaternion variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The Quaternion variable that has the supplied id.</returns>
        /// </summary>
        public QuaternionVar GetQuaternionVar (int id) {
            for (int i = 0; i < m_QuaternionVars.Length; i++) {
                if (m_QuaternionVars[i].id == id)
                    return m_QuaternionVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a GameObject variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The GameObject variable that has the supplied id.</returns>
        /// </summary>
        public GameObjectVar GetGameObjectVar (int id) {
            for (int i = 0; i < m_GameObjectVars.Length; i++) {
                if (m_GameObjectVars[i].id == id)
                    return m_GameObjectVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a Texture variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The Texture variable that has the supplied id.</returns>
        /// </summary>
        public TextureVar GetTextureVar (int id) {
            for (int i = 0; i < m_TextureVars.Length; i++) {
                if (m_TextureVars[i].id == id)
                    return m_TextureVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a Material variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The Material variable that has the supplied id.</returns>
        /// </summary>
        public MaterialVar GetMaterialVar (int id) {
            for (int i = 0; i < m_MaterialVars.Length; i++) {
                if (m_MaterialVars[i].id == id)
                    return m_MaterialVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a UnityEngine.Object variable.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The Object variable that has the supplied id.</returns>
        /// </summary>
        public ObjectVar GetObjectVar (int id) {
            for (int i = 0; i < m_ObjectVars.Length; i++) {
                if (m_ObjectVars[i].id == id)
                    return m_ObjectVars[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a DynamicList.
        /// <param name="id">The id of the variable.</param>
        /// <returns>The DynamicList that has the supplied id.</returns>
        /// </summary>
        public DynamicList GetDynamicList (int id) {
            for (int i = 0; i < m_DynamicLists.Length; i++) {
                if (m_DynamicLists[i].id == id)
                    return m_DynamicLists[i];
            }

            return null;
        }

        /// <summary> 
        /// Returns a FsmEvent.
        /// <param name="id">The id of the fsmEvent.</param>
        /// <returns>The FsmEvent variable that has the supplied id.</returns>
        /// </summary>
        public FsmEvent GetFsmEvent (int id) {
            for (int i = 0; i < m_ConcreteFsmEvents.Length; i++) {
                if (m_ConcreteFsmEvents[i].id == id)
                    return m_ConcreteFsmEvents[i];
            }

            return null;
        }

        /// <summary>
        /// Returns a list of variables that have the supplied type.
        /// <param name="type">The type of the variables to be returned.</param>
        /// <returns>All variables in this blackboard that have the supplied type.</returns>
        /// </summary>
        public List<Variable> GetVariables (System.Type type) {
            List<Variable> variables = new List<Variable>();

            // Float
            if (type == typeof(FloatVar)) {
                for (int i = 0; i < m_FloatVars.Length; i++)
                    variables.Add(m_FloatVars[i]);
            }
            // Int
            else if (type == typeof(IntVar)) {
                for (int i = 0; i < m_IntVars.Length; i++)
                    variables.Add(m_IntVars[i]);
            }
            // Bool
            else if (type == typeof(BoolVar)) {
                for (int i = 0; i < m_BoolVars.Length; i++)
                    variables.Add(m_BoolVars[i]);
            }
            // String
            else if (type == typeof(StringVar)) {
                for (int i = 0; i < m_StringVars.Length; i++)
                    variables.Add(m_StringVars[i]);
            }
            // Vector3
            else if (type == typeof(Vector3Var)) {
                for (int i = 0; i < m_Vector3Vars.Length; i++)
                    variables.Add(m_Vector3Vars[i]);
            }
            // Rect
            else if (type == typeof(RectVar)) {
                for (int i = 0; i < m_RectVars.Length; i++)
                    variables.Add(m_RectVars[i]);
            }
            // Color
            else if (type == typeof(ColorVar)) {
                for (int i = 0; i < m_ColorVars.Length; i++)
                    variables.Add(m_ColorVars[i]);
            }
            // Quaternion
            else if (type == typeof(QuaternionVar)) {
                for (int i = 0; i < m_QuaternionVars.Length; i++)
                    variables.Add(m_QuaternionVars[i]);
            }
            // GameObject
            else if (type == typeof(GameObjectVar)) {
                for (int i = 0; i < m_GameObjectVars.Length; i++)
                    variables.Add(m_GameObjectVars[i]);
            }
            // Texture
            else if (type == typeof(TextureVar)) {
                for (int i = 0; i < m_TextureVars.Length; i++)
                    variables.Add(m_TextureVars[i]);
            }
            // Material
            else if (type == typeof(MaterialVar)) {
                for (int i = 0; i < m_MaterialVars.Length; i++)
                    variables.Add(m_MaterialVars[i]);
            }
            // Object
            else if (type == typeof(ObjectVar)) {
                for (int i = 0; i < m_ObjectVars.Length; i++)
                    variables.Add(m_ObjectVars[i]);
            }
            // DynamicList
            else if (type == typeof(DynamicList)) {
                for (int i = 0; i < m_DynamicLists.Length; i++)
                    variables.Add(m_DynamicLists[i]);
            }
            // FsmEvent
            else if (type == typeof(FsmEvent)) {
                for (int i = 0; i < m_ConcreteFsmEvents.Length; i++)
                    variables.Add(m_ConcreteFsmEvents[i]);
            }
            else if (type == typeof(Variable)) {
                // Float
                for (int i = 0; i < m_FloatVars.Length; i++)
                    variables.Add(m_FloatVars[i]);
                // Int
                for (int i = 0; i < m_IntVars.Length; i++)
                    variables.Add(m_IntVars[i]);
                // Bool
                for (int i = 0; i < m_BoolVars.Length; i++)
                    variables.Add(m_BoolVars[i]);
                // String
                for (int i = 0; i < m_StringVars.Length; i++)
                    variables.Add(m_StringVars[i]);
                // Vector3
                for (int i = 0; i < m_Vector3Vars.Length; i++)
                    variables.Add(m_Vector3Vars[i]);
                // Rect
                for (int i = 0; i < m_RectVars.Length; i++)
                    variables.Add(m_RectVars[i]);
                // Color
                for (int i = 0; i < m_ColorVars.Length; i++)
                    variables.Add(m_ColorVars[i]);
                // Quaternion
                for (int i = 0; i < m_QuaternionVars.Length; i++)
                    variables.Add(m_QuaternionVars[i]);
                // GameObject
                for (int i = 0; i < m_GameObjectVars.Length; i++)
                    variables.Add(m_GameObjectVars[i]);
                // Texture
                for (int i = 0; i < m_TextureVars.Length; i++)
                    variables.Add(m_TextureVars[i]);
                // Material
                for (int i = 0; i < m_MaterialVars.Length; i++)
                    variables.Add(m_MaterialVars[i]);
                // Object
                for (int i = 0; i < m_ObjectVars.Length; i++)
                    variables.Add(m_ObjectVars[i]);
                // DynamicList
                for (int i = 0; i < m_DynamicLists.Length; i++)
                    variables.Add(m_DynamicLists[i]);
                // FsmEvent
                for (int i = 0; i < m_ConcreteFsmEvents.Length; i++)
                    variables.Add(m_ConcreteFsmEvents[i]);
            }

            return variables;
        }
        #endregion Get Variables

        
        #region Add Variables
        /// <summary>
        /// Returns a unique id for a new variable.
        /// The Blackboard's variables ids are always positive.
        /// <returns>A new unique id; used to create new variables.</returns>
        /// </summary> 
        protected virtual int GetUniqueID () {
            // Get variables
            var variables = this.variables;

            // Get a list of ids
            var ids = new List<int>(); 
            for (int i = 0; i < variables.Length; i++)
                ids.Add(variables[i].id);

            // Get a new unique id in the list
            int newId;
            do {
                newId = Mathf.Abs(System.Guid.NewGuid().GetHashCode());
            } while (ids.Contains(newId));

            return newId;
        }

        /// <summary>
        /// Adds a new FloatVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public FloatVar AddFloatVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var floatVar = new ConcreteFloatVar("New Float", this, newId);
            // Create the list
            var floatList = new List<ConcreteFloatVar>(m_FloatVars);
            // Add variable to the list
            floatList.Add(floatVar);
            // Create a new array
            m_FloatVars = floatList.ToArray();
            // Return the new variable
            return floatVar;
        }

        /// <summary>
        /// Adds a new IntVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public IntVar AddIntVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var intVar = new ConcreteIntVar("New Int", this, newId);
            // Create the list
            var intList = new List<ConcreteIntVar>(m_IntVars);
            // Add variable to the list
            intList.Add(intVar);
            // Create a new array
            m_IntVars = intList.ToArray();
            // Return the new variable
            return intVar;
        }

        /// <summary>
        /// Adds a new BoolVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public BoolVar AddBoolVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var boolVar = new ConcreteBoolVar("New Bool", this, newId);
            // Create the list
            var boolList = new List<ConcreteBoolVar>(m_BoolVars);
            // Add variable to the list
            boolList.Add(boolVar);
            // Create a new array
            m_BoolVars = boolList.ToArray();
            // Return the new variable
            return boolVar;
        }

        /// <summary>
        /// Adds a new StringVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public StringVar AddStringVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var stringVar = new ConcreteStringVar("New String", this, newId);
            // Create the list
            var stringList = new List<ConcreteStringVar>(m_StringVars);
            // Add variable to the list
            stringList.Add(stringVar);
            // Create a new array
            m_StringVars = stringList.ToArray();
            // Return the new variable
            return stringVar;
        }

        /// <summary>
        /// Adds a new Vector3Var to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public Vector3Var AddVector3Var () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var vector3Var = new ConcreteVector3Var("New Vector3", this, newId);
            // Create the list
            var vector3List = new List<ConcreteVector3Var>(m_Vector3Vars);
            // Add variable to the list
            vector3List.Add(vector3Var);
            // Create a new array
            m_Vector3Vars = vector3List.ToArray();
            // Return the new variable
            return vector3Var;
        }

        /// <summary>
        /// Adds a new RectVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public RectVar AddRectVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var rectVar = new ConcreteRectVar("New Rect", this, newId);
            // Create the list
            var rectList = new List<ConcreteRectVar>(m_RectVars);
            // Add variable to the list
            rectList.Add(rectVar);
            // Create a new array
            m_RectVars = rectList.ToArray();
            // Return the new variable
            return rectVar;
        }

        /// <summary>
        /// Adds a new ColorVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public ColorVar AddColorVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var colorVar = new ConcreteColorVar("New Color", this, newId);
            // Create the list
            var colorList = new List<ConcreteColorVar>(m_ColorVars);
            // Add variable to the list
            colorList.Add(colorVar);
            // Create a new array
            m_ColorVars = colorList.ToArray();
            // Return the new variable
            return colorVar;
        }

        /// <summary>
        /// Adds a new QuaternionVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public QuaternionVar AddQuaternionVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var quaternionVar = new ConcreteQuaternionVar("New Quaternion", this, newId);
            // Create the list
            var quaternionList = new List<ConcreteQuaternionVar>(m_QuaternionVars);
            // Add variable to the list
            quaternionList.Add(quaternionVar);
            // Create a new array
            m_QuaternionVars = quaternionList.ToArray();
            // Return the new variable
            return quaternionVar;
        }

        /// <summary>
        /// Adds a new GameObjectVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public GameObjectVar AddGameObjectVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var gameObjectVar = new ConcreteGameObjectVar("New GameObject", this, newId);
            // Create the list
            var gameObjectList = new List<ConcreteGameObjectVar>(m_GameObjectVars);
            // Add variable to the list
            gameObjectList.Add(gameObjectVar);
            // Create a new array
            m_GameObjectVars = gameObjectList.ToArray();
            // Return the new variable
            return gameObjectVar;
        }

        /// <summary>
        /// Adds a new TextureVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public TextureVar AddTextureVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var textureVar = new ConcreteTextureVar("New Texture", this, newId);
            // Create the list
            var textureList = new List<ConcreteTextureVar>(m_TextureVars);
            // Add variable to the list
            textureList.Add(textureVar);
            // Create a new array
            m_TextureVars = textureList.ToArray();
            // Return the new variable
            return textureVar;
        }

        /// <summary>
        /// Adds a new MaterialVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public MaterialVar AddMaterialVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var materialVar = new ConcreteMaterialVar("New Material", this, newId);
            // Create the list
            var materialList = new List<ConcreteMaterialVar>(m_MaterialVars);
            // Add variable to the list
            materialList.Add(materialVar);
            // Create a new array
            m_MaterialVars = materialList.ToArray();
            // Return the new variable
            return materialVar;
        }

        /// <summary>
        /// Adds a new ObjectVar to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public ObjectVar AddObjectVar () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var objectVar = new ConcreteObjectVar("New Object", this, newId);
            // Create the list
            var objectList = new List<ConcreteObjectVar>(m_ObjectVars);
            // Add variable to the list
            objectList.Add(objectVar);
            // Create a new array
            m_ObjectVars = objectList.ToArray();
            // Return the new variable
            return objectVar;
        }

        /// <summary>
        /// Adds a new DynamicList to the blackboard.
        /// <returns>The new variable.</returns>
        /// </summary> 
        public DynamicList AddDynamicList () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var dynamicList = new ConcreteDynamicList("New Dynamic List", this, newId);
            // Create the list
            var dynamicListList = new List<ConcreteDynamicList>(m_DynamicLists);
            // Add variable to the list
            dynamicListList.Add(dynamicList);
            // Create a new array
            m_DynamicLists = dynamicListList.ToArray();
            // Return the new variable
            return dynamicList;
        }

        /// <summary>
        /// Adds a new FsmEvent to the blackboard.
        /// <returns>The new fsmEvent.</returns>
        /// </summary> 
        public FsmEvent AddFsmEvent () {
            // Get a new id
            var newId = GetUniqueID();
            // Creates the variable
            var fsmEvent = new ConcreteFsmEvent("New FsmEvent", this, newId, false);
            // Create the list
            var fsmEventList = new List<ConcreteFsmEvent>(m_ConcreteFsmEvents);
            // Add variable to the list
            fsmEventList.Add(fsmEvent);
            // Create a new array
            m_ConcreteFsmEvents = fsmEventList.ToArray();
            // Return the new variable
            return fsmEvent;
        }
        #endregion Add Variables

        
        #region Remove Variables
        /// <summary>
        /// Removes a float variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveFloatVar (int i) {
            // Set variable as invalid
            m_FloatVars[i].SetAsInvalid();
            // Create the list
            var floatList = new List<ConcreteFloatVar>(m_FloatVars);
            // Remove variable from list
            floatList.RemoveAt(i);
            // Recreate array
            m_FloatVars = floatList.ToArray();
        }

        /// <summary>
        /// Removes an int variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveIntVar (int i) {
            // Set variable as invalid
            m_IntVars[i].SetAsInvalid();
            // Create the list
            var intList = new List<ConcreteIntVar>(m_IntVars);
            // Remove variable from list
            intList.RemoveAt(i);
            // Recreate array
            m_IntVars = intList.ToArray();
        }

        /// <summary>
        /// Removes a bool variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveBoolVar (int i) {
            // Set variable as invalid
            m_BoolVars[i].SetAsInvalid();
            // Create the list
            var boolList = new List<ConcreteBoolVar>(m_BoolVars);
            // Remove variable from list
            boolList.RemoveAt(i);
            // Recreate array
            m_BoolVars = boolList.ToArray();
        }

        /// <summary>
        /// Removes a string variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveStringVar (int i) {
            // Set variable as invalid
            m_StringVars[i].SetAsInvalid();
            // Create the list
            var stringList = new List<ConcreteStringVar>(m_StringVars);
            // Remove variable from list
            stringList.RemoveAt(i);
            // Recreate array
            m_StringVars = stringList.ToArray();
        }

        /// <summary>
        /// Removes a Vector3 variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveVector3Var (int i) {
            // Set variable as invalid
            m_Vector3Vars[i].SetAsInvalid();
            // Create the list
            var vector3List = new List<ConcreteVector3Var>(m_Vector3Vars);
            // Remove variable from list
            vector3List.RemoveAt(i);
            // Recreate array
            m_Vector3Vars = vector3List.ToArray();
        }

        /// <summary>
        /// Removes a Rect variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveRectVar (int i) {
            // Set variable as invalid
            m_RectVars[i].SetAsInvalid();
            // Create the list
            var rectList = new List<ConcreteRectVar>(m_RectVars);
            // Remove variable from list
            rectList.RemoveAt(i);
            // Recreate array
            m_RectVars = rectList.ToArray();
        }

        /// <summary>
        /// Removes a Color variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveColorVar (int i) {
            // Set variable as invalid
            m_ColorVars[i].SetAsInvalid();
            // Create the list
            var colorList = new List<ConcreteColorVar>(m_ColorVars);
            // Remove variable from list
            colorList.RemoveAt(i);
            // Recreate array
            m_ColorVars = colorList.ToArray();
        }

        /// <summary>
        /// Removes a Quaternion variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveQuaternionVar (int i) {
            // Set variable as invalid
            m_QuaternionVars[i].SetAsInvalid();
            // Create the list
            var quaternionList = new List<ConcreteQuaternionVar>(m_QuaternionVars);
            // Remove variable from list
            quaternionList.RemoveAt(i);
            // Recreate array
            m_QuaternionVars = quaternionList.ToArray();
        }

        /// <summary>
        /// Removes a GameObject variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveGameObjectVar (int i) {
            // Set variable as invalid
            m_GameObjectVars[i].SetAsInvalid();
            // Create the list
            var gameObjectList = new List<ConcreteGameObjectVar>(m_GameObjectVars);
            // Remove variable from list
            gameObjectList.RemoveAt(i);
            // Recreate array
            m_GameObjectVars = gameObjectList.ToArray();
        }

        /// <summary>
        /// Removes a Texture variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveTextureVar (int i) {
            // Set variable as invalid
            m_TextureVars[i].SetAsInvalid();
            // Create the list
            var textureList = new List<ConcreteTextureVar>(m_TextureVars);
            // Remove variable from list
            textureList.RemoveAt(i);
            // Recreate array
            m_TextureVars = textureList.ToArray();
        }

        /// <summary>
        /// Removes a Material variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveMaterialVar (int i) {
            // Set variable as invalid
            m_MaterialVars[i].SetAsInvalid();
            // Create the list
            var materialList = new List<ConcreteMaterialVar>(m_MaterialVars);
            // Remove variable from list
            materialList.RemoveAt(i);
            // Recreate array
            m_MaterialVars = materialList.ToArray();
        }

        /// <summary>
        /// Removes an Object variable from the blackboard.
        /// <param name="i">The variable index to be removed.</param>
        /// </summary> 
        public void RemoveObjectVar (int i) {
            // Set variable as invalid
            m_ObjectVars[i].SetAsInvalid();
            // Create the list
            var objectList = new List<ConcreteObjectVar>(m_ObjectVars);
            // Remove variable from list
            objectList.RemoveAt(i);
            // Recreate array
            m_ObjectVars = objectList.ToArray();
        }

        /// <summary>
        /// Removes a DynamicList from the blackboard.
        /// <param name="i">The dynamic list index to be removed.</param>
        /// </summary> 
        public void RemoveDynamicList (int i) {
            // Set variable as invalid
            m_DynamicLists[i].SetAsInvalid();
            // Create the list
            var dynamicListList = new List<ConcreteDynamicList>(m_DynamicLists);
            // Remove variable from list
            dynamicListList.RemoveAt(i);
            // Recreate array
            m_DynamicLists = dynamicListList.ToArray();
        }

        /// <summary>
        /// Removes a fsmEvent from the blackboard.
        /// <param name="i">The fsmEvent index to be removed.</param>
        /// </summary> 
        public void RemoveFsmEvent (int i) {
            // Set variable as invalid
            m_ConcreteFsmEvents[i].SetAsInvalid();
            // Create the list
            var fsmEventList = new List<ConcreteFsmEvent>(m_ConcreteFsmEvents);
            // Remove variable from list
            fsmEventList.RemoveAt(i);
            // Recreate array
            m_ConcreteFsmEvents = fsmEventList.ToArray();
        }

        /// <summary>
        /// Removes the supplied variable from the blackboard.
        /// <param name="variable">The variable to be removed.</param>
        /// </summary> 
        public void RemoveVariable (Variable variable) {
            if (variable != null && variable.blackboard == this) {
                // Float
                if (variable is ConcreteFloatVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_FloatVars.Length; i++) {
                        // The variable has the current index?
                        if (m_FloatVars[i] == variable) {
                            // Remove variable
                            this.RemoveFloatVar(i);
                            break;
                        }
                    }
                }
                // Int
                else if (variable is ConcreteIntVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_IntVars.Length; i++) {
                        // The variable has the current index?
                        if (m_IntVars[i] == variable) {
                            // Remove variable
                            this.RemoveIntVar(i);
                            break;
                        }
                    }
                    return;
                }
                // Bool
                else if (variable is ConcreteBoolVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_BoolVars.Length; i++) {
                        // The variable has the current index?
                        if (m_BoolVars[i] == variable) {
                            // Remove variable
                            this.RemoveBoolVar(i);
                            break;
                        }
                    }
                    return;
                }
                // String
                else if (variable is ConcreteStringVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_StringVars.Length; i++) {
                        // The variable has the current index?
                        if (m_StringVars[i] == variable) {
                            // Remove variable
                            this.RemoveStringVar(i);
                            break;
                        }
                    }
                    return;
                }
                // Vector3
                else if (variable is ConcreteVector3Var) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_Vector3Vars.Length; i++) {
                        // The variable has the current index?
                        if (m_Vector3Vars[i] == variable) {
                            // Remove variable
                            this.RemoveVector3Var(i);
                            break;
                        }
                    }
                    return;
                }
                // Rect
                else if (variable is ConcreteRectVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_RectVars.Length; i++) {
                        // The variable has the current index?
                        if (m_RectVars[i] == variable) {
                            // Remove variable
                            this.RemoveRectVar(i);
                            break;
                        }
                    }
                    return;
                }
                // Color
                else if (variable is ConcreteColorVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_ColorVars.Length; i++) {
                        // The variable has the current index?
                        if (m_ColorVars[i] == variable) {
                            // Remove variable
                            this.RemoveColorVar(i);
                            break;
                        }
                    }
                    return;
                }
                // Quaternion
                else if (variable is ConcreteQuaternionVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_QuaternionVars.Length; i++) {
                        // The variable has the current index?
                        if (m_QuaternionVars[i] == variable) {
                            // Remove variable
                            this.RemoveQuaternionVar(i);
                            break;
                        }
                    }
                    return;
                }
                // GameObject
                else if (variable is ConcreteGameObjectVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_GameObjectVars.Length; i++) {
                        // The variable has the current index?
                        if (m_GameObjectVars[i] == variable) {
                            // Remove variable
                            this.RemoveGameObjectVar(i);
                            break;
                        }
                    }
                    return;
                }
                // Texture
                else if (variable is ConcreteTextureVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_TextureVars.Length; i++) {
                        // The variable has the current index?
                        if (m_TextureVars[i] == variable) {
                            // Remove variable
                            this.RemoveTextureVar(i);
                            break;
                        }
                    }
                    return;
                }
                // Material
                else if (variable is ConcreteMaterialVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_MaterialVars.Length; i++) {
                        // The variable has the current index?
                        if (m_MaterialVars[i] == variable) {
                            // Remove variable
                            this.RemoveMaterialVar(i);
                            break;
                        }
                    }
                    return;
                }
                // Object
                else if (variable is ConcreteObjectVar) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_ObjectVars.Length; i++) {
                        // The variable has the current index?
                        if (m_ObjectVars[i] == variable) {
                            // Remove variable
                            this.RemoveObjectVar(i);
                            break;
                        }
                    }
                    return;
                }
                // DynamicList
                else if (variable is ConcreteDynamicList) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_DynamicLists.Length; i++) {
                        // The variable has the current index?
                        if (m_DynamicLists[i] == variable) {
                            // Remove variable
                            this.RemoveDynamicList(i);
                            break;
                        }
                    }
                    return;
                }
                // FsmEvent
                else if (variable is FsmEvent) {
                    // Searchs for the variable index
                    for (int i = 0; i < m_ConcreteFsmEvents.Length; i++) {
                        // The variable has the current index?
                        if (m_ConcreteFsmEvents[i] == variable) {
                            // Remove variable
                            this.RemoveFsmEvent(i);
                            break;
                        }
                    }
                    return;
                }
            }
        }
        #endregion Remove Variables
    }
}