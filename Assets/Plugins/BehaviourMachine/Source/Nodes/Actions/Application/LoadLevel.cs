//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Loads the level by its name or index.
    /// </summary>
    [NodeInfo ( category = "Action/Application/",
                icon = "SceneAsset",
                description = "Loads the level by its name or index",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Application.LoadLevel.html")]
    public class LoadLevel : ActionNode {

        public enum LoadLevelType {
            LoadLevel,
            LoadLevelAdditive,
            LoadLevelAsync,
            LoadLevelAdditiveAsync
        }

        /// <summary>
        /// The name of the level to load.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Index instead", tooltip = "The name of the level to load")]
        public StringVar levelName;

        /// <summary>
        /// The level to load.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Name instead", tooltip = "The level to load")]
        public IntVar levelIndex;

        /// <summary>
        /// LoadLevel: destroys all objects in the current level. LoadLevelAdditive: does not destroy objects in the current level. LoadLevelAsync: destroys all objects in the current level,loads all objects in a background loading thread. LoadLevelAdditiveAsync: load level Additive and Async.
        /// </summary>
        [Tooltip("LoadLevel: destroys all objects in the current level. LoadLevelAdditive: does not destroy objects in the current level. LoadLevelAsync: destroys all objects in the current level,loads all objects in a background loading thread. LoadLevelAdditiveAsync: load level Additive and Async")]
        public LoadLevelType loadLevelType = LoadLevelType.LoadLevel;

        public override void Reset () {
            levelName = new ConcreteStringVar();
            levelIndex = new ConcreteIntVar();
            loadLevelType = LoadLevelType.LoadLevel;
        }

        public override Status Update () {

            if (!levelName.isNone) {
                switch (loadLevelType) {
                    case LoadLevelType.LoadLevel:
                        Application.LoadLevel(levelName.Value);
                        break;
                    case LoadLevelType.LoadLevelAdditive:
                        Application.LoadLevelAdditive(levelName.Value);
                        break;
                    case LoadLevelType.LoadLevelAsync:
                        Application.LoadLevelAsync(levelName.Value);
                        break;
                    case LoadLevelType.LoadLevelAdditiveAsync:
                        Application.LoadLevelAdditiveAsync(levelName.Value);
                        break;
                }
            }
            else if (!levelIndex.isNone) {
                switch (loadLevelType) {
                    case LoadLevelType.LoadLevel:
                        Application.LoadLevel(levelIndex.Value);
                        break;
                    case LoadLevelType.LoadLevelAdditive:
                        Application.LoadLevelAdditive(levelIndex.Value);
                        break;
                    case LoadLevelType.LoadLevelAsync:
                        Application.LoadLevelAsync(levelIndex.Value);
                        break;
                    case LoadLevelType.LoadLevelAdditiveAsync:
                        Application.LoadLevelAdditiveAsync(levelIndex.Value);
                        break;
                }
            }
            else
                return Status.Error;

            return Status.Success;
        }
    }
}