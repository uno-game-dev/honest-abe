//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
	/// <summary>
	/// Returns Success if the device is being shaken; otherwise returns Failure.
	/// <summary>
	[NodeInfo(  category = "Condition/Input/",
                icon = "BuildSettings.iPhone.Small",
                description = "Returns Success if the device is being shaken; otherwise returns Failure")]
	public class IsDeviceBeingShaken : ConditionNode {

		/// <summary>
		/// Shake intensity to test.
		/// <summary>
		[VariableInfo(tooltip = "Shake intensity to test")]
		public FloatVar shakeIntensity;

		public override void Reset () {
            base.Reset();

            shakeIntensity = 2f;
        }

        public override Status Update () {
            // Validate members
        	if (shakeIntensity.isNone)
        		return Status.Error;

        	if (Input.acceleration.sqrMagnitude > shakeIntensity.Value * shakeIntensity.Value) {
        		// Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

        		return Status.Success;
        	}
        	else
        		return Status.Failure;
        }
	}
}