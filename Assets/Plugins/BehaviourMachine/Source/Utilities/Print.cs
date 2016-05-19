//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary> 
    /// Class containing methods to print messages on the Unity console.
    /// </summary>
    public static class Print {

        public static readonly string bmColoredStringLogo = "{<b><color=#78a413>b</color></b>}";
        public static readonly string bmStringLogo = "{b}";

        /// <summary> 
        /// Returns the logo for the current platform.
        /// <returns>The {b} logo.</returns> 
        /// </summary>
        public static string GetLogo () {
            if (Application.platform == RuntimePlatform.OSXEditor)
                return Print.bmColoredStringLogo;
            else
                return Print.bmStringLogo;
        }

        /// <summary> 
        /// Logs message to the Unity Console.
        /// <param name="message">The message to be printed in the console.</param> 
        /// </summary>
        static public void Log (string message) {
            if (Debug.isDebugBuild) {
                Debug.Log(Print.GetLogo() + ": " + message);
            }
        }

        /// <summary> 
        /// Logs message to the Unity Console.
        /// <param name="message">The message to be printed in the console.</param> 
        /// <param name="context">An object to be highlighted if you select the message in the console.</param> 
        /// </summary>
        static public void Log (string message, UnityEngine.Object context) {
            if (Debug.isDebugBuild) {
                Debug.Log(Print.GetLogo() + ": " + message, context);
            }
        }

        /// <summary> 
        /// Logs a warning message to the Unity Console.
        /// <param name="message">The message to be printed in the console.</param> 
        /// </summary>
        static public void LogWarning (string message) {
            if (Debug.isDebugBuild)
                Debug.LogWarning(Print.GetLogo() + ": " + message);
        }

        /// <summary> 
        /// Logs a warning message to the Unity Console.
        /// <param name="message">The message to be printed in the console.</param> 
        /// <param name="context">An object to be highlighted if you select the message in the console.</param> 
        /// </summary>
        static public void LogWarning (string message, UnityEngine.Object context) {
            if (Debug.isDebugBuild)
                Debug.LogWarning(Print.GetLogo() + ": " + message, context);
        }

        /// <summary> 
        /// Logs an error message to the Unity Console.
        /// <param name="message">The message to be printed in the console.</param> 
        /// </summary>
        static public void LogError (string message) {
            if (Debug.isDebugBuild)
                Debug.LogError(Print.GetLogo() + ": " + message);
        }

        /// <summary> 
        /// Logs an error message to the Unity Console.
        /// <param name="message">The message to be printed in the console.</param> 
        /// <param name="context">An object to be highlighted if you select the message in the console.</param> 
        /// </summary>
        static public void LogError (string message, UnityEngine.Object context) {
            if (Debug.isDebugBuild)
                Debug.LogError(Print.GetLogo() + ": " + message, context);
        }
    }
}