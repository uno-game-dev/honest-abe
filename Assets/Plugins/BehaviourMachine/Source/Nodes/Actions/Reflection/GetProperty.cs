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
                description = "Gets the \"Property Name\" of \"Target Object\". Returns Error if \"Target Object\" is null")]
    public class GetProperty : PropertyOrField {

        [VariableInfo(canBeConstant = false)]
        public FloatVar SingleValue;
        [VariableInfo(canBeConstant = false)]
        public IntVar intValue;
        [VariableInfo(canBeConstant = false)]
        public BoolVar BooleanValue;
        [VariableInfo(canBeConstant = false)]
        public StringVar StringValue;
        [VariableInfo(canBeConstant = false)]
        public Vector3Var Vector3Value;
        [VariableInfo(canBeConstant = false)]
        public RectVar RectValue;
        [VariableInfo(canBeConstant = false)]
        public ColorVar ColorValue;
        [VariableInfo(canBeConstant = false)]
        public GameObjectVar GameObjectValue;
        [VariableInfo(canBeConstant = false)]
        public MaterialVar MaterialValue;
        [VariableInfo(canBeConstant = false)]
        public TextureVar TextureValue;
        [VariableInfo(canBeConstant = false)]
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
                if (!UpdateMemberInfo())
                    return Status.Error;
            }

            // Can write in property?
            if (!m_CanRead)
                return Status.Error;

            // Float
            if (m_CachedPropertyType == typeof(float) && !SingleValue.isNone) {
                if (m_PropertyInfo != null)
                    SingleValue.Value = (float)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    SingleValue.Value = (float)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // Int
            else if (m_CachedPropertyType == typeof(int) && !intValue.isNone) {
                if (m_PropertyInfo != null)
                    intValue.Value = (int)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    intValue.Value = (int)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // Bool
            else if (m_CachedPropertyType == typeof(bool) && !BooleanValue.isNone) {
                if (m_PropertyInfo != null)
                    BooleanValue.Value = (bool)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    BooleanValue.Value = (bool)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // String
            else if (m_CachedPropertyType == typeof(string) && !StringValue.isNone) {
                if (m_PropertyInfo != null)
                    StringValue.Value = (string)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    StringValue.Value = (string)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // Vector3
            else if (m_CachedPropertyType == typeof(Vector3) && !Vector3Value.isNone) {
                if (m_PropertyInfo != null)
                    Vector3Value.Value = (Vector3)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    Vector3Value.Value = (Vector3)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // Rect
            else if (m_CachedPropertyType == typeof(Rect) && !RectValue.isNone) {
                if (m_PropertyInfo != null)
                    RectValue.Value = (Rect)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    RectValue.Value = (Rect)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // Color
            else if (m_CachedPropertyType == typeof(Color) && !ColorValue.isNone) {
                if (m_PropertyInfo != null)
                    ColorValue.Value = (Color)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    ColorValue.Value = (Color)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // GameObject
            else if (m_CachedPropertyType == typeof(GameObject) && !GameObjectValue.isNone) {
                if (m_PropertyInfo != null)
                    GameObjectValue.Value = (GameObject)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    GameObjectValue.Value = (GameObject)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // Material
            else if (m_CachedPropertyType == typeof(Material) && !MaterialValue.isNone) {
                if (m_PropertyInfo != null)
                    MaterialValue.Value = (Material)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    MaterialValue.Value = (Material)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // Texture
            else if (m_CachedPropertyType == typeof(Texture) && !TextureValue.isNone) {
                if (m_PropertyInfo != null)
                    TextureValue.Value = (Texture)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    TextureValue.Value = (Texture)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // UnityObject
            else if (typeof(Object).IsAssignableFrom(m_CachedPropertyType) && !ObjectValue.isNone) {
                if (m_PropertyInfo != null)
                    ObjectValue.Value = (Object)m_PropertyInfo.GetValue(targetObject.Value, null);
                else
                    ObjectValue.Value = (Object)m_FieldInfo.GetValue(targetObject.Value);
                return Status.Success;
            }
            // Enum
            else if (m_CachedPropertyType.IsEnum && !StringValue.isNone) {
                System.Enum enumValue;
                if (m_PropertyInfo != null) {
                    enumValue = m_PropertyInfo.GetValue(targetObject.Value, null) as System.Enum;
                    StringValue.Value = enumValue != null ? enumValue.ToString() : string.Empty;
                }
                else {
                    enumValue = m_FieldInfo.GetValue(targetObject.Value) as System.Enum;
                    StringValue.Value = enumValue != null ? enumValue.ToString() : string.Empty;
                }
                return Status.Success;
            }
            
            return Status.Error;
        }
	}
}
