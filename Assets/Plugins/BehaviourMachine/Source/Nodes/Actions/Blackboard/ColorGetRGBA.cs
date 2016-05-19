//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the components (r, g, b, a) of a color.
    /// </summary>
    [NodeInfo( 	category = "Action/Blackboard/",
				icon = "Blackboard",
                description = "Gets the components (r, g, b, a) of a color")]
	public class ColorGetRGBA : ActionNode {

		/// <summary>
        /// The variable to get the component values.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The variable to get the component values")]
        public ColorVar variable;

	    /// <summary>
	    /// Store the red component of the color.
	    /// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the red component of the color")]
		public FloatVar storeR;

		/// <summary>
	    /// Store the green component of the color.
	    /// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the green component of the color")]
		public FloatVar storeG;

		/// <summary>
	    /// Store the blue component of the color.
	    /// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the blue component of the color")]
		public FloatVar storeB;

		/// <summary>
	    /// Store the alpha component of the color.
	    /// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the alpha component of the color")]
		public FloatVar storeA;

		public override void Reset () {
			variable = new ConcreteColorVar();
			storeR = new ConcreteFloatVar();
			storeG = new ConcreteFloatVar();
			storeB = new ConcreteFloatVar();
			storeA = new ConcreteFloatVar();
		}

		public override Status Update () {
			if (variable.isNone)
				return Status.Error;

			// Get variable values
			storeR.Value = variable.Value.r;
			storeG.Value = variable.Value.g;
			storeB.Value = variable.Value.b;
			storeA.Value = variable.Value.a;

			return Status.Success;
		}
	}
}