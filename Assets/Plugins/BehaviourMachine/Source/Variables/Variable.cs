//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {

    /// <summary>
    /// Base class for variables.
    /// <seealso cref="BehaviourMachine.InternalBlackboard" />
    /// </summary>
    [System.Serializable]
    public class Variable {

        #region Members
        [SerializeField]
        int m_ID;
        [SerializeField]
        string m_Name;
        [SerializeField]
        InternalBlackboard m_Blackboard;
        [SerializeField]
        bool m_IsConstant;
        #endregion Members

        
        #region Properties
        /// <summary>
        /// Returns the variable unique id.
        /// </summary>
        public int id {get {return m_ID;}}

        /// <summary>
        /// Returns the variable name.
        /// </summary>
        public virtual string name {get {return m_Name;} set {m_Name = m_Blackboard != null ? m_Blackboard.GetUniqueVariableName(value) : value;}}

        /// <summary>
        /// Returns true if the variable is invalid; false otherwise.
        /// An invalid variable should be ignored or recreated.
        /// </summary>
        public bool isInvalid {get {return isNone && name != " ";}}

        /// <summary>
        /// Returns whenever the variable is a constant.
        /// </summary>
        public bool isConstant {get {return m_Blackboard == null && m_IsConstant;}}

        /// <summary>
        /// Returns whenever the variable is none (think null in programming language).
        /// </summary>
        public bool isNone {get {return m_Blackboard == null && !m_IsConstant;}}

        /// <summary>
        /// Returns whenever the variable is a global variable.
        /// </summary>
        public bool isGlobal {get {return m_Blackboard is InternalGlobalBlackboard;}}

        /// <summary>
        /// Returns the variable blackboard.
        /// </summary>
        public InternalBlackboard blackboard {get {return m_Blackboard;}}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public virtual object genericValue {get {return null;} set {}}
        #endregion Properties


        #region Constructors
        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public Variable () {
            m_Blackboard = null;
            m_IsConstant = false;
            m_Name = " ";
        }

        /// <summary>
        /// Constructor for variables that will be added to a .
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public Variable (string name, InternalBlackboard blackboard, int id) {
            m_Blackboard = blackboard;
            m_IsConstant = false;
            m_ID = id;
            this.name = name;
        }
        #endregion Constructors

        #region Public Methods
        /// <summary>
		/// After call this function the variable will be a constant and should not be in the blackboard.
        /// </summary>
        public void SetAsConstant () {
            m_Blackboard = null;
            m_IsConstant = true;
            m_Name = " ";
        }

        /// <summary>
        /// After call this function the variable will be none and should not be in the blackboard.
        /// </summary>
        public void SetAsNone () {
            m_Blackboard = null;
            m_IsConstant = false;
            m_Name = " ";
        }

        /// <summary>
        /// After call this function the variable will be invalid and should be recreated.
        /// </summary>
        public void SetAsInvalid () {
            m_Blackboard = null;
            m_IsConstant = false;
            m_Name = "Invalid";
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// </summary>
        public virtual void OnValidate () {}
        #endregion Public Methods
    }

    /// <summary>
    /// Base class to store float values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteFloatVar))]
    public abstract class FloatVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract float Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (float)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public FloatVar () : base () {}

        /// <summary>
        /// Constructor for float variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public FloatVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// User-defined conversion from FloatVar to float
        /// </summary>
        public static implicit operator float (FloatVar variable) {
            return variable.Value;
        }

        /// <summary>
        /// User-defined conversion from float to FloatVar
        /// </summary>
        public static implicit operator FloatVar (float value) {
            return new ConcreteFloatVar(value);
        }
    }

    /// <summary>
    /// Base class to store int values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteIntVar))]
    public abstract class IntVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract int Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (int)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public IntVar () : base () {}

        /// <summary>
        /// Constructor for int variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public IntVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// User-defined conversion from IntVar to int
        /// </summary>
        public static implicit operator int (IntVar variable) {
            return variable.Value;
        }

        /// <summary>
        /// User-defined conversion from int to IntVar
        /// </summary>
        public static implicit operator IntVar (int value) {
            return new ConcreteIntVar(value);
        }
    }

    /// <summary>
    /// Base class to store bool values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteBoolVar))]
    public abstract class BoolVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract bool Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (bool)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public BoolVar () : base () {}

        /// <summary>
        /// Constructor for bool variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public BoolVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// User-defined conversion from BoolVar to bool
        /// </summary>
        public static implicit operator bool (BoolVar variable) {
            return variable.Value;
        }

        /// <summary>
        /// User-defined conversion from bool to BoolVar
        /// </summary>
        public static implicit operator BoolVar (bool value) {
            return new ConcreteBoolVar(value);
        }
    }

    /// <summary>
    /// Base class to store string values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteStringVar))]
    public abstract class StringVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract string Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (string)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public StringVar () : base () {}

        /// <summary>
        /// Constructor for string variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public StringVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {Value = string.Empty;}

        /// <summary>
        /// User-defined conversion from StringVar to string
        /// </summary>
        public static implicit operator string (StringVar variable) {
            return variable.Value;
        }

        /// <summary>
        /// User-defined conversion from string to StringVar
        /// </summary>
        public static implicit operator StringVar (string value) {
            return new ConcreteStringVar(value);
        }
    }

    /// <summary>
    /// Base class to store Vector3 values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteVector3Var))]
    public abstract class Vector3Var : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract Vector3 Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (Vector3)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public Vector3Var () : base () {}

        /// <summary>
        /// Constructor for Vector3 variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public Vector3Var (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// The Vector2 value of this variable.
        /// Value.z is ignored.
        /// </summary>
        public Vector2 vector2Value {
            get {return new Vector2(this.Value.x, this.Value.y);}
            set {this.Value = new Vector3(value.x, value.y, this.Value.z);}
        }

        /// <summary>
        /// User-defined conversion from Vector3Var to Vector3
        /// </summary>
        public static implicit operator Vector3 (Vector3Var variable) {
            return variable.Value;
        }

        /// <summary>
        /// User-defined conversion from Vector3 to Vector3Var
        /// </summary>
        public static implicit operator Vector3Var (Vector3 value) {
            return new ConcreteVector3Var(value);
        }
    }

    /// <summary>
    /// Base class to store Rect values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteRectVar))]
    public abstract class RectVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract Rect Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (Rect)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public RectVar () : base () {}

        /// <summary>
        /// Constructor for Rect variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public RectVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// User-defined conversion from RectVar to Rect
        /// </summary>
        public static implicit operator Rect (RectVar variable) {
            return variable.Value;
        }

        /// <summary>
        /// User-defined conversion from Rect to RectVar
        /// </summary>
        public static implicit operator RectVar (Rect value) {
            return new ConcreteRectVar(value);
        }
    }

    /// <summary>
    /// Base class to store Color values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteColorVar))]
    public abstract class ColorVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract Color Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (Color)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ColorVar () : base () {}

        /// <summary>
        /// Constructor for Color variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ColorVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// User-defined conversion from ColorVar to Color
        /// </summary>
        public static implicit operator Color (ColorVar variable) {
            return variable.Value;
        }

        /// <summary>
        /// User-defined conversion from Color to ColorVar
        /// </summary>
        public static implicit operator ColorVar (Color value) {
            return new ConcreteColorVar(value);
        }
    }

    /// <summary>
    /// Base class to store Quaternion values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteQuaternionVar))]
    public abstract class QuaternionVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract Quaternion Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (Quaternion)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public QuaternionVar () : base () {}

        /// <summary>
        /// Constructor for Quaternion variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public QuaternionVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// User-defined conversion from QuaternionVar to Quaternion
        /// </summary>
        public static implicit operator Quaternion (QuaternionVar variable) {
            return variable.Value;
        }

        /// <summary>
        /// User-defined conversion from Quaternion to QuaternionVar
        /// </summary>
        public static implicit operator QuaternionVar (Quaternion value) {
            return new ConcreteQuaternionVar(value);
        }
    }

    /// <summary>
    /// Base class to store GameObject values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteGameObjectVar))]
    public abstract class GameObjectVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract GameObject Value {get; set;}

        /// <summary>
        /// The game object transform.
        /// </summary>
        public abstract Transform transform {get;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (GameObject)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public GameObjectVar () : base () {}

        /// <summary>
        /// Constructor for GameObject variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public GameObjectVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// User-defined conversion from GameObjectVar to GameObject
        /// </summary>
        public static implicit operator GameObject (GameObjectVar variable) {
            return variable.Value;
        }

        /// <summary>
        /// User-defined conversion from GameObject to GameObjectVar
        /// </summary>
        public static implicit operator GameObjectVar (GameObject value) {
            return new ConcreteGameObjectVar(value);
        }
    }

    /// <summary>
    /// Base class to store Texture values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteTextureVar))]
    public abstract class TextureVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract Texture Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (Texture)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public TextureVar () : base () {}

        /// <summary>
        /// Constructor for Texture variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public TextureVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// User-defined conversion from TextureVar to Texture.
        /// <param name="variable">.</param>
        /// </summary>
        public static implicit operator Texture (TextureVar variable) {
            return variable.Value;
        }
        /// <summary>
        /// User-defined conversion from Texture to TextureVar.
        /// </summary>
        public static implicit operator TextureVar (Texture value) {
            return new ConcreteTextureVar(value);
        }
    }

    /// <summary>
    /// Base class to store Material values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteMaterialVar))]
    public abstract class MaterialVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract Material Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (Material)value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public MaterialVar () : base () {}

        /// <summary>
        /// Constructor for Material variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public MaterialVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// User-defined conversion from MaterialVar to Material
        /// </summary>
        public static implicit operator Material (MaterialVar variable) {
            return variable.Value;
        }
        /// <summary>
        /// User-defined conversion from Material to MaterialVar
        /// </summary>
        public static implicit operator MaterialVar (Material value) {
            return new ConcreteMaterialVar(value);
        }
    }

    /// <summary>
    /// Base class to store Object values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteObjectVar))]
    public abstract class ObjectVar : Variable {

        /// <summary>
        /// Variable value.
        /// </summary>
        public abstract UnityEngine.Object Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = (UnityEngine.Object)value;}}

        /// <summary>
        /// Returns the type of objects that should be stored by this variable.
        /// </summary>
        public virtual System.Type ObjectType {get{return typeof(UnityEngine.Object);} set {}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ObjectVar () : base () {}

        /// <summary>
        /// Constructor for Object variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ObjectVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// Call this to validate the Value that is stored by this variable.
        /// If the Value is not of type object it will be removed from the variable.
        /// </summary>
        public override void OnValidate () {
            if (Value != null && !ObjectType.IsAssignableFrom(Value.GetType()))
                Value = null;
        }

        /// <summary>
        /// User-defined conversion from MaterialVar to Object
        /// </summary>
        public static implicit operator UnityEngine.Object (ObjectVar variable) {
            return variable.Value;
        }
        /// <summary>
        /// User-defined conversion from UnityEngine.Object to ObjectVar
        /// </summary>
        public static implicit operator ObjectVar (UnityEngine.Object value) {
            return new ConcreteObjectVar(value);
        }
    }

    /// <summary>
    /// Base class to store Material values.
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteDynamicList))]
    public abstract class DynamicList : Variable {

        /// <summary>
        /// The list value.
        /// </summary>
        public abstract IList<System.Object> Value {get; set;}

        /// <summary>
        /// A generic get and set value.
        /// </summary>
        public override object genericValue {get {return this.Value;} set {this.Value = value as IList<System.Object>;}}

        /// <summary>
        /// Get and set an object at the supplied index.
        /// </summary>
        public abstract object this [int index] {
            get;
            set;
        }

        /// <summary>
        /// Number of elements in the list.
        /// </summary>
        public abstract int Count {
            get;
        }

        /// <summary>
        /// Adds an item to the list.
        /// <param name="value">The value to be added.</param>
        /// </summary>
        public abstract void Add (object value);

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public abstract void Clear ();

        /// <summary>
        /// Determines whether the list contains a specific value.
        /// <param name="value">The value to search for.</param>
        /// <returns>True if the list has the supplied value; false otherwise.</returns>
        /// </summary>
        public abstract bool Contains (object value);

        /// <summary>
        /// Determines the index of a specific item in the list.
        /// <param name="value">The value to search value.</param>
        /// <returns>The index of the supplied element.</returns>
        /// </summary>
        public abstract int IndexOf (object value);

        /// <summary>
        /// Inserts an item to the list at the specified index.
        /// <param name="index">The index to insert the element.</param>
        /// <param name="value">The element to be inserted in the list.</param>
        /// </summary>
        public abstract void Insert (int index, object value);

        /// <summary>
        /// Removes an element from the list.
        /// <param name="value">The element to be removed.</param>
        /// </summary>
        public abstract void Remove (object value);

        /// <summary>
        /// Removes an element on the supplied index.
        /// <param name="index">.</param>
        /// </summary>
        public abstract void RemoveAt (int index);

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public DynamicList () : base () {}

        /// <summary>
        /// Constructor for DynamicList variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public DynamicList (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// FsmEvents are used to change the enabled state in the FSM.
    /// <seealso cref="BehaviourMachine.StateMachine" />
    /// </summary>
    [System.Serializable]
    [ConcreteClass(typeof(ConcreteFsmEvent))]
    public class FsmEvent : Variable {

        #region Members
        [HideInInspector]
        [SerializeField]
        bool m_IsSystem;
        #endregion Members


        #region Properties
        /// <summary>
        /// Returns the variable name.
        /// Prohibits changing names of system events.
        /// </summary>
        public override string name {get {return base.name;} set {if (!m_IsSystem) base.name = value;}}

        /// <summary>
        /// Returns True if it's a system event; false otherwise.
        /// </summary>
        public bool isSystem {get {return m_IsSystem;}}
        #endregion Properties


        /// <summary>
        /// Constructor for constant FsmEvents.
        /// </summary>
        public FsmEvent () : base () {}

        /// <summary>
        /// Constructor for FsmEvent that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// <param name="isSystem">Returns true if this is a system event; otherwise false.</param>
        /// </summary>
        public FsmEvent (string name, InternalBlackboard blackboard, int id, bool isSystem) : base (name, blackboard, id) {
            m_IsSystem = isSystem;
        }
    }
}