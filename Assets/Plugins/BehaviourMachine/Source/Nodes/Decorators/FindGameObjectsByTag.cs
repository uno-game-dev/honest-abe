//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Tick its child for each game object in the scene that has the supplied tag.
    /// </summary>
    [NodeInfo ( category = "Decorator/",
                icon = "PlayLoopOff",
                description = "Tick its child for each game object in the scene that has the supplied tag",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject.FindGameObjectsWithTag.html")]
    public class FindGameObjectsByTag : DecoratorNode {

        /// <summary>
        /// The tag to search for.
        /// </summary>
        [VariableInfo(tooltip = "The tag to search for")]
        public StringVar tag;

        /// <summary>
        /// Optionally specifies the maximum number of iterations.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optionally specifies the maximum number of iterations")]
        public IntVar maxIterations;

        /// <summary>
        /// The game object that has the supplied tag. This value changes for each iteration.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The game object that has the supplied tag. This value changes for each iteration")]
        public GameObjectVar storeGameObject;

        public override void Reset () {
            tag = new ConcreteStringVar();
            storeGameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Validate members
            if (child == null || tag.isNone || storeGameObject.isNone)
                return Status.Error;

            Status childStatus = Status.Error;

            // Don't use max iterations?
            if (maxIterations.isNone) {
                foreach (var gameObject in GameObject.FindGameObjectsWithTag(tag.Value)) {
                    // Store the game object
                    storeGameObject.Value = gameObject;

                    // Tick child
                    child.OnTick();
                    childStatus = child.status;

                    if (childStatus == Status.Error || childStatus == Status.Running)
                        return childStatus;
                }
            }
            else {
                var iterations = maxIterations.Value;

                foreach (var gameObject in GameObject.FindGameObjectsWithTag(tag.Value)) {
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

            return Status.Success;
        }
    }
}