//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Returns Success if the device physical orientation (reported by OS) is the same as \"Device Orientation\"; otherwise returns Failure.
    /// <summary>
    [NodeInfo(  category = "Condition/Input/",
                icon = "BuildSettings.iPhone.Small",
                description = "Returns Success if the device physical orientation (reported by OS) is the same as \"Device Orientation\"; otherwise returns Failure",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Input-deviceOrientation.html")]
    public class IsDeviceOrientation : ConditionNode {

        /// <summary>
        /// The orientation to test.
        /// <summary>
        [Tooltip("The orientation to test")]
        public DeviceOrientation deviceOrientation;

        public override void Reset () {
            base.Reset();

            deviceOrientation = DeviceOrientation.Unknown;
        }

        public override Status Update () {
            if (Input.deviceOrientation == deviceOrientation) {
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