//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System;
using System.Collections;

namespace BehaviourMachine {

    #region Variable Attributes
    /// <summary>
    /// An attribute that stores the tooltip of a field; if it's a Variable field then use VariableInfoAttribute.tooltip.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TooltipAttribute : Attribute {
        /// <summary>
        /// The tooltip of the field.
        /// </summary>
        public readonly string tooltip = "";

        /// <summary>
        /// TooltipAttribute constructor.
        /// <param name="tooltip">The toolip value.</param>
        /// </summary>
        public TooltipAttribute (string tooltip) {
            this.tooltip = tooltip;
        }
    }

    /// <summary>
    /// Meta data information about the variable field.
    /// <seealso cref="BehaviourMachine.Variable" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class VariableInfoAttribute : Attribute {
        /// <summary>
        /// True if the variable can accept a none value.
        /// </summary>
        public bool requiredField = true;

        /// <summary>
        /// True if the variable can be constant.
        /// </summary>
        public bool canBeConstant = true;

        /// <summary>
        /// The label to show in the pop-up menu when the field has a None value.
        /// </summary>
        public string nullLabel = "None";

        /// <summary>
        /// The variable tooltip.
        /// </summary>
        public string tooltip = string.Empty;

        /// <summary>
        /// If True then the type of the variable can be changed to any other (ony used by Variable fields).
        /// </summary>
        public bool fixedType = false;
    }

    /// <summary>
    /// An attribute used by abstract variables classes to store the type of its concrete class.
    /// <seealso cref="BehaviourMachine.Variable" />
    /// </summary>
    [AttributeUsage (AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ConcreteClassAttribute : Attribute {
        
        Type m_ConcreteType;

        /// <summary>
        /// The concrete class type.
        /// </summary>
        public Type concreteType {get {return m_ConcreteType;}}

        /// <summary>
        /// Class constructor.
        /// <param name="type">The type of the concrete class.</param>
        /// </summary>
        public ConcreteClassAttribute (Type type) {
            m_ConcreteType = type;
        }
    }

    /// <summary>
    /// An attribute used to identify custom variables in the editor.
    /// <seealso cref="BehaviourMachine.Variable" />
    /// </summary>
    [AttributeUsage (AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CustomVariableAttribute : Attribute {

        string m_Icon;

        /// <summary>
        /// The custom variable icon.
        /// </summary>
        public string icon {get {return m_Icon;}}

        /// <summary>
        /// Class constructor.
        /// <param name="icon">The variable icon.</param>
        /// </summary>
        public CustomVariableAttribute (string icon) {
            this.m_Icon = icon;
        }
    }
    #endregion Variable Attributes

    
    #region Node Attributes
    /// <summary>
    /// An attribute used by ObjectValueDrawer to draw the object using another string property.
    /// <seealso cref="BehaviourMachineEditor.ObjectValueDrawer" />
    /// </summary>
    public class ObjectValueAttribute : PropertyAttribute {
        string m_TypePropertyPath;

        /// <summary>
        /// The path of the string property that stores the object type.
        /// </summary>
        public string typePropertyPath {get {return m_TypePropertyPath;}}

        /// <summary>
        /// Class constructor.
        /// <param name = "typePropertyPath">The path of the property that stores the object type.</param>
        /// </summary>
        public ObjectValueAttribute (string typePropertyPath) {
            this.m_TypePropertyPath = typePropertyPath;
        }
    }

    /// <summary>
    /// An attribute used by UnityObjectTypeDrawer to draw a string as a UnityEngine.Object type.
    /// <seealso cref="BehaviourMachineEditor.UnityObjectTypeDrawer" />
    /// </summary>
    public class UnityObjectTypeAttribute : PropertyAttribute {
        /// <summary>
        /// Receive a type as string and returns a System.Type.
        /// <param name="objectType">The type as string (System.Type.ToString()).</param>
        /// <returns>Returns the Type of the supplied string.</returns>
        /// </summary>
        public static Type GetObjectType (string objectType) {
            return !string.IsNullOrEmpty(objectType) ? (TypeUtility.GetType(objectType) ?? typeof(UnityEngine.Object)) : typeof(UnityEngine.Object);
        }
    }

    /// <summary>
    /// An attribute used by NodeTextAreaDrawer to draw a text area for a string or StringVar.
    /// <seealso cref="NodeTextAreaDrawer" />
    /// </summary>
    public class NodeTextAreaAttribute : PropertyAttribute {
        string m_Hint;
        int m_Lines;

        /// <summary>
        /// The hint message.
        /// </summary>
        public string hint {get {return m_Hint;}}

        /// <summary>
        /// The number of lines, three by default.
        /// </summary>
        public int lines {get {return m_Lines;}}

        /// <summary>
        /// Class constructor.
        /// <param name="hint">The hint used by the property drawer.</param>
        /// <param name="lines">The number of lines.</param>
        /// </summary>
        public NodeTextAreaAttribute (string hint, int lines) {
            m_Hint = hint;
            m_Lines = lines > 0 ? lines : 1;
        }
    }

    /// <summary>
    /// An attribute used by TreeStateDrawer to draw a tree's InternalStateBehaviour property, only InternalStateBehaviours in the same game object as the node tree can be assigned to the property with this attribute.
    /// <seealso cref="TreeStateDrawer" />
    /// </summary>
    public class TreeStateAttribute : PropertyAttribute {}

    /// <summary>
    /// An attribute used by UnityObjectPropertyDrawer to draw a unity object property.
    /// <seealso cref="BehaviourMachineEditor.UnityObjectPropertyDrawer" />
    /// <seealso cref="BehaviourMachine.SetProperty" />
    /// <seealso cref="BehaviourMachine.GetProperty" />
    /// <seealso cref="BehaviourMachine.PropertyOrField" />
    /// </summary>
    public class UnityObjectPropertyAttribute : PropertyAttribute {}

    /// <summary>
    /// An attribute used by ReflectedPropertyDrawer to draw a unity object property.
    /// <seealso cref="BehaviourMachineEditor.ReflectedPropertyDrawer" />
    /// </summary>
    public class ReflectedPropertyAttribute : PropertyAttribute {
        
        string m_ObjectPropertyPath;
        string m_ComponentPropertyPath;
        string m_IsStaticPropertyPath;
        Type m_PropertyType;

        /// <summary>
        /// The path of the Unity object property.
        /// </summary>
        public string objectPropertyPath {get {return m_ObjectPropertyPath;}}

        /// <summary>
        /// The path of the component property.
        /// </summary>
        public string componentPropertyPath {get {return m_ComponentPropertyPath;}}

        /// <summary>
        /// The path of the component property.
        /// </summary>
        public string isStaticPropertyPath {get {return m_IsStaticPropertyPath;}}

        /// <summary>
        /// The type of the property.
        /// </summary>
        public Type propertyType {get {return m_PropertyType;}}

        /// <summary>
        /// Class constructor.
        /// <param name = "objectPropertyPath">The path of the Unity object property.</param>
        /// <param name = "componentPropertyPath">The path of the component property.</param>
        /// <param name = "isStaticPropertyPath">The path of the isStatic property.</param>
        /// <param name = "propertyType">The type of the property.</param>
        /// </summary>
        public ReflectedPropertyAttribute (string objectPropertyPath, string componentPropertyPath, string isStaticPropertyPath, Type propertyType) {
            this.m_ObjectPropertyPath = objectPropertyPath;
            this.m_ComponentPropertyPath = componentPropertyPath;
            this.m_IsStaticPropertyPath = isStaticPropertyPath;
            this.m_PropertyType = propertyType;
        }
    }

    /// <summary>
    /// An attribute that stores node's meta data information.
    /// <seealso cref="BehaviourMachine.ActionNode" />
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NodeInfoAttribute : Attribute {
        /// <summary>
        /// The node category.
        /// </summary>
        public string category = string.Empty;

        /// <summary>
        /// The icon name in Resources folder.
        /// </summary>
        public string icon = "DefaultAsset";

        /// <summary>
        /// The url for the node help.
        /// </summary>
        public string url = "http://www.behaviourmachine.com";

        /// <summary>
        /// The description of the node.
        /// </summary>
        public string description = string.Empty;
    }
    #endregion


    #region Unity Attributes
    /// <summary>
    /// An attribute used by ParentPropertyDrawer to draw the m_Parent field of a state.
    /// <seealso cref="ParentPropertyDrawer" />
    /// </summary>
    public class ParentPropertyAttribute : PropertyAttribute {}

    /// <summary>
    /// An attribute used by MonoBehaviourDrawer to draw the m_Target and m_MonoBehaviour field of a MonoState.
    /// <seealso cref="MonoBehaviourDrawer" />
    /// </summary>
    public class MonoBehaviourAttribute : PropertyAttribute {}

    /// <summary>
    /// An attribute used by HideScriptDrawer to draw the m_HideScrip field of a state.
    /// <seealso cref="HideScriptDrawer" />
    /// </summary>
    public class HideScriptAttribute : PropertyAttribute {}

    /// <summary>
    /// An attribute used by StateColorDrawer to draw the m_Color field of a state.
    /// <seealso cref="StateColorDrawer" />
    /// </summary>
    public class StateColorAttribute : PropertyAttribute {}

    /// <summary>
    /// An attribute used by SelectableLabelDrawer to draw a string, float, int or Object field as a selectable label.
    /// <seealso cref="SelectableLabelDrawer" />
    /// </summary>
    public class SelectableLabelAttribute : PropertyAttribute {}

    /// <summary>
    /// An attribute used by TextAreaDrawer to draw a text area for a string.
    /// <seealso cref="TextAreaDrawer" />
    /// </summary>
    public class TextAreaAttribute : PropertyAttribute {
        string m_Hint;
        int m_Lines;

        /// <summary>
        /// The hint message.
        /// </summary>
        public string hint {get {return m_Hint;}}

        /// <summary>
        /// The number of lines, three by default.
        /// </summary>
        public int lines {get {return m_Lines;}}

        /// <summary>
        /// Class constructor.
        /// <param name="hint">The hint used by the property drawer.</param>
        /// <param name="lines">The number of lines.</param>
        /// </summary>
        public TextAreaAttribute (string hint, int lines) {
            m_Hint = hint;
            m_Lines = lines > 0 ? lines : 1;
        }
    }
    #endregion Unity Attributes
}
