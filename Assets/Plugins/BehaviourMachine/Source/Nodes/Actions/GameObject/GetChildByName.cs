//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets a child game object. Returns Failure if there is no child with the specified name.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Gets a child game object. Returns Failure if there is no child with the specified name")]
	public class GetChildByName : ActionNode {

		/// <summary>
        /// The game object to get the child.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the child")]
        public GameObjectVar gameObject;

        /// <summary>
	    /// The child name.
	    /// </summary>
        [VariableInfo(tooltip = "The child name")]
		public StringVar childName;

		/// <summary>
	    /// Store the child game object.
	    /// </summary>
		[VariableInfo (canBeConstant = false, tooltip = "Store the child game object")]
		public GameObjectVar storeChild;

		public override void Reset () {
			gameObject = new ConcreteGameObjectVar(this.self);
			childName = new ConcreteStringVar();
			storeChild = new ConcreteGameObjectVar();
		}

		public override Status Update () {
			// Validate members
			if (gameObject.Value == null || childName.isNone || storeChild.isNone)
				return Status.Error;

			// Get child by name
			foreach (Transform child in gameObject.Value.transform) {
				// The child has the specified name?
				if (child.name == childName.Value) {
					storeChild.Value = child.gameObject;
					return Status.Success;
				}
			}

			// Child not found
			storeChild.Value = null;
			return Status.Failure;
		}
	}
}
