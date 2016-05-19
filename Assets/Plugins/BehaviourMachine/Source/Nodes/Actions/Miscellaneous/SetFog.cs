//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
	/// <summary>
    /// Sets the fog properties of the scene. These values can be accessed in Edit -> Render Settings from the toolbar.
    /// </summary>
    [NodeInfo(  category = "Action/Miscellaneous/",
    			icon = "GameManager",
                description = "Sets the fog properties of the scene. These values can be accessed in Edit -> Render Settings from the toolbar",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/RenderSettings.html")]
	public class SetFog : ActionNode {

		public enum FogMode {DontChange, Linear, Exponential, Exp2}

		/// <summary>
		/// Use fog in the scene?
		/// </summary>
		[VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Use fog in the scene?")]
		public BoolVar fog;

		/// <summary>
		/// Fog mode to use.
		/// </summary>
		[Tooltip("Fog mode to use")]
		public SetFog.FogMode fogMode;

		/// <summary>
		/// Fog color.
		/// </summary>
		[VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Fog color")]
		public ColorVar fogColor;

		/// <summary>
		/// Density for exponential fog.
		/// </summary>
		[VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "Density for exponential fog")]
		public FloatVar fogDensity;

		/// <summary>
		/// The starting distance of linear fog.
		/// </summary>
		[VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The starting distance of linear fog")]
		public FloatVar linearFogStart;

		/// <summary>
		/// The ending distance of linear fog.
		/// </summary>
		[VariableInfo(requiredField = false, nullLabel = "Don't Change", tooltip = "The ending distance of linear fog")]
		public FloatVar linearFogEnd;

		public override void Reset () {
			fog = new ConcreteBoolVar();
			fogColor = new ConcreteColorVar();
			fogDensity = new ConcreteFloatVar();
			linearFogStart = new ConcreteFloatVar();
			linearFogEnd = new ConcreteFloatVar();
		}

		public override Status Update () {
			// Set fog
			if (!fog.isNone) RenderSettings.fog = fog.Value;

			switch (fogMode) {
				case SetFog.FogMode.Linear:
					RenderSettings.fogMode = UnityEngine.FogMode.Linear;
					break;
				case SetFog.FogMode.Exponential:
					RenderSettings.fogMode = UnityEngine.FogMode.Exponential;
					break;
				case SetFog.FogMode.Exp2:
					RenderSettings.fogMode = UnityEngine.FogMode.ExponentialSquared;
					break;
			}

			if (!fogColor.isNone) RenderSettings.fogColor = fogColor.Value;
			if (!fogDensity.isNone) RenderSettings.fogDensity = fogDensity.Value;
			if (!linearFogStart.isNone) RenderSettings.fogStartDistance = linearFogStart.Value;
			if (!linearFogEnd.isNone) RenderSettings.fogEndDistance = linearFogEnd.Value;

			return Status.Success;
		}
	}
}