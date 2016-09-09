//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the parent game object. Returns Failure if the game object has no parent.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Gets the parent game object. Returns Failure if the game object has no parent")]
	public class GetParent : ActionNode {

		/// <summary>
        /// The game object to get the parent.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the parent")]
        public GameObjectVar gameObject;

		/// <summary>
	    /// Store the parent game object.
	    /// </summary>
		[VariableInfo (canBeConstant = false, tooltip = "Store the parent game object")]
		public GameObjectVar storeParent;

		public override void Reset () {
			gameObject = new ConcreteGameObjectVar(this.self);
			storeParent = new ConcreteGameObjectVar();
		}

		public override Status Update () {
			// Validate members
			if (storeParent.isNone || gameObject.Value == null)
				return Status.Error;

			// Get parent
			var parent = gameObject.Value.transform.parent;

			// Validate parent
			if (parent != null) {
				storeParent.Value = parent.gameObject;
				return Status.Success;
			}
			else {
				storeParent.Value = null;
				return Status.Failure;
			}
		}
	}
}