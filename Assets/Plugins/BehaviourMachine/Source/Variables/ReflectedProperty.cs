//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Reflection;

namespace BehaviourMachine {

    /// <summary>
    /// Access any plublic (static or instance) float property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class FloatProperty : FloatVar {

        [VariableInfo(tooltip = "The target object")]
    	public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(float))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override float Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (float) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (float) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return 0f;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(FloatProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public FloatProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public FloatProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) int property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class IntProperty : IntVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(int))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override int Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (int) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (int) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return 0;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(IntProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public IntProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public IntProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) bool property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class BoolProperty : BoolVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(bool))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override bool Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (bool) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (bool) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return false;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(BoolProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public BoolProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public BoolProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) string property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class StringProperty : StringVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(string))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override string Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (string) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (string) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return string.Empty;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(StringProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public StringProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public StringProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) Vector3 property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class Vector3Property : Vector3Var {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(Vector3))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Vector3 Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (Vector3) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (Vector3) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return Vector3.zero;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(Vector3Property) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public Vector3Property () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public Vector3Property (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) Rect property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class RectProperty : RectVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(Rect))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Rect Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (Rect) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (Rect) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return new Rect(0,0,0,0);
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(RectProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public RectProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public RectProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) Color property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class ColorProperty : ColorVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(Color))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Color Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (Color) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (Color) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return Color.white;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(ColorProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ColorProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public ColorProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) Quaternion property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class QuaternionProperty : QuaternionVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(Quaternion))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Quaternion Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (Quaternion) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (Quaternion) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return Quaternion.identity;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(QuaternionProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public QuaternionProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public QuaternionProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) GameObject property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class GameObjectProperty : GameObjectVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(GameObject))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override GameObject Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (GameObject) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (GameObject) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return null;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(GameObjectProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// The game object transform.
        /// </summary>
        public override Transform transform {
            get {
                GameObject gameObject = this.Value;
                return gameObject != null ? gameObject.transform : null;
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public GameObjectProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public GameObjectProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) Texture property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class TextureProperty : TextureVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(Texture))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Texture Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (Texture) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (Texture) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return null;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(TextureProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public TextureProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public TextureProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) Material property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class MaterialProperty : MaterialVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(Material))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Material Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (Material) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (Material) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return null;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(MaterialProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public MaterialProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public MaterialProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }

    /// <summary>
    /// Access any plublic (static or instance) UnityEngine.Object property or field through reflection.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Reflection_10")]
    public class ObjectProperty : ObjectVar {

        [VariableInfo(tooltip = "The target object")]
        public UnityEngine.Object target;

        [HideInInspector]
        public Component component;

        [HideInInspector]
        public bool isStatic;

        [ReflectedProperty("target", "component", "isStatic", typeof(Object))]
        [Tooltip("The name of the property")]
        public string property = string.Empty;


        #region Private Members
        [System.NonSerialized]
        MemberInfo m_MemberInfo;
        [System.NonSerialized]
        PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        FieldInfo m_FieldInfo;
        [System.NonSerialized]
        object m_Target;
        #endregion Private Members


        /// <summary>
        /// Init the private data.
        /// </summary>
        void Initialize () {
            if (target != null) {
                System.Type targetType = null;
                if (component == null) {
                    targetType = target.GetType();
                    m_Target = isStatic ? null : target;
                }
                else {
                    targetType = component.GetType();
                    m_Target = isStatic ? null : component;
                }

                // Try to get the property info
                m_PropertyInfo = targetType.GetProperty(property);

                if (m_PropertyInfo == null) { 
                    // Try to get the field info
                    m_FieldInfo = targetType.GetField(property);
                    m_MemberInfo = m_FieldInfo;
                }
                else
                    m_MemberInfo = m_PropertyInfo;
            }
        }

        /// <summary>
        /// Variable value.
        /// </summary>
        public override UnityEngine.Object Value {
            get {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Get value
                if (m_PropertyInfo != null) {
                    return (UnityEngine.Object) m_PropertyInfo.GetValue(m_Target, null);
                }
                else if (m_FieldInfo != null) {
                    return (UnityEngine.Object) m_FieldInfo.GetValue(m_Target);
                }
                else {
                    if (component == null)
                        Print.LogError("No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                    return null;
                }
            }

            set {
                // Needs to load the member info?
                if (m_MemberInfo == null)
                    Initialize();

                // Set value
                if (m_PropertyInfo != null) {
                    m_PropertyInfo.SetValue(m_Target, value, null);
                }
                else if (m_FieldInfo != null) {
                    m_FieldInfo.SetValue(m_Target, value);
                }
                else {
                    if (component == null)
                        Print.LogError("(ObjectProperty) No property with name \'" + property +  "\' in the object \'" + target + "\'.");
                    else
                        Print.LogError("No property with name \'" + property +  "\' in the component \'" + target + "." + component + "\'.");
                }
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public ObjectProperty () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public ObjectProperty (GameObject self) : base () {
            this.target = self;
        }

        /// <summary>
        /// This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        /// Validate members.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (target == null || (component != null && component.gameObject != target)) {
                component = null;
                isStatic = false;
                property = string.Empty;
            }
        }  
    }
}
