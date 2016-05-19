//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {

    /// <summary>
    /// Store float values.
    /// </summary>
    [System.Serializable]
    public class ConcreteFloatVar : FloatVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public float value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override float Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteFloatVar () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteFloatVar (float value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for float variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteFloatVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Store int values.
    /// </summary>
    [System.Serializable]
    public class ConcreteIntVar : IntVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public int value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override int Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteIntVar () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteIntVar (int value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for int variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteIntVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Store bool values.
    /// </summary>
    [System.Serializable]
    public class ConcreteBoolVar : BoolVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public bool value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override bool Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteBoolVar () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteBoolVar (bool value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for bool variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteBoolVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

        /// <summary>
    /// Store string values.
    /// </summary>
    [System.Serializable]
    public class ConcreteStringVar : StringVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public string value = string.Empty;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override string Value {get {return value;} set{this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteStringVar () : base () {value = string.Empty;}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteStringVar (string value) {
            this.SetAsConstant();
            this.Value = value;
        }

        /// <summary>
        /// Constructor for string variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteStringVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {Value = string.Empty;}
    }

    /// <summary>
    /// Store Vector3 values.
    /// </summary>
    [System.Serializable]
    public class ConcreteVector3Var : Vector3Var {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public Vector3 value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Vector3 Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteVector3Var () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteVector3Var (Vector3 value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for Vector3 variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteVector3Var (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Store Rect values.
    /// </summary>
    [System.Serializable]
    public class ConcreteRectVar : RectVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public Rect value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Rect Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteRectVar () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteRectVar (Rect value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for Rect variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteRectVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Store Color values.
    /// </summary>
    [System.Serializable]
    public class ConcreteColorVar : ColorVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public Color value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Color Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteColorVar () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteColorVar (Color value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for Color variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteColorVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Store Quaternion values.
    /// </summary>
    [System.Serializable]
    public class ConcreteQuaternionVar : QuaternionVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public Quaternion value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Quaternion Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteQuaternionVar () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteQuaternionVar (Quaternion value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for Quaternion variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteQuaternionVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Store GameObject values.
    /// </summary>
    [System.Serializable]
    public class ConcreteGameObjectVar : GameObjectVar {

        [System.NonSerialized]
        Transform m_Transform;

        /// <summary>
        /// Serialized value.
        /// </summary>
        public GameObject value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override GameObject Value {get {return value;} set {this.value = value; m_Transform = null;}}

        /// <summary>
        /// The game object transform.
        /// Automatically caches the transform.
        /// </summary>
        public override Transform transform {
            get {
                if (m_Transform == null && this.value != null)
                    m_Transform = this.value.transform;
                return m_Transform;
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteGameObjectVar () : base () {}

        /// <summary>
        /// Constructor for none variables.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteGameObjectVar (GameObject value) : base () {
            this.value = value;
        }

        /// <summary>
        /// Constructor for GameObject variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteGameObjectVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// </summary>
        public override void OnValidate () {
            m_Transform = null;
        }
    }

    /// <summary>
    /// Store Texture values.
    /// </summary>
    [System.Serializable]
    public class ConcreteTextureVar : TextureVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public Texture value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Texture Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteTextureVar () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteTextureVar (Texture value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for Texture variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteTextureVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Store Material values.
    /// </summary>
    [System.Serializable]
    public class ConcreteMaterialVar : MaterialVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        public Material value;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Material Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteMaterialVar () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteMaterialVar (Material value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for Material variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteMaterialVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Store Object values.
    /// </summary>
    [System.Serializable]
    public class ConcreteObjectVar : ObjectVar {

        /// <summary>
        /// Serialized value.
        /// </summary>
        [ObjectValue("objectType")]
        public UnityEngine.Object value;

        /// <summary>
        /// The type of the objects that can be added to this variable.
        /// </summary>
        [HideInInspector]
        [UnityObjectType]
        public string objectType = string.Empty;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override UnityEngine.Object Value {get {return value;} set {this.value = value;}}

        /// <summary>
        /// Returns the type of objects that should be stored by this variable.
        /// </summary>
        public override System.Type ObjectType {
            get {
                return UnityObjectTypeAttribute.GetObjectType(this.objectType);
            }
            set {
                objectType = value != null ? value.ToString() : string.Empty;
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ConcreteObjectVar () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="value">The value of the variable.</param>
        /// </summary>
        public ConcreteObjectVar (UnityEngine.Object value) {
            this.SetAsConstant();
            this.value = value;
        }

        /// <summary>
        /// Constructor for Object variables that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// </summary>
        public ConcreteObjectVar (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Base class to store List values.
    /// </summary>
    [System.Serializable]
    public class ConcreteDynamicList : DynamicList {

        [System.NonSerialized]
        List<System.Object> m_List = new List<System.Object>();

        /// <inheritdoc/>
        public override IList<System.Object> Value {get {return m_List;} set {m_List = new List<System.Object>(value);}}

        /// <inheritdoc/>
        public override object this [int index] {
            get {return m_List[index];}
            set {m_List[index] = value;}
        }

        /// <inheritdoc/>
        public override int Count {
            get {return m_List.Count;}
        }

        /// <inheritdoc/>
        public override void Add (object value) {
            m_List.Add(value);
        }

        /// <inheritdoc/>
        public override void Clear () {
            m_List.Clear();
        }

        /// <inheritdoc/>
        public override bool Contains (object value) {
            return m_List.Contains(value);
        }

        /// <inheritdoc/>
        public override int IndexOf (object value) {
            return m_List.IndexOf(value);
        }

        /// <inheritdoc/>
        public override void Insert (int index, object value) {
            m_List.Insert (index, value);
        }

        /// <inheritdoc/>
        public override void Remove (object value) {
            m_List.Remove(value);
        }

        /// <inheritdoc/>
        public override void RemoveAt (int index) {
            m_List.RemoveAt (index);
        }

        /// <inheritdoc/>
        public ConcreteDynamicList () : base () {}

        /// <inheritdoc/>
        public ConcreteDynamicList (string name, InternalBlackboard blackboard, int id) : base (name, blackboard, id) {}
    }

    /// <summary>
    /// Concrete FsmEvent.
    /// <seealso cref="BehaviourMachine.StateMachine" />
    /// </summary>
    [System.Serializable]
    public class ConcreteFsmEvent : FsmEvent {
        /// <summary>
        /// Constructor for constant FsmEvents.
        /// </summary>
        public ConcreteFsmEvent () : base () {}

        /// <summary>
        /// Constructor for ConcreteFsmEvent that will be added to a blackboard.
        /// <param name="name">The name of the variable.</param>
        /// <param name="blackboard">The variable blackboard.</param>
        /// <param name="id">The unique id of the variable</param>
        /// <param name="isSystem">Returns true if this is a system event; otherwise false.</param>
        /// </summary>
        public ConcreteFsmEvent (string name, InternalBlackboard blackboard, int id, bool isSystem) : base (name, blackboard, id, isSystem) {}
    }
}
