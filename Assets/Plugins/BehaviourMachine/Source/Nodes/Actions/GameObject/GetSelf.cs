//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the self game object; the one that has the tree's script attached.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Gets the self game object; the one that has the tree's script attached")]
	public class GetSelf : ActionNode {

		/// <summary>
	    /// Store self.
	    /// </summary>
		[VariableInfo (canBeConstant = false, tooltip = "Store self")]
		public GameObjectVar storeSelf;

		public override void Reset () {
			storeSelf = new ConcreteGameObjectVar();
		}

		public override Status Update () {
			if (storeSelf.isNone)
				return Status.Error;

			storeSelf.Value = this.self;

			return Status.Success;
		}
	}
}