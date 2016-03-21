//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Reflection;
using BehaviourMachine;
using BehaviourMachineEditor;

/// <summary>
/// A class that holds all Behaviour Machine editor callbacks in the Unity main menu. 
/// </summary>
public class BehaviourMachineToolbar {

	/// <summary>
    /// Opens the BehaviourWindow.
    /// <seealso cref="BehaviourWindow" />
    /// </summary>
    [MenuItem("Window/Behaviour")]
    [MenuItem("Tools/BehaviourMachine/Behaviour Window", false, 1001)]
    public static void ShowBehaviourWindow () {
        EditorWindow.GetWindow<BehaviourMachineMainWindow>(Print.GetLogo() + " Behaviour");
    }

    /// <summary>
    /// Opens the AddNodeWindow.
    /// <seealso cref="AddNodeWindow" /> 
    /// </summary>
    [MenuItem("Window/Add Node")]
    [MenuItem("Tools/BehaviourMachine/Add Node Window", false, 1002)]
    public static void ShowAddNodeWindow () {
        EditorWindow.GetWindow<BehaviourMachineAddNodeWindow>("Add Node");
    }

    /// <summary>
    /// Select GlobalBlackboard; creates if not exists.
    /// <returns>The GlobalBlackboard prefab.</returns>
    /// <seealso cref="BehaviourMachine.InternalGlobalBlackboard" />
    /// </summary>
    [MenuItem("Tools/BehaviourMachine/Global Blackboard", false, 2000)]
    [MenuItem("Assets/Create/Global Blackboard")]
    public static void CreateOrGetGlobalBlackboard () {
        if (Application.isPlaying) {
            // Get object
            Selection.activeObject = GlobalBlackboard.Instance.gameObject;
        }
        else {
            var prefab = Resources.Load("GlobalBlackboard") as GameObject;
            // Create GlobalBlackboard
            if (prefab == null) {
                // The target folder address
                string folder = "Assets/Resources";

                // Does Resources folder exist?
                if (!FileUtility.DirectoryExists(folder)) {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                // Create the prefab
                var go = new GameObject("GlobalBlackboard", typeof(GlobalBlackboard));
                Selection.activeObject = PrefabUtility.CreatePrefab(folder + "/GlobalBlackboard.prefab", go);
                GameObject.DestroyImmediate(go);
            }
            else {
                if (prefab.GetComponent<GlobalBlackboard>() == null)
                    prefab.AddComponent<GlobalBlackboard>();
                Selection.activeObject = prefab;
            }
        }
    }

    /// <summary>
    /// Opens the about window.
    /// </summary>
    [MenuItem("Tools/BehaviourMachine/About...", false, 3000)]
    public static void ShowAboutBehaviourWindow () {
        EditorWindow.GetWindow<BehaviourMachineAboutWindow>(true, "About Behaviour");
    }

    /// <summary>
    /// Opens the Behaviour Machine preferences.
    /// </summary>
    [MenuItem("Tools/BehaviourMachine/Preferences...", false, 3001)]
    public static void ShowPreferencesWindow () {
        // Get assembly
        var asm = Assembly.GetAssembly(typeof(EditorWindow));
        // Get Preferences window type
        System.Type preferencesWindowType = asm.GetType("UnityEditor.PreferencesWindow");
        // Get the ShowPreferencesWindow methods info
        MethodInfo ShowPreferencesWindowMethod = preferencesWindowType.GetMethod("ShowPreferencesWindow", BindingFlags.NonPublic|BindingFlags.Static);
        // Call ShowPreferencesWindow
        ShowPreferencesWindowMethod.Invoke(null, null);
    }

    /// <summary>
    /// Opens the Behaviour Machine getting started in the browser.
    /// </summary>
    [MenuItem("Tools/BehaviourMachine/Help/Getting Started", false, 3011)]
    public static void GoToGettingStarted () {
        Help.BrowseURL("http://behaviourmachine.com/GettingStarted/GettingStarted.pdf");
    }

    /// <summary>
    /// Opens the Behaviour Machine user manual in the browser.
    /// </summary>
    [MenuItem("Tools/BehaviourMachine/Help/User Manual", false, 3012)]
    public static void GoToUserManual () {
        Help.BrowseURL("http://www.behaviourmachine.com/user-manual/");
    }

    /// <summary>
    /// Opens the Behaviour Machine scripting reference in the browser.
    /// </summary>
    [MenuItem("Tools/BehaviourMachine/Help/Scripting Reference", false, 3013)]
    public static void GoToScriptReference () {
        Help.BrowseURL("http://behaviourmachine.com/ScriptReference");
    }

    /// <summary>
    /// Opens the Behaviour Machine youtube channel in the browser.
    /// </summary>
    [MenuItem("Tools/BehaviourMachine/Help/Videos", false, 3014)]
    public static void GoToVideos () {
        Help.BrowseURL("http://www.youtube.com/channel/UCirkB3MbbdFk5s6PKRNynuA");
    }

    /// <summary>
    /// Opens the Behaviour Machine forum in the browser.
    /// </summary>
    [MenuItem("Tools/BehaviourMachine/Help/Forum", false, 3015)]
    public static void GoToForum () {
        Help.BrowseURL("http://behaviourmachine.com/Forum");
    }
}