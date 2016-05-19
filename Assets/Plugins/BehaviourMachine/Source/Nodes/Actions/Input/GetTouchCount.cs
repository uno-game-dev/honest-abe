//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Gets the number of touches.
    /// </summary>
    [NodeInfo(  category = "Action/Input/",
                icon = "BuildSettings.iPhone.Small",
                description = "Gets the number of touches",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Input-touchCount.html")]
	public class GetTouchCount : ActionNode {

		/// <summary>
		/// Store the number of touches.
		/// </summary>
		[VariableInfo(canBeConstant = false, tooltip = "Store the number of touches")]
		public IntVar storeTouchCount;

		public override void Reset () {
			storeTouchCount = new ConcreteIntVar();
		}

		public override Status Update () {
			storeTouchCount.Value = Input.touchCount;
			return Status.Success;
		}
	}
}