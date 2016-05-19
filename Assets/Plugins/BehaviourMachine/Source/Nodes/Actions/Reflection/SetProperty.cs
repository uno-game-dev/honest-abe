//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Reflection;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Property Name" of "Target Object".
    /// Only float, int, bool, string, Vector3, Rect, Color, GameObject and Object types are supported.
    /// Returns Error if "Target Object" is null.
    /// </summary>
    [NodeInfo(  category = "Action/Reflection/",
                icon = "Reflection",
                description = "Set's the \"Property Name\" of \"Target Object\". Returns Error if \"Target Object\" is null")]
    public class SetProperty : PropertyOrField {

        public IntVar intValue;
        public FloatVar SingleValue;
        public BoolVar BooleanValue;
        public StringVar StringValue;
        public Vector3Var Vector3Value;
        public RectVar RectValue;
        public ColorVar ColorValue;
        public GameObjectVar GameObjectValue;
        public MaterialVar MaterialValue;
        public TextureVar TextureValue;
        public ObjectVar ObjectValue;

        public override void Reset () {
            base.Reset();
            SingleValue = new ConcreteFloatVar();
            intValue = new ConcreteIntVar();
            BooleanValue = new ConcreteBoolVar();
            StringValue = new ConcreteStringVar();
            Vector3Value = new ConcreteVector3Var();
            RectValue = new ConcreteRectVar();
            ColorValue = new ConcreteColorVar();
            GameObjectValue = new ConcreteGameObjectVar(this.self);
            MaterialValue = new ConcreteMaterialVar();
            TextureValue = new ConcreteTextureVar();
            ObjectValue = new ConcreteObjectVar();
        }

        /// <summary>
        /// Behaviour Machine callback called when a value is changed in the inspector (Called in the editor only).
        /// </summary>
        public override void OnValidate () {
            // The property is a unity object?
            var propertyType = this.propertyType;
            if (propertyType != null && propertyType.IsSubclassOf(typeof(Object))) {
                if (ObjectValue.isConstant) {
                    ObjectValue.ObjectType = propertyType;
                    ObjectValue.OnValidate();
                }
                else {
                    var objectType = ObjectValue.ObjectType;
                    if (objectType != propertyType && !objectType.IsSubclassOf(propertyType)) {
                        ObjectValue = new ConcreteObjectVar();
                    }
                }
            }
        }

        public override Status Update () {
            // Has valid parameters?
            if (targetObject.isNone || targetObject.Value == null || string.IsNullOrEmpty(propertyName))
                return Status.Error;

            // Needs to update MemberInfo?
            if (m_MemberInfo == null || m_CachedTargetType != targetObject.Value.GetType() || m_CachedPropertyName != propertyName) {
                if (!UpdateMemberInfo()) {
                    return Status.Error;
                }
            }

            // Can write in property?
            if (!m_CanWrite) {
                return Status.Error;
            }

            // Float
            if (m_CachedPropertyType == typeof(float) && !SingleValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, SingleValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, SingleValue.Value);
                return Status.Success;
            }
            // Int
            else if (m_CachedPropertyType == typeof(int) && !intValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, intValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, intValue.Value);
                return Status.Success;
            }
            // Bool
            else if (m_CachedPropertyType == typeof(bool) && !BooleanValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, BooleanValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, BooleanValue.Value);
                return Status.Success;
            }
            // String
            else if (m_CachedPropertyType == typeof(string) && !StringValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, StringValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, StringValue.Value);
                return Status.Success;
            }
            // Vector3
            else if (m_CachedPropertyType == typeof(Vector3) && !Vector3Value.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, Vector3Value.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, Vector3Value.Value);
                return Status.Success;
            }
            // Rect
            else if (m_CachedPropertyType == typeof(Rect) && !RectValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, RectValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, RectValue.Value);
                return Status.Success;
            }
            // Color
            else if (m_CachedPropertyType == typeof(Color) && !ColorValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, ColorValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, ColorValue.Value);
                return Status.Success;
            }
            // GameObject
            else if (m_CachedPropertyType == typeof(GameObject) && !GameObjectValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, GameObjectValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, GameObjectValue.Value);
                return Status.Success;
            }
            // Material
            else if (m_CachedPropertyType == typeof(Material) && !MaterialValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, MaterialValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, MaterialValue.Value);
                return Status.Success;
            }
            // Texture
            else if (m_CachedPropertyType == typeof(Texture) && !TextureValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, TextureValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, TextureValue.Value);
                return Status.Success;
            }
            // UnityObject
            else if (typeof(Object).IsAssignableFrom(m_CachedPropertyType) && !ObjectValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, ObjectValue.Value, null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, ObjectValue.Value);
                return Status.Success;
            }
            // Enum
            else if (m_CachedPropertyType.IsEnum && !StringValue.isNone) {
                if (m_PropertyInfo != null)
                    m_PropertyInfo.SetValue(targetObject.Value, System.Enum.Parse (m_CachedPropertyType, StringValue.Value), null);
                else
                    m_FieldInfo.SetValue(targetObject.Value, System.Enum.Parse (m_CachedPropertyType, StringValue.Value));
                return Status.Success;
            }

            return Status.Error;
        }
	}
}
