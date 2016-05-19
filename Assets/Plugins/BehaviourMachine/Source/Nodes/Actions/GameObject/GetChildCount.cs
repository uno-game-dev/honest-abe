//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the number of children a game object has.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Gets the number of children a game object has")]
	public class GetChildCount : ActionNode {

		/// <summary>
        /// The game object to get the child count.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the child count")]
        public GameObjectVar gameObject;

		/// <summary>
	    /// Store the number of children the game object has.
	    /// </summary>
		[VariableInfo (canBeConstant = false, tooltip = "Store the number of children the game object has")]
		public IntVar storeChildCount;

		public override void Reset () {
			gameObject = new ConcreteGameObjectVar(this.self);
			storeChildCount = new ConcreteIntVar();
		}

		public override Status Update () {
			// Validate members
			if (gameObject.Value == null || storeChildCount.isNone)
				return Status.Error;

			storeChildCount.Value = gameObject.Value.transform.childCount;
			return Status.Success;
		}
	}
}