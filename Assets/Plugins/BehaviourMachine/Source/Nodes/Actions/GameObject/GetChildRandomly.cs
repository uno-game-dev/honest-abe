//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets a random child. Returns Failure if there is no child in the game object.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Gets a random child. Returns Failure if there is no child in the game object")]
	public class GetChildRandomly : ActionNode {

		/// <summary>
        /// The game object to get the child.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the child. Returns Failure if there is no child in the game object")]
        public GameObjectVar gameObject;

		/// <summary>
	    /// Store the child game object.
	    /// </summary>
		[VariableInfo (canBeConstant = false, tooltip = "Store the child game object")]
		public GameObjectVar storeChild;

		public override void Reset () {
			gameObject = new ConcreteGameObjectVar(this.self);
			storeChild = new ConcreteGameObjectVar();
		}

		public override Status Update () {
			// Get the target transform
			var targetTransform = gameObject.Value != null ? gameObject.Value.transform : null;

			// Validate members
			if (targetTransform == null || storeChild.isNone)
				return Status.Error;

			if (targetTransform.childCount == 0) {
				storeChild.Value = null;
				return Status.Failure;
			}

			// Get child
			storeChild.Value = targetTransform.GetChild(Random.Range(0, targetTransform.childCount)).gameObject;
			return Status.Success;
		}
	}
}
