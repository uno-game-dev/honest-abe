//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Tick its child for each game object in the scene that has the supplied name.
    /// </summary>
    [NodeInfo ( category = "Decorator/",
                icon = "PlayLoopOff",
                description = "Tick its child for each game object in the scene that has the supplied name",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject.FindGameObjectsWithTag.html")]
    public class FindGameObjectsByName : DecoratorNode {

        /// <summary>
        /// The name to search for.
        /// </summary>
        [VariableInfo(tooltip = "The name to search for")]
        public StringVar targetName;

        /// <summary>
        /// Optionally specifies the maximum number of iterations.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optionally specifies the maximum number of iterations")]
        public IntVar maxIterations;

        /// <summary>
        /// The game object that has the supplied name. This value changes for each iteration.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The game object that has the supplied name. This value changes for each iteration")]
        public GameObjectVar storeGameObject;

        public override void Reset () {
            targetName = new ConcreteStringVar();
            storeGameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Validate members
            if (child == null || targetName.isNone || storeGameObject.isNone)
                return Status.Error;

            Status childStatus = Status.Error;

            // Don't use max iterations?
            if (maxIterations.isNone) {
                foreach (GameObject gameObject in Object.FindObjectsOfType(typeof(GameObject))) {
                    // The game object has the supplied name?
                    if (gameObject.name == targetName.Value) {
                        // Store the game object
                        storeGameObject.Value = gameObject;

                        // Tick child
                        child.OnTick();
                        childStatus = child.status;

                        if (childStatus == Status.Error || childStatus == Status.Running)
                            return childStatus;
                    }
                }
            }
            else {
                var iterations = maxIterations.Value;
                foreach (GameObject gameObject in Object.FindObjectsOfType(typeof(GameObject))) {
                    // The game object has the supplied name?
                    if (gameObject.name == targetName.Value) {
                        if (--iterations < 0)
                            break;

                        // Store the game object
                        storeGameObject.Value = gameObject;

                        // Tick child
                        child.OnTick();
                        childStatus = child.status;

                        if (childStatus == Status.Error || childStatus == Status.Running)
                            return childStatus;
                    }
                }
            }

            return Status.Success;
        }
    }
}