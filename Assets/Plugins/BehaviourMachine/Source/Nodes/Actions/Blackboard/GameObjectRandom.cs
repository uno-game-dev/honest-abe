//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects an object in a set of game objects.
    /// </summary>
    [NodeInfo(  category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects an object in a set of game objects")]
    public class GameObjectRandom : ActionNode {

        /// <summary>
        /// The possible game object values.
        /// </summary>
        [VariableInfo(tooltip = "The possible game object values")]
        public GameObjectVar[] gameObjects;

        /// <summary>
        /// Store the random selected game object.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected game object")]
        public GameObjectVar storeGameObject;

        public override void Reset () {
            gameObjects = new GameObjectVar[0];
            storeGameObject = new ConcreteGameObjectVar();
        }

        public override Status Update () {
            // Validate members
            if (gameObjects.Length == 0 || storeGameObject.isNone)
                return Status.Error;

            // Randomly selects a game object
            storeGameObject.Value = gameObjects[Random.Range(0, gameObjects.Length)].Value;
            return Status.Success;
        }

    }
}