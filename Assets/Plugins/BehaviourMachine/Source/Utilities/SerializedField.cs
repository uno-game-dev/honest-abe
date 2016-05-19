//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System;
using System.Collections;

namespace BehaviourMachine {

	/// <summary> 
    /// Used by NodeSerialization when a node's public member is changed.
    /// <seealso cref="BehaviourMachine.NodeSerialization" />
    /// </summary>
	public class SerializedField {

		#region Members
		object m_Value;
		FieldType m_FieldType;
		#endregion Members

		/// <summary> 
		/// The field value.
		/// <summary> 
		public object value {get {return m_Value;}}

		/// <summary> 
		/// The field type.
		/// <summary> 
		public FieldType fieldType {get {return m_FieldType;}}

		/// <summary> 
		/// Class constructor.
		/// <param name="value">The field value.</param>
		/// <param name="fieldType">The field type.</param>
		/// <summary> 
		public SerializedField (object value, FieldType fieldType) {
			m_Value = value;
			m_FieldType = fieldType;
		}

		/// <summary> 
		/// Returns True if the value has the supplied type; False otherwise.
		/// <param name="type">The field type to check.</param>
		/// <returns>True if the value has the supplied type; False otherwise.</returns>
		/// <summary> 
		public bool HasType (System.Type type) {
			switch (m_FieldType) {
				case FieldType.Int:
					return type == typeof(int);
				case FieldType.String:
					return type == typeof(string); 
				case FieldType.Float:
					return type == typeof(float);
				case FieldType.Enum:
					return type.IsEnum;
				case FieldType.Bool:
					return type == typeof(bool);
				case FieldType.Vector2:
					return type == typeof(Vector2);
				case FieldType.Vector3:
					return type == typeof(Vector3);
				case FieldType.Vector4:
					return type == typeof(Vector4);
				case FieldType.Quaternion:
					return type == typeof(Quaternion);
				case FieldType.Rect:
					return type == typeof(Rect);
				case FieldType.Color:
					return type == typeof(Color);
				case FieldType.LayerMask:
					return type == typeof(LayerMask);
				case FieldType.AnimationCurve:
					return type == typeof(AnimationCurve);
				case FieldType.Array:
					return type.IsArray;
				case FieldType.Constant:
					return type.IsSubclassOf(typeof(Variable));
				case FieldType.None:
					return type.IsSubclassOf(typeof(Variable));
				case FieldType.UnityObject:
					return type.IsSubclassOf(typeof(UnityEngine.Object)) || type == typeof(UnityEngine.Object);
				case FieldType.State:
					return type == typeof(InternalStateBehaviour);
				case FieldType.Generic:
					return !type.IsValueType && !typeof(Variable).IsAssignableFrom(type) && !typeof(UnityEngine.Object).IsAssignableFrom(type);
			}
			return false;
		}
	}
}
