//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Reflection;

namespace BehaviourMachine {

    /// <summary>
    /// Base class for nodes that perform operations on properties or fields.
    /// <seealso cref="BehaviourMachine.SetProperty" />
    /// <seealso cref="BehaviourMachine.GetProperty" />
    /// </summary>
    public abstract class PropertyOrField : ActionNode {

        [VariableInfo(tooltip = "The target object")]
        public ObjectVar targetObject;

        [UnityObjectProperty]
        [Tooltip("The name of the property")]
        public string propertyName = string.Empty;

        public override void Reset () {
            targetObject = (UnityEngine.Object)null;
            propertyName = string.Empty;
        }


        #region Property Field
        protected MemberInfo m_MemberInfo;
        [System.NonSerialized]
        protected PropertyInfo m_PropertyInfo;
        [System.NonSerialized]
        protected FieldInfo m_FieldInfo;
        [System.NonSerialized]
        protected Object m_Target;
        [System.NonSerialized]
        protected System.Type m_CachedTargetType;
        [System.NonSerialized]
        protected string m_CachedPropertyName = string.Empty;
        [System.NonSerialized]
        protected System.Type m_CachedPropertyType;
        [System.NonSerialized]
        protected bool m_CanWrite = false;
        [System.NonSerialized]
        protected bool m_CanRead = false;

        /// <summary>
        /// Returns the "Property Name" type.
        /// </summary>
        public System.Type propertyType {
            get {
                if (targetObject.isNone || string.IsNullOrEmpty(propertyName))
                    return null;
                if (m_MemberInfo == null || propertyName != m_CachedPropertyName || targetObject.ObjectType != m_CachedTargetType)
                    UpdateMemberInfo();
                return m_CachedPropertyType;
            }
        }

        /// <summary>
        /// Cache member info.
        /// </summary>
        protected bool UpdateMemberInfo () {
            ClearCached();
            var type = targetObject.ObjectType;
            m_PropertyInfo = type.GetProperty(propertyName);
            if (m_PropertyInfo == null) {
                m_FieldInfo = type.GetField(propertyName);
                m_MemberInfo = m_FieldInfo;
            }
            else
                m_MemberInfo = m_PropertyInfo;

            if (m_MemberInfo != null) {
                // Cache values
                m_CachedTargetType = type;
                m_CachedPropertyName = propertyName;
                // Get property type
                if (m_FieldInfo != null) {
                    m_CachedPropertyType = m_FieldInfo.FieldType;
                    m_CanWrite = true;
                    m_CanRead = true;
                }
                else if (m_PropertyInfo != null) {
                    m_CachedPropertyType = m_PropertyInfo.PropertyType;
                    m_CanWrite = m_PropertyInfo.CanWrite;
                    m_CanRead = m_PropertyInfo.CanRead;
                }
                // Not supported
                else {
                    Print.LogError("MemberInfo not supported for: " + propertyName);
                    ClearCached();
                    return false;
                }
                return true;
            }
            else
                ClearCached();
            return false;
        }

        /// <summary>
        /// Cache cached data.
        /// </summary>
        void ClearCached () {
            m_MemberInfo = null;
            m_PropertyInfo = null;
            m_Target = null;
            m_FieldInfo = null;
            m_CachedTargetType = null;
            m_CachedPropertyName = string.Empty;
            m_CachedPropertyType = null;
            m_CanWrite = false;
            m_CanRead = false;
        }
        #endregion Property Field
    }
}
