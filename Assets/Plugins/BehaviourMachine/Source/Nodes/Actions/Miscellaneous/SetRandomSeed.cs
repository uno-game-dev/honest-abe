//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
	/// <summary>
    /// Sets the seed for the random number generator.
    /// </summary>
    [NodeInfo(  category = "Action/Miscellaneous/",
    			icon = "Random",
                description = "Sets the seed for the random number generator",
                url = "http://docs.unity3d.com/ScriptReference/Random-seed.html")]
	public class SetRandomSeed : ActionNode {

		/// <summary>
		/// The new value of the seed.
		/// </summary>
		[VariableInfo(tooltip = "The new value of the seed")]
		public IntVar newSeed;

		public override void Reset () {
			newSeed = new ConcreteIntVar();
		}

		public override Status Update () {
			// Validate members
			if (newSeed.isNone)
				return Status.Error;

			// Set seed
			Random.seed = newSeed.Value;
			return Status.Success;
		}
	}
}