//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the the topmost parent game object in the hierarchy. Always returns Success.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Gets the the topmost parent game object in the hierarch. Always returns Success",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform-root.html")]
	public class GetRoot : ActionNode {

		/// <summary>
        /// The game object to get the root.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the root")]
        public GameObjectVar gameObject;

		/// <summary>
	    /// Store the root game object.
	    /// </summary>
		[VariableInfo (canBeConstant = false, tooltip = "Store the root game object")]
		public GameObjectVar storeRoot;

		public override void Reset () {
			gameObject = new ConcreteGameObjectVar(this.self);
			storeRoot = new ConcreteGameObjectVar();
		}

		public override Status Update () {
			if (storeRoot.isNone || gameObject.Value == null)
				return Status.Error;

			// Get root
			var root = gameObject.Value.transform.root;
			storeRoot.Value = root != null ? root.gameObject : null;

			return Status.Success;
		}
	}
}