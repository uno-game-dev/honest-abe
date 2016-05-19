//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
	/// <summary>
    /// Sets the color of the scene's ambient light.
    /// </summary>
    [NodeInfo(  category = "Action/Miscellaneous/",
    			icon = "Light",
                description = "Sets the color of the scene's ambient light",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/RenderSettings-ambientLight.html")]
	public class SetAmbientLight : ActionNode {

		/// <summary>
		/// The new color of the ambient light.
		/// </summary>
		[VariableInfo(tooltip = "The new color of the ambient light")]
		public ColorVar newColor;

		public override void Reset () {
			newColor = new ConcreteColorVar();
		}

		public override Status Update () {
			// Validate members
			if (newColor.isNone)
				return Status.Error;

			// Set ambient light color
			RenderSettings.ambientLight = newColor.Value;
			return Status.Success;
		}
	}
}