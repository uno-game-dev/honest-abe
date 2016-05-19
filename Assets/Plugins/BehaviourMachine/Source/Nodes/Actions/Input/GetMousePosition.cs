//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the current mouse position in screen coordinates.
    /// <summary>
    [NodeInfo(  category = "Action/Input/",
    			icon = "Mouse",
                description = "Gets the current mouse position in screen coordinates",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Input-mousePosition.html")]
	public class GetMousePosition : ActionNode {

		/// <summary>
		/// Store the mouse position; z is ignored.
		/// <summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the mouse position; z is ignored")]
		public Vector3Var storeMousePosition;

		/// <summary>
		/// Store the x component of the mouse position.
		/// <summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the x component of the mouse position")]
		public FloatVar storeX;

		/// <summary>
		/// Store the y component of the mouse position.
		/// <summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the y component of the mouse position")]
		public FloatVar storeY;

		public override void Reset () {
			storeMousePosition = new ConcreteVector3Var();
			storeX = new ConcreteFloatVar();
			storeY = new ConcreteFloatVar();
		}

		public override Status Update () {

			// Store the mouse position;
			storeMousePosition.vector2Value = Input.mousePosition;
			storeX.Value = storeMousePosition.Value.x;
			storeY.Value = storeMousePosition.Value.y;

			return Status.Success;
		}
	}
}