//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly gets a game object in the scene. Returns Failure if there is no game object with the specified tag.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
    			icon = "GameObject",
                description = "Randomly gets a game object in the scene. Returns Failure if there is no game object with the specified tag")]
	public class GetRandomGameObject : ActionNode {

		/// <summary>
	    /// Optionally filter game objects by tag.
	    /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optionally filter game objects by tag")]
		public StringVar filterByTag;

		/// <summary>
	    /// Store the game object.
	    /// </summary>
		[VariableInfo (canBeConstant = false, tooltip = "Store the game object")]
		public GameObjectVar storeGameObject;

		public override void Reset () {
			filterByTag= new ConcreteStringVar();
			storeGameObject = new ConcreteGameObjectVar();
		}

		public override Status Update () {
			// Validate members
			if (storeGameObject.isNone)
				return Status.Error;

			// Create a game object array
			GameObject[] gameObjects;

			// Filter by tag?
			if (!filterByTag.isNone) {
				// Get all game objects with the specified tag
				gameObjects = GameObject.FindGameObjectsWithTag(filterByTag.Value);

				// There is no game object with the specified tag?
				if (gameObjects.Length == 0) {
					storeGameObject.Value = null;
					return Status.Failure;
				}
			}
			else {
				// Get all game objects in the scene
				gameObjects = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
			}

			// Randomly selects a game object
			storeGameObject.Value = gameObjects[Random.Range(0, gameObjects.Length)];
			return Status.Success;
		}
	}
}