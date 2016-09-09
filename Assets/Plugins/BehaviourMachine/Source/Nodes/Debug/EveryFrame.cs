//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
	/// <summary>
	/// Every node before this one runs once when the state is enabled. Every node after this one runs every frame (Only for ActionStates).
	/// </summary>
	[NodeInfo ( category = "Debug/",
				icon = "Function",
				description = "Every node before this one runs once when the state is enabled. Every node after this one runs every frame (Only for ActionStates)")]
	public class EveryFrame : Update {
		public override void OnEnable () {}
		public override void OnDisable () {}
	}
}