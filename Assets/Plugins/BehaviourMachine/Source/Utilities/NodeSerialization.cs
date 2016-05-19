//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {

    public enum FieldType {NotSupported, Int, String, Float, Enum, Bool, Vector2, Vector3, Vector4, Quaternion, Rect, Color, LayerMask, AnimationCurve, State, UnityObject, Constant, None, FloatVar, IntVar, BoolVar, StringVar, Vector3Var, RectVar, ColorVar, QuaternionVar, GameObjectVar, TextureVar, MaterialVar, ObjectVar, FsmEvent, Array, Generic, DynamicList}

    /// <summary>
    /// Custom serialization for nodes.
    /// A NodeSerialization store and retrieves node information during an assembly reload.
    /// <seealso cref="BehaviourMachine.ActionNode" />
    /// <seealso cref="BehaviourMachine.InternalBehaviourTree" />
    /// </summary>
    [Serializable]
    public sealed class NodeSerialization {

        #region Static Properties
        static Dictionary<Type, FieldInfo[]> s_NodeFields = new Dictionary<Type, FieldInfo[]> ();
        static Dictionary<Type, int> s_NodeHash = new Dictionary<Type, int> ();
        static Dictionary<Type, string> s_StringHash = new Dictionary<Type, string> ();
        static FieldInfo s_IdField;

        public static FieldInfo idField {
            get {
                if (s_IdField == null)
                    s_IdField = typeof(ActionNode).GetField("instanceID", BindingFlags.Instance | BindingFlags.Public);
                return s_IdField;
            }
        }
        #endregion Static Properties

        #region Static Methods
        /// <summary>
        /// Returns the supplied node type hash code.
        /// <param name="nodeType">The type of the node.</param>
        /// <returns>The type hashcode.</returns>
        /// </summary>
        static int TypeToHash (Type nodeType) {
            int hash;
            if (s_NodeHash.TryGetValue(nodeType, out hash))
                return hash;

            hash = TypeToStringHash(nodeType).GetHashCode();
            s_NodeHash.Add(nodeType, hash);

            return hash;
        }

        /// <summary>
        /// Returns the type hash string.
        /// <param name="type">The target type.</param>
        /// <returns>The type hash as string.</returns>
        /// </summary>
        static string TypeToStringHash (Type type) {
            string stringHash;
            if (s_StringHash.TryGetValue(type, out stringHash))
                return stringHash;

            stringHash = string.Empty;
            var fields = GetSerializedFields (type);

            for (int i = 0; i < fields.Length; i++) {
                var fieldType = fields[i].FieldType;
                stringHash += fieldType.ToString() + "-";

                if (!fieldType.IsValueType && !fieldType.IsAbstract && !fieldType.IsInterface && !typeof(UnityEngine.Object).IsAssignableFrom(fieldType))
                    stringHash += TypeToStringHash(fieldType);
            }

            s_StringHash.Add(type, stringHash);

            return stringHash;
        }

        /// <summary>
        /// Returns the serialized fields of the supplied type. 
        /// For the time being only public fields are serialized.
        /// <param name="type">The target type to get the serialized fields.</param>
        /// <returns>The serialized fields of the supplied type.</returns>
        /// </summary>
        public static FieldInfo[] GetSerializedFields (Type type) {
            // The type's fields are already loaded?
            FieldInfo[] fields;
            if (NodeSerialization.s_NodeFields.TryGetValue (type, out fields))
                return fields;

            // Lookup table to sort fields
            Dictionary<Type, int> lookup = new Dictionary<Type, int>();
            int count = 0;
            lookup[type] = count++;
            Type parent = type.BaseType;
            while (parent != null) {
                lookup[parent] = count;
                count++;
                parent = parent.BaseType;
            }

            // Get public fields and sort
            fields = type.GetFields (BindingFlags.Public | BindingFlags.Instance).OrderByDescending(field => lookup[field.DeclaringType]).ToArray();
            NodeSerialization.s_NodeFields.Add (type, fields);     // store fields in dictionary
            return fields;
        }
        #endregion Static Methods

        #region Animation Curve
        /// <summary>
        /// Contanier class to store AnimationCurve values.
        /// </summary>
        [System.Serializable]
        sealed class AnimationCurveContainer {
            /// <summary>
            /// The curve stored by this object.
            /// </summary>
            public AnimationCurve curve;

            /// <summary>
            /// Creates a new AnimationCurveContainer.
            /// <summary>
            public AnimationCurveContainer (AnimationCurve curve) {
                this.curve = curve;
            }

            /// <summary>
            // User-defined conversion from Container to AnimationCurver
            /// </summary>
            public static implicit operator AnimationCurve (AnimationCurveContainer container) {
                return container.curve;
            }
            /// <summary>
            // User-defined conversion from AnimationCurver to Container
            /// </summary>
            public static implicit operator AnimationCurveContainer (AnimationCurve curve) {
                return new AnimationCurveContainer(curve);
            }
        }
        #endregion Animation Curve

        #region Members
        [SerializeField]
        List<string> m_NodeTypes = new List<string>();          // Store the node type
        [SerializeField]
        List<int> m_NodeTypeHash = new List<int>();             // Store the node type hash
        [SerializeField]
        List<int> m_NodeFieldStartIndex = new List<int>();      // store the index of the first field of a node in m_FieldBytes
        [SerializeField]
        List<int> m_BranchIndex = new List<int>();              // store the branch index of the node
        [SerializeField]
        List<UnityEngine.Object> m_UnityObjectValue = new List<UnityEngine.Object>();       // store UnityEngine.Object values
        [SerializeField]
        List<int> m_IntValue = new List<int>();                 // Store int values
        [SerializeField]
        List<float> m_FloatValue = new List<float>();           // Store float values
        [SerializeField]
        List<string> m_StringValue = new List<string>();        // Store string values
        [SerializeField]
        List<AnimationCurveContainer> m_CurveValue = new List<AnimationCurveContainer>();   // Store animation values
        [SerializeField]
        List<string> m_GenericFieldType = new List<string>();   // store other classes types
        [SerializeField]
        List<int> m_GenericFieldHash = new List<int>();         // store other classes hash types
        [SerializeField]
        List<int> m_GenericFieldSize = new List<int>();         // Store number of fields in a generic type
        [SerializeField]
        List<int> m_FieldIndex = new List<int>();               // The field index value
        [SerializeField]
        List<FieldType> m_FieldType = new List<FieldType>();    // Store the field type
        [SerializeField]
        List<string> m_FieldPath = new List<string>();          // Store the field path

        [System.NonSerialized]
        int m_NextFieldIndex = 0;
        [System.NonSerialized]
        bool m_ResaveNodes = false;
        [System.NonSerialized]
        InternalBlackboard m_Blackboard;
        [System.NonSerialized]
        INodeOwner m_NodeOwner;
        [System.NonSerialized]
        GameObject m_GameObject;
        [System.NonSerialized]
        Dictionary<int, List<ActionNode>> m_BranchChildren;
        #endregion Members

        #region Properties
        /// <summary>
        /// Returns true if the nodes need to be resaved.
        /// </summary>
        public bool resaveNodes {get {return m_ResaveNodes;}}
        #endregion Properties

        #region Private Methods
        /// <summary>
        /// Clears all data in the object.
        /// </summary>
        public void Clear () {
            m_NodeTypes.Clear();
            m_NodeTypeHash.Clear();
            m_NodeFieldStartIndex.Clear();
            m_UnityObjectValue.Clear();
            m_IntValue.Clear();
            m_FloatValue.Clear();
            m_StringValue.Clear();
            m_CurveValue.Clear();
            m_GenericFieldType.Clear();
            m_GenericFieldHash.Clear();
            m_GenericFieldSize.Clear();
            m_FieldIndex.Clear();
            m_FieldType.Clear();
            m_FieldPath.Clear();
            m_BranchIndex.Clear();
            m_NextFieldIndex = 0;
            m_GameObject = null;
            m_NodeOwner = null;
            m_Blackboard = null;
        }

        /// <summary>
        /// Saves a node.
        /// <param name="node">The target node to be saved.</param>
        /// </summary>
        void Save (ActionNode node) {
            // It's a valid node?
            if (node == null)
                return;

            Type type =  node.GetType();                    // get type
            m_NodeTypes.Add(type.ToString());               // save type
            m_NodeTypeHash.Add(TypeToHash(type));           // save type hash
            m_NodeFieldStartIndex.Add(m_NextFieldIndex);    // save the index of the first field
            m_BranchIndex.Add(node.branch != null ? node.branch.GetIndex() : -1);   // save the parent branch index

            // Save serialized fields
            FieldInfo[] fields = NodeSerialization.GetSerializedFields (type);
            for (int i = 0; i < fields.Length; i++) {
                var field = fields[i];
                SaveNodeField (field.FieldType, field.GetValue(node), field.Name);
                m_NextFieldIndex++;
            }
        }

        /// <summary>
        /// Saves a node field.
        /// <param name="fieldType">The fieldType of the field.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="fieldPath">The field path.</param>
        /// </summary>
        void SaveNodeField (Type fieldType, object value, string fieldPath) {

            // It's not a value type?
            if (!fieldType.IsValueType) {
                // String
                if (fieldType == typeof(string)) {
                    m_FieldType.Add(FieldType.String);
                    m_FieldPath.Add(fieldPath);
                    m_FieldIndex.Add(m_StringValue.Count);
                    m_StringValue.Add((string)value);
                }
                // Variable
                else if (typeof(BehaviourMachine.Variable).IsAssignableFrom(fieldType)) {
                    // Interface?
                    if (fieldType.IsInterface) {
                        m_FieldType.Add(FieldType.NotSupported);
                        m_FieldPath.Add(fieldPath);
                        m_FieldIndex.Add(0);
                        Print.LogError("Can't serialize or interface field: " + fieldType.ToString());
                        return;
                    }
                    
                    // Get the variable
                    var variable = value as BehaviourMachine.Variable ?? (Activator.CreateInstance(fieldType.IsAbstract ? TypeUtility.GetConcreteType(fieldType) : fieldType) as BehaviourMachine.Variable);
                    Type valueType = variable.GetType();

                    // Its none?
                    if (variable == null || variable.isNone) {
                        m_FieldType.Add(FieldType.None);    // Save type
                        m_FieldPath.Add(fieldPath);         // Save path
                        
                        // Save variable type
                        m_FieldIndex.Add(m_StringValue.Count);
                        // Save the ConcreteType
                        m_StringValue.Add((fieldType.IsAbstract ? TypeUtility.GetConcreteType(fieldType) : fieldType).FullName);
                    }
                    // Its a constant?
                    else if (variable.isConstant) {
                        // Save the variable as a generic object
                        m_FieldIndex.Add(m_GenericFieldType.Count);
                        m_GenericFieldType.Add(valueType.FullName);
                        m_GenericFieldHash.Add(TypeToHash(valueType));
                        m_FieldType.Add(FieldType.Constant);
                        m_FieldPath.Add(fieldPath);
                        // Save obj fields
                        FieldInfo[] fields = NodeSerialization.GetSerializedFields (valueType);
                        m_GenericFieldSize.Add(fields.Length);
                        for (int i = 0; i < fields.Length; i++) {
                            m_NextFieldIndex++;
                            var field = fields[i];
                            SaveNodeField (field.FieldType, field.GetValue(variable), fieldPath + "." + field.Name);
                        }
                        // m_NextFieldIndex--;
                    }
                    // It is a Blackboard or Global variable
                    else {
                        // Save path
                        m_FieldPath.Add(fieldPath);
                        // Save variable id
                        m_FieldIndex.Add(m_IntValue.Count);
                        m_IntValue.Add(variable.id);

                        // Get the variable type
                        System.Type variableType = variable.GetType();
                        // Save variable type
                        if (variableType == typeof(ConcreteIntVar))
                            m_FieldType.Add(FieldType.IntVar);
                        else if (variableType == typeof(ConcreteFloatVar))
                            m_FieldType.Add(FieldType.FloatVar);
                        else if (variableType == typeof(ConcreteBoolVar))
                            m_FieldType.Add(FieldType.BoolVar);
                        else if (variableType == typeof(ConcreteStringVar))
                            m_FieldType.Add(FieldType.StringVar);
                        else if (variableType == typeof(ConcreteVector3Var))
                            m_FieldType.Add(FieldType.Vector3Var);
                        else if (variableType == typeof(ConcreteRectVar))
                            m_FieldType.Add(FieldType.RectVar);
                        else if (variableType == typeof(ConcreteColorVar))
                            m_FieldType.Add(FieldType.ColorVar);
                        else if (variableType == typeof(ConcreteQuaternionVar))
                            m_FieldType.Add(FieldType.QuaternionVar);
                        else if (variableType == typeof(ConcreteGameObjectVar))
                            m_FieldType.Add(FieldType.GameObjectVar);
                        else if (variableType == typeof(ConcreteTextureVar))
                            m_FieldType.Add(FieldType.TextureVar);
                        else if (variableType == typeof(ConcreteMaterialVar))
                            m_FieldType.Add(FieldType.MaterialVar);
                        else if (variableType == typeof(ConcreteObjectVar))
                            m_FieldType.Add(FieldType.ObjectVar);
                        else if (variableType == typeof(ConcreteDynamicList))
                            m_FieldType.Add(FieldType.DynamicList);
                        else if (variableType == typeof(ConcreteFsmEvent))
                            m_FieldType.Add(FieldType.FsmEvent);
                    }

                }
                // UnityEngine.Object
                else if (fieldType.IsSubclassOf(typeof(UnityEngine.Object)) || fieldType == typeof(UnityEngine.Object)) {
                    m_FieldIndex.Add(m_UnityObjectValue.Count);
                    m_UnityObjectValue.Add(value as UnityEngine.Object);

                    // It's a InternalStateBehaviour?
                    if (typeof(InternalStateBehaviour).IsAssignableFrom(fieldType))
                        m_FieldType.Add(FieldType.State);
                    else
                        m_FieldType.Add(FieldType.UnityObject);

                    // Save the field path
                    m_FieldPath.Add(fieldPath);
                }
                // Array
                else if (fieldType.IsArray) {
                    // Get element type
                    Type elementType = fieldType.GetElementType();
                    if (elementType == null)
                        return;

                    // Get the array value
                    var array = value as Array ?? Array.CreateInstance (elementType, 0);

                    // Save the array field
                    m_FieldIndex.Add(m_GenericFieldType.Count);         // saves the array element type index
                    m_GenericFieldType.Add(elementType.FullName);       // saves the element type
                    m_GenericFieldHash.Add(TypeToHash(elementType));    // saves the element hash
                    m_GenericFieldSize.Add(array.Length);               // saves the array size
                    m_FieldType.Add(FieldType.Array);                   // saves the field type
                    m_FieldPath.Add(fieldPath);                         // saves the field path

                    // Saves the array elements
                    IEnumerator enumerator = array.GetEnumerator();
                    try {
                        int i = 0;
                        while (enumerator.MoveNext()) {
                            object current = enumerator.Current;
                            m_NextFieldIndex++;
                            SaveNodeField (elementType, current, fieldPath + ".data[" + i++.ToString() + "]");
                        }
                    }
                    finally {
                        // Dispose enumerator if an exception was thrown
                        IDisposable disposable = enumerator as IDisposable;
                        if (disposable != null)
                            disposable.Dispose ();
                    }
                }
                // Animation Curve
                else if (fieldType == typeof(AnimationCurve)) {
                    m_FieldType.Add(FieldType.AnimationCurve);
                    m_FieldIndex.Add(m_CurveValue.Count);
                    m_CurveValue.Add((AnimationCurve)value);
                    m_FieldPath.Add(fieldPath);
                }
                // Not Supported
                else {
                    m_FieldType.Add(FieldType.NotSupported);
                    m_FieldPath.Add(fieldPath);
                    m_FieldIndex.Add(0);
                    Print.LogWarning("Could not serialize \'" + fieldPath + "\'. Type \'" + fieldType + "\' not supported.");
                }
            }
            // int
            else if (fieldType == typeof(int)) {
                m_FieldType.Add(FieldType.Int);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_IntValue.Count);
                m_IntValue.Add((int)value);
            }
            // float
            else if (fieldType == typeof(float)) {
                m_FieldType.Add(FieldType.Float);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_FloatValue.Count);
                m_FloatValue.Add((float)value);
            }
            // bool
            else if (fieldType == typeof(bool)) {
                m_FieldType.Add(FieldType.Bool);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_IntValue.Count);
                m_IntValue.Add(((bool)value) ? 1 : 0);
            }
            // Vector2
            else if (fieldType == typeof(Vector2)) {
                m_FieldType.Add(FieldType.Vector2);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_FloatValue.Count);
                // Save Vector2 components
                var v2 = (Vector2) value;
                m_FloatValue.Add(v2.x);
                m_FloatValue.Add(v2.y);
            }
            // Vector3
            else if (fieldType == typeof(Vector3)) {
                m_FieldType.Add(FieldType.Vector3);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_FloatValue.Count);
                // Save Vector3 components
                var v3 = (Vector3) value;
                m_FloatValue.Add(v3.x);
                m_FloatValue.Add(v3.y);
                m_FloatValue.Add(v3.z);
            }
            // Vector4
            else if (fieldType == typeof(Vector4)) {
                m_FieldType.Add(FieldType.Vector4);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_FloatValue.Count);
                // Save Vector4 components
                var v4 = (Vector4) value;
                m_FloatValue.Add(v4.x);
                m_FloatValue.Add(v4.y);
                m_FloatValue.Add(v4.z);
                m_FloatValue.Add(v4.w);
            }
            // Quaternion
            else if (fieldType == typeof(Quaternion)) {
                m_FieldType.Add(FieldType.Quaternion);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_FloatValue.Count);
                // Save Quaternion components
                var q = (Quaternion) value;
                m_FloatValue.Add(q.x);
                m_FloatValue.Add(q.y);
                m_FloatValue.Add(q.z);
                m_FloatValue.Add(q.w);
            }
            // Rect
            else if (fieldType == typeof(Rect)) {
                m_FieldType.Add(FieldType.Rect);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_FloatValue.Count);
                // Save Rect components
                var rect = (Rect) value;
                m_FloatValue.Add(rect.x);
                m_FloatValue.Add(rect.y);
                m_FloatValue.Add(rect.width);
                m_FloatValue.Add(rect.height);
            }
            // Color
            else if (fieldType == typeof(Color)) {
                m_FieldType.Add(FieldType.Color);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_FloatValue.Count);
                // Save Color components
                var color = (Color) value;
                m_FloatValue.Add(color.r);
                m_FloatValue.Add(color.g);
                m_FloatValue.Add(color.b);
                m_FloatValue.Add(color.a);
            }
            // Enum
            else if (fieldType.IsEnum) {
                m_FieldType.Add(FieldType.Enum);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_IntValue.Count);
                m_IntValue.Add((int)value);
            }
            // LayerMask
            else if (fieldType == typeof(LayerMask)) {
                m_FieldType.Add(FieldType.LayerMask);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(m_IntValue.Count);
                m_IntValue.Add(((LayerMask)value).value);
            }
            // Not Supported
            else {
                m_FieldType.Add(FieldType.NotSupported);
                m_FieldPath.Add(fieldPath);
                m_FieldIndex.Add(0);
                Print.LogWarning("Could not serialize \'" + fieldPath + "\'. Type \'" + fieldType + "\' not supported.");
            }
        }

        /// <summary>
        /// Retrieves a node stored by this NodeSerialization.
        /// <param name="loadedNodes">List of nodes already loaded by this NodeSerialization.</param>
        /// <param name="nodeIndex">The node index to be loaded.</param>
        /// </summary>
        ActionNode Load (List<ActionNode> loadedNodes, int nodeIndex) {
            string typeName = m_NodeTypes[nodeIndex];
            Type type = TypeUtility.GetType (typeName);
            ActionNode node = null;

            // It's a valid type?
            if (type == null) {
                // Creates a MissingNode node
                type = typeof(MissingNode);
                node = ActionNode.CreateInstance(type, m_GameObject, m_NodeOwner);
                var missingNode = node as MissingNode;
                missingNode.missingNodeType = typeName;
                missingNode.OnTick();
            }
            else {
                node = ActionNode.CreateInstance(type, m_GameObject, m_NodeOwner);
            }

            // Retrieve the parent branch
            var branchIndex = m_BranchIndex[nodeIndex];
            if (branchIndex != -1) {
                // The branch is already been tracked?
                if (m_BranchChildren.ContainsKey(branchIndex))
                    m_BranchChildren[branchIndex].Add(node);
                else
                    m_BranchChildren[branchIndex] = new List<ActionNode>() {node};
            }

            m_NextFieldIndex = m_NodeFieldStartIndex[nodeIndex];
            // The type has changed?
            if (m_NodeTypeHash[nodeIndex] != TypeToHash(type)) {
                node.Reset();
                TryLoadFields(node, m_NodeFieldStartIndex[nodeIndex + 1] - m_NextFieldIndex, string.Empty);
            }
            else {
                // Load fields
                FieldInfo[] fields = NodeSerialization.GetSerializedFields(type);
                for (int i = 0; i < fields.Length; i++) {
                    // Get the field
                    FieldInfo field = fields[i];
                    // Load and set the field value
                    try {
                        field.SetValue(node, LoadNodeField(m_FieldType[m_NextFieldIndex], field.FieldType, field.Name));
                    }
                    catch (Exception e) {
                        Print.LogError("Error when loading object of type \'" + type.ToString() + "\'; in field \'" + field.Name + "\'\n" + e);
                    }
                    m_NextFieldIndex++;
                }
            }

            return node;
        }

        /// <summary>
        /// Load all fields of an object.
        /// <param name="obj">The target object.</param>
        /// <param name="fieldPath">The field path.</param>
        /// </summary>
        void LoadFields (object target, string fieldPath) {
            // Load target fields
            FieldInfo[] fields = NodeSerialization.GetSerializedFields(target.GetType());
            for (int i = 0; i < fields.Length; i++) {
                // Get the field
                FieldInfo field = fields[i];
                // Load and set the field value
                try {
                    field.SetValue(target, LoadNodeField(m_FieldType[m_NextFieldIndex], field.FieldType, fieldPath + "." + field.Name));
                }
                catch (Exception e) {
                    Print.LogError("Error when loading object of type \'" + target.GetType().ToString() + "\'; in field \'" + field.Name + "\'\n" + e);
                }
                m_NextFieldIndex++;
            }
        }

        /// <summary>
        /// Try to load the serialized fields of an object that the type has changed.
        /// <param name="obj">The target object.</param>
        /// <param name="fieldSize">The number of fields to be loaded.</param>
        /// <param name="currentFieldPath">The current field path.</param>
        /// </summary>
        void TryLoadFields (object target, int fieldSize, string currentFieldPath) {
            fieldSize += m_NextFieldIndex;

            // Load old field values
            var serializedFields = new Dictionary<string, SerializedField>();
            for (; m_NextFieldIndex < fieldSize; m_NextFieldIndex++) {
                var fieldPath = m_FieldPath[m_NextFieldIndex];
                var fieldType = m_FieldType[m_NextFieldIndex];

                try {
                    // Get value
                    var value = LoadNodeField(fieldType, null, fieldPath);
                    // Avoid argument exception
                    if (fieldType != FieldType.UnityObject || value != null)
                        serializedFields.Add(fieldPath, new SerializedField(value, fieldType));
                }
                catch (Exception e) {
                    Print.LogError("Error when loading object of type \'" + target.GetType().ToString() + "\'; in field path \'" + fieldPath + "\'\n" + e);
                }
            }

            // It is an ActionNode?
            if (target is ActionNode) {
                ((ActionNode)target).Reset();
            }
            // The current path is empty?
            bool emptyPath = currentFieldPath == string.Empty;

            // Set old values in the target object
            FieldInfo[] fields = NodeSerialization.GetSerializedFields(target.GetType());
            for (int i = 0; i < fields.Length; i++) {
                SerializedField serializedField;
                FieldInfo field = fields[i];

                // Try to get the field
                serializedFields.TryGetValue(emptyPath ? field.Name : currentFieldPath + "." + field.Name, out serializedField);
                
                if (serializedField != null && ((serializedField.value != null && (field.FieldType.IsAssignableFrom(serializedField.value.GetType()) || (field.FieldType.IsEnum && serializedField.fieldType == FieldType.Enum)))  || (serializedField.value == null && serializedField.HasType(field.FieldType)))) {
                    try {
                        field.SetValue(target, serializedField.value);
                    }
                    catch (Exception e) {
                        Print.LogError("Error when loading node of type: " + target.GetType().ToString() + "; in field " + field.Name + "\n" + e);
                    }
                }
            }

            // Resave nodes?
            if (Application.isEditor)
                m_ResaveNodes = true;
        }

        /// <summary>
        /// Loads a node field.
        /// <param name="fieldType">The target field type enum.</param>
        /// <param name="type">The real target field type.</param>
        /// <param name="fieldPath">The field path.</param>
        /// </summary>
        object LoadNodeField (FieldType fieldType, System.Type type, string fieldPath) {
            var fieldIndex = m_FieldIndex[m_NextFieldIndex];
            // value types
            switch (fieldType) {
                // It's a none var?
                case FieldType.None:
                    // Get generic type
                    var genericType = TypeUtility.GetType(m_StringValue[fieldIndex]);
                    
                    if (genericType == null) {
                        // Try to get the field type
                        if (type != null)
                            genericType = type.IsAbstract ? TypeUtility.GetConcreteType(type) : type;
                    }
                    else if (genericType.IsAbstract) {
                        // Try to get the concrete type
                        genericType = TypeUtility.GetConcreteType(genericType) ?? TypeUtility.GetConcreteType(type);
                    }
                    else if (genericType.IsInterface) {
                        Print.LogError("Can't serialize abstract or interface fields: " + genericType.ToString());
                        return null;
                    }

                    // Create the generic object
                    if (typeof(GameObjectVar).IsAssignableFrom(genericType))
                        return Activator.CreateInstance(genericType, new object[] {m_GameObject});
                    else
                        return Activator.CreateInstance(genericType);
                // It's a constant var?
                case FieldType.Constant:
                    // Get generic type
                    genericType = TypeUtility.GetType(m_GenericFieldType[fieldIndex]);

                    if (genericType == null) {
                        // Try to get the field type
                        if (type != null)
                            genericType = type.IsAbstract ? TypeUtility.GetConcreteType(type) : type;
                    }
                    else if (genericType.IsAbstract) {
                        // Try to get the concrete type
                        genericType = TypeUtility.GetConcreteType(genericType) ?? TypeUtility.GetConcreteType(type);
                    }
                    else if (genericType.IsInterface) {
                        Print.LogError("Can't serialize interface fields: " + genericType.ToString());
                        return null;
                    }

                    // Create the generic object
                    var variable = Activator.CreateInstance(genericType) as BehaviourMachine.Variable;

                    if (variable == null)
                        return null;

                    // The type has changed?
                    if (m_GenericFieldHash[fieldIndex] != TypeToHash(genericType)) {
                        var fieldSize = m_GenericFieldSize[fieldIndex];
                        m_NextFieldIndex++;
                        TryLoadFields(variable, fieldSize, fieldPath);
                    }
                    else {
                        m_NextFieldIndex++;
                        LoadFields(variable, fieldPath);
                    }

                    variable.SetAsConstant();
                    m_NextFieldIndex--;

                    return variable;
                // FloatVar
                case FieldType.FloatVar:
                    // Get variable id
                    var varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetFloatVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetFloatVar(varId);
                    else
                        return new ConcreteFloatVar();
                // IntVar
                case FieldType.IntVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetIntVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetIntVar(varId);
                    else
                        return new ConcreteIntVar();
                // BoolVar
                case FieldType.BoolVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetBoolVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetBoolVar(varId);
                    else
                        return new ConcreteBoolVar();
                // StringVar
                case FieldType.StringVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetStringVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetStringVar(varId);
                    else
                        return new ConcreteStringVar();
                // Vector3Var
                case FieldType.Vector3Var:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetVector3Var(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetVector3Var(varId);
                    else
                        return new ConcreteVector3Var();
                // RectVar
                case FieldType.RectVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetRectVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetRectVar(varId);
                    else
                        return new ConcreteRectVar();
                // ColorVar
                case FieldType.ColorVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetColorVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetColorVar(varId);
                    else
                        return new ConcreteColorVar();
                // QuaternionVar
                case FieldType.QuaternionVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetQuaternionVar(varId); 
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetQuaternionVar(varId);
                    else
                        return new ConcreteQuaternionVar();
                // GameObjectVar
                case FieldType.GameObjectVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetGameObjectVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetGameObjectVar(varId);
                    else
                        return new ConcreteGameObjectVar(m_GameObject);
                // TextureVar
                case FieldType.TextureVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetTextureVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetTextureVar(varId);
                    else
                        return new ConcreteTextureVar();
                // MaterialVar
                case FieldType.MaterialVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetMaterialVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetMaterialVar(varId);
                    else
                        return new ConcreteMaterialVar();
                // ObjectVar
                case FieldType.ObjectVar:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetObjectVar(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetObjectVar(varId);
                    else
                        return new ConcreteObjectVar();
                // DynamicList
                case FieldType.DynamicList:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetDynamicList(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetDynamicList(varId);
                    else
                        return new ConcreteDynamicList();
                // FsmEvent
                case FieldType.FsmEvent:
                    // Get variable id
                    varId = m_IntValue[fieldIndex];
                    // Get variable
                    if (varId > 0)
                        return m_Blackboard.GetFsmEvent(varId);
                    else if (InternalGlobalBlackboard.Instance != null)
                        return InternalGlobalBlackboard.Instance.GetFsmEvent(varId);
                    else
                        return new ConcreteFsmEvent();
                // string
                case FieldType.String:
                    return m_StringValue[fieldIndex];
                // int
                case FieldType.Int:
                    return m_IntValue[fieldIndex];
                // float
                case FieldType.Float:
                    return m_FloatValue[fieldIndex];
                // bool
                case FieldType.Bool:
                    return m_IntValue[fieldIndex] == 1 ? true : false;
                // Vector2
                case FieldType.Vector2:
                    return new Vector2(m_FloatValue[fieldIndex], m_FloatValue[fieldIndex + 1]);
                // Vector3
                case FieldType.Vector3:
                    return new Vector3(m_FloatValue[fieldIndex], m_FloatValue[fieldIndex + 1], m_FloatValue[fieldIndex + 2]);
                // Vector4
                case FieldType.Vector4:
                    return new Vector4 (m_FloatValue[fieldIndex], m_FloatValue[fieldIndex + 1], m_FloatValue[fieldIndex + 2], m_FloatValue[fieldIndex + 3]);
                // Quaternion
                case FieldType.Quaternion:
                    return new Quaternion (m_FloatValue[fieldIndex], m_FloatValue[fieldIndex + 1], m_FloatValue[fieldIndex + 2], m_FloatValue[fieldIndex + 3]);
                // Rect
                case FieldType.Rect:
                    return new Rect (m_FloatValue[fieldIndex], m_FloatValue[fieldIndex + 1], m_FloatValue[fieldIndex + 2], m_FloatValue[fieldIndex + 3]);
                // Color
                case FieldType.Color:
                    return new Color (m_FloatValue[fieldIndex], m_FloatValue[fieldIndex + 1], m_FloatValue[fieldIndex + 2], m_FloatValue[fieldIndex + 3]);
                // Enum
                case FieldType.Enum:
                    return m_IntValue[fieldIndex];
                // LayerMask
                case FieldType.LayerMask:
                    LayerMask layerMask = m_IntValue[fieldIndex];
                    return layerMask;
                // InternalStateBehaviour
                case FieldType.State:
                    var state = m_UnityObjectValue[fieldIndex] as InternalStateBehaviour;
                    var tree = m_NodeOwner as InternalBehaviourTree;
                    if (state != null && tree != null && state != tree && state.parent != tree)
                        state = null;
                    return state;
                // UnityEngine.Object
                case FieldType.UnityObject:
                    if (m_UnityObjectValue[fieldIndex] == null && type != null)
                        return System.Convert.ChangeType(null, type);
                    else
                        return m_UnityObjectValue[fieldIndex];
                // Array
                case FieldType.Array:
                    // Get element type
                    Type elementType = TypeUtility.GetType(m_GenericFieldType[fieldIndex]);
                    if (elementType == null) {
                        Print.LogError("Could not find System.Type \'" + m_GenericFieldType[fieldIndex] + "\' (Array Element)");
                        return null;
                    }

                    var size = m_GenericFieldSize[fieldIndex];// m_FieldSize[m_NextFieldIndex];               // Get array size
                    var array = Array.CreateInstance(elementType, size);    // Create array

                    // TODO: convert array element if m_ArrayElementType[m_NextFieldIndex] != elementType

                    // Load array elements
                    for (int i = 0; i < size; i++) {
                        m_NextFieldIndex++;
                        array.SetValue(LoadNodeField(m_FieldType[m_NextFieldIndex], elementType, fieldPath + ".data[" + i.ToString() + "]" ), i);
                    }

                    // Set value
                    return array;
                case FieldType.AnimationCurve:
                    return m_CurveValue[fieldIndex].curve;
                default:
                    Print.LogError("Type not supported: " + fieldType.ToString());
                    break;
            }

            return null;
        }
        #endregion Private Methods

        
        #region Public Methods
        /// <summary>
        /// Call this to store the nodes.
        /// <param name="nodes">The list of nodes to be saved.</param>
        /// <param name="gameObject">The target game object.</param>
        /// <param name="nodeOwner">The node owner.</param>
        /// </summary>
        public void SaveNodes (ActionNode[] nodes, GameObject gameObject, INodeOwner nodeOwner) {
            m_ResaveNodes = false;

            // Validate parameters
            if (nodes == null || gameObject == null || nodeOwner == null)
                return;

            // Clear data
            Clear();

            // Update game object, tree and blackboard
            m_GameObject = gameObject;
            m_NodeOwner = nodeOwner;
            m_Blackboard = gameObject.GetComponent<InternalBlackboard>();

            // Save nodes
            for (int i = 0; i < nodes.Length; i++)
                Save (nodes[i]);

            m_NodeFieldStartIndex.Add(m_NextFieldIndex);
        }

        /// <summary>
        /// Call this to load nodes.
        /// <param name="gameObject">The target game object.</param>
        /// <param name="nodeOwner">The node owner.</param>
        /// </summary>
        public ActionNode[] LoadNodes (GameObject gameObject, INodeOwner nodeOwner) {

            var nodes = new List<ActionNode>();
            m_BranchChildren = new Dictionary<int, List<ActionNode>>();

            // Validate parameters
            if (gameObject == null || nodeOwner == null )
                return nodes.ToArray();

            m_GameObject = gameObject;
            m_NodeOwner = nodeOwner;
            m_Blackboard = gameObject.GetComponent<InternalBlackboard>();

            // Goes through all node types and creates an instance
            for (int i = 0; i < m_NodeTypes.Count; i++) {
                ActionNode node = Load(nodes, i);

                // It's a valid node?
                if (node != null)
                    nodes.Add(node);
            }

            // Add children to branch
            foreach (var pair in m_BranchChildren) {
                var branch = nodes[pair.Key] as BranchNode;

                if (branch == null || !branch.SetChildren(m_BranchChildren[pair.Key].ToArray()))
                    nodeOwner.HierarchyChanged();
            }

            m_BranchChildren = null;

            return nodes.ToArray();
        }
        #endregion Public Methods
    }
}