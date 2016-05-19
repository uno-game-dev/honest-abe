//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using System;
using System.Reflection;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
    /// <summary>
    /// Utility class to get types.
    /// </summary>
    public class TypeUtility {

        static Assembly[] s_LoadedAssemblies;
        static Dictionary<string, Type> s_LoadedType = new Dictionary<string, Type>();
        static Dictionary<Type, Type> s_ConcreteType = new Dictionary<Type, Type>();
        static Dictionary<Type, Type> s_BaseType = new Dictionary<Type, Type>();

        #region Properties
        /// <summary>
        /// Runtime assemblies.
        /// </summary>
        public static Assembly[] loadedAssemblies {
            get {
                if (s_LoadedAssemblies == null) {
                    s_LoadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                    // Remove Editor assemblies
                    var runtimeAsms = new List<Assembly>();
                    foreach (Assembly asm in s_LoadedAssemblies) {
                        if (!asm.GetName().Name.Contains("Editor"))
                            runtimeAsms.Add(asm);
                    }
                    s_LoadedAssemblies = runtimeAsms.ToArray();
                }

                return s_LoadedAssemblies;
            }
        }
        #endregion Properties

        #region Public Methods
        /// <summary>
        /// Returns the children classes of the supplied type.
        /// <param name="baseType">The base type.</param>
        /// <returns>The children classes of baseType.</returns>
        /// </summary>
        public static System.Type[] GetDerivedTypes (System.Type baseType) {
            // Create the derived type list
            var derivedTypes = new List<System.Type>();

            foreach (Assembly asm in TypeUtility.loadedAssemblies) {
                // Get types
                Type[] exportedTypes;

                try {
                    exportedTypes = asm.GetExportedTypes();
                }
                catch (ReflectionTypeLoadException) {
                    Print.LogWarning(string.Format("Behaviour Machine will ignore the following assembly due to type-loading errors: {0}", asm.FullName));
                    continue;
                }

                for (int i = 0; i < exportedTypes.Length; i++) {
                    // Get type
                    Type type = exportedTypes[i];
                    // The type is a subclass of baseType?
                    if (!type.IsAbstract && type.IsSubclassOf (baseType) && type.FullName != null) {
                        derivedTypes.Add(type);
                    }
                }
            }
            derivedTypes.Sort((Type o1, Type o2) => o1.ToString().CompareTo (o2.ToString ()));
            return derivedTypes.ToArray();
        }

        /// <summary>
        /// Returns the System.Type of the supplied name.
        /// <param name="name">The type name.</param>
        /// <returns>The System.Type of the supplied string.</returns>
        /// </summary>
        public static Type GetType (string name) {
            // Try to get the type
            Type type = null;
            if (s_LoadedType.TryGetValue(name, out type))
                return type;

            // Try C# scripts
            type = Type.GetType (name + ",Assembly-CSharp-firstpass") ?? Type.GetType (name + ",Assembly-CSharp");

            // Try AppDomain
            if (type == null) {
                foreach (Assembly asm in TypeUtility.loadedAssemblies) {
                    type = asm.GetType (name);
                    if (type != null)
                        break;
                }
            }

            // Add type
            s_LoadedType.Add(name, type);

            return type;
        }

        /// <summary>
        /// Returns the concrete class System.Type of the supplied type.
        /// It goes trough the class inheritance of the supplied type and searchs for the ConcreteClassAttribute.
        /// <param name="targetType">The target type.</param>
        /// <returns>The concrete class System.Type.</returns>
        /// <seealso cref="BehaviourMachine.Attributes" />
        /// </summary>
        public static Type GetConcreteType (Type targetType) {
            // Try to get the type
            Type type = null;
            if (s_ConcreteType.TryGetValue(targetType, out type))
                return type;

            {
                System.Type typeIterator = targetType;
                do {
                    // if (typeIterator.IsAbstract) {
                        var attrs = typeIterator.GetCustomAttributes(typeof(ConcreteClassAttribute), true);
                        if (attrs != null && attrs.Length > 0 && attrs[0] is ConcreteClassAttribute) {
                            var concreteAttr = attrs[0] as ConcreteClassAttribute;
                            type = concreteAttr.concreteType;
                            break;
                        }
                    // }
                    typeIterator = typeIterator.BaseType;
                } while (typeIterator != null && typeIterator.BaseType != typeof(Variable));
            }

            if (type == null)
                type = targetType;

            // Add type
            s_ConcreteType.Add(targetType, type);

            return type;
        }

        /// <summary>
        /// Returns the base class System.Type of the supplied type.
        /// The base type its the last abstract type in the class hierarchy.
        /// <param name="targetType">The target type.</param>
        /// <returns>The base class System.Type.</returns>
        /// </summary>
        public static Type GetBaseType (Type targetType) {
            // Try to get the type
            Type type = null;
            if (s_BaseType.TryGetValue(targetType, out type))
                return type;

            {
                System.Type typeIterator = targetType;
                while (typeIterator != typeof(object)) {
                    if (typeIterator.IsAbstract)
                        type = typeIterator;
                    typeIterator = typeIterator.BaseType;
                }
            }

            if (type == null)
                type = targetType;

            // Add type
            s_BaseType.Add(targetType, type);

            return type;
        }
        #endregion Public Methods
    }
}