//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the child at the specified index. Returns Error if there is no child in the game object or the index is greater than the child count.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Gets the child at the specified index. Returns Error if there is no child in the game object or the index is greater than the child count")]
	public class GetChildAtIndex : ActionNode {

		/// <summary>
        /// The game object to get the child.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the child")]
        public GameObjectVar gameObject;

        /// <summary>
	    /// The child index.
	    /// </summary>
        [VariableInfo(tooltip = "The child index")]
		public IntVar childIndex;

		/// <summary>
	    /// Store the child game object.
	    /// </summary>
		[VariableInfo (canBeConstant = false, tooltip = "Store the child game object")]
		public GameObjectVar storeChild;

		public override void Reset () {
			gameObject = new ConcreteGameObjectVar(this.self);
			childIndex = 0;
			storeChild = new ConcreteGameObjectVar();
		}

		public override Status Update () {
			// Get the target transform
			var targetTransform = gameObject.Value != null ? gameObject.Value.transform : null;

			// Validate members
			if (targetTransform == null || childIndex.isNone)
				return Status.Error;

			if (childIndex.Value < 0 || childIndex.Value >= targetTransform.childCount) {
				storeChild.Value = null;
				return Status.Error;
			}

			// Get the child
			storeChild.Value = targetTransform.GetChild(childIndex.Value).gameObject;
			return Status.Success;
		}
	}
}
