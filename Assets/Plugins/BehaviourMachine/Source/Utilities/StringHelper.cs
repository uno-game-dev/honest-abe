//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
    /// <summary> 
    /// Helper class to manage strings.
    /// </summary>
    public class StringHelper {

        /// <summary> 
        /// Split and adds camel case in the supplied string.
        /// <param name="text">The string to split and add camel case.</param>
        /// <returns>The supplied string with camel case and splitted.</returns>
        /// </summary>
        public static string SplitCamelCase (string text) {
                
            string word = string.Empty;
            
            if (!string.IsNullOrEmpty(text)) {

                if (text[text.Length - 1] == ')')
                    return text;

                word += (char.IsLower(text[0])) ? char.ToUpper(text[0]) : text[0];
                text = text.Substring(1);
                
                for(int i = 0; i < text.Length; i++)
                    word += ((char.IsLower(text[i])) ? "" : " ") + text[i];
            }
                
            return word;
        }

        /// <summary> 
        /// Returns a unique name in a list of strings.
        /// <param name="names">A list of strings.</param>
        /// <param name="name">The desired unique name.</param>
        /// <returns>A unique name in the list of strings.</returns>
        /// </summary>
        public static string GetUniqueNameInList (List<string> names, string name) {
            string uniqueName = name;
            int y = 1;

            while (names.Contains(uniqueName)) {
                y++;
                uniqueName = name + " (" + y.ToString() + ")";
            }

            return uniqueName;
        }
    }
}