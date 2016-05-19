//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Reloads the current level.
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "SceneAsset",
                description = "Reloads the current level",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Application.LoadLevel.html")]
    public class ReloadLevel : ActionNode {

        public enum LoadLevelType {
            LoadLevel,
            LoadLevelAdditive,
            LoadLevelAsync,
            LoadLevelAdditiveAsync
        }

        /// <summary>
        /// LoadLevel: destroys all objects in the current level. LoadLevelAdditive: does not destroy objects in the current level. LoadLevelAsync: destroys all objects in the current level,loads all objects in a background loading thread. LoadLevelAdditiveAsync: load level Additive and Async.
        /// </summary>
        [Tooltip("LoadLevel: destroys all objects in the current level. LoadLevelAdditive: does not destroy objects in the current level. LoadLevelAsync: destroys all objects in the current level,loads all objects in a background loading thread. LoadLevelAdditiveAsync: load level Additive and Async")]
        public LoadLevelType loadLevelType = LoadLevelType.LoadLevel;

        public override void Reset () {
            loadLevelType = LoadLevelType.LoadLevel;
        }

        public override Status Update () {

            switch (loadLevelType) {
                case LoadLevelType.LoadLevel:
                    Application.LoadLevel(Application.loadedLevel);
                    break;
                case LoadLevelType.LoadLevelAdditive:
                    Application.LoadLevelAdditive(Application.loadedLevel);
                    break;
                case LoadLevelType.LoadLevelAsync:
                    Application.LoadLevelAsync(Application.loadedLevel);
                    break;
                case LoadLevelType.LoadLevelAdditiveAsync:
                    Application.LoadLevelAdditiveAsync(Application.loadedLevel);
                    break;
            }

            return Status.Success;
        }
    }
}