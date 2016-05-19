//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets a child game object by tag and/or name. Returns Failure if there is no child with the specified tag/name.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "FilterByLabel",
                description = "Gets a child game object by tag and/or name. Returns Failure if there is no child with the specified tag/name")]
	public class GetChildByTag : ActionNode {

		/// <summary>
        /// The game object to get the child.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the child")]
        public GameObjectVar gameObject;

        /// <summary>
	    /// The child tag.
	    /// </summary>
        [VariableInfo(tooltip = "The child tag")]
		public StringVar childTag;

        /// <summary>
	    /// Optionally filter child by name.
	    /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optionally filter child by name")]
		public StringVar childName;

		/// <summary>
	    /// Store the child game object.
	    /// </summary>
		[VariableInfo (canBeConstant = false, tooltip = "Store the child game object")]
		public GameObjectVar storeChild;

		public override void Reset () {
			gameObject = new ConcreteGameObjectVar(this.self);
			childTag = new ConcreteStringVar();
			childName = new ConcreteStringVar();
			storeChild = new ConcreteGameObjectVar();
		}

		public override Status Update () {
			// Validate members
			if (gameObject.Value == null || childTag.isNone || storeChild.isNone)
				return Status.Error;

			// Get the specified name or null
			string name = childName.isNone ? null : childName.Value;

			// Get child by name
			foreach (Transform child in gameObject.Value.transform) {
				// The child has the specified tag?
				if (child.CompareTag(childTag.Value)) {
					// No name was specified or the child has the specified name?
					if (name == null || child.name == name) {
						storeChild.Value = child.gameObject;
						return Status.Success;
					}
				}
			}

			// Child not found
			storeChild.Value = null;
			return Status.Failure;
		}
	}
}
