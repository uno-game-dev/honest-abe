//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets two perpendicular axis in a 3D space relative to a game object. This node is useful to get the input of an analog/digital stick or wasd.
    /// <summary>
    [NodeInfo(  category = "Action/Input/",
                icon = "Axis",
                description = "Gets two perpendicular axis in a 3D space relative to a game object. This node is useful to get the input of an analog/digital stick or wasd")]
    public class GetAxisVector3 : ActionNode {

        public enum AxisPlane {XZ, XY, YZ}

        /// <summary>
        /// The name of the axis (e.g. Horizontal, Vertical...). See axis in Edit/Project Settings/Input.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Horizontal", tooltip = "The name of the axis (e.g. Horizontal, Vertical...). See axis in Edit/Project Settings/Input")]
        public StringVar horizontalAxis;

        /// <summary>
        /// The name of the axis (e.g. Horizontal, Vertical...). See axis in Edit/Project Settings/Input.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Vertical", tooltip = "The name of the axis (e.g. Horizontal, Vertical...). See axis in Edit/Project Settings/Input")]
        public StringVar verticalAxis;

        /// <summary>
        /// The plane to map the axis.
        /// <summary>
        [Tooltip("The plane to map the axis")]
        public AxisPlane mapToPlane;

        /// <summary>
        /// An optional game object to map the input relative to.
        /// <summary>
        [VariableInfo(requiredField = false, tooltip = "An optional game object to map the input relative to")]
        public GameObjectVar relativeTo;

        /// <summary>
        /// An optional float to multiply the input before store in "Store Input".
        /// <summary>
        [VariableInfo(tooltip = "An optional float to multiply the input before store in \"Store Input\"")]
        public FloatVar multiplier;

        public bool normalize = false;

        /// <summary>
        /// Variable to store the input value.
        /// <summary>
        [VariableInfo(canBeConstant = false, tooltip = "Variable to store the input value")]
        public Vector3Var storeInput;

        public override void Reset () {
            horizontalAxis = new ConcreteStringVar();
            verticalAxis = new ConcreteStringVar();
            mapToPlane = AxisPlane.XZ;
            relativeTo = new ConcreteGameObjectVar(this.self);
            multiplier = 1f;
            normalize = false;
            storeInput = new ConcreteVector3Var();
        }

        public override Status Update () {
            // It has a store input?
            if (storeInput.isNone || multiplier.isNone)
                return Status.Error;

            float horizontalAxisValue = Input.GetAxis(!horizontalAxis.isNone ? horizontalAxis.Value : "Horizontal");
            float verticalAxisValue = Input.GetAxis(!verticalAxis.isNone ? verticalAxis.Value : "Vertical");

            Vector3 forward = Vector3.zero;
            Vector3 right = Vector3.zero;

            if (!relativeTo.isNone && relativeTo.Value != null) {
                switch (mapToPlane) {
                case AxisPlane.XZ:
                    forward = relativeTo.Value.transform.TransformDirection(Vector3.forward);
                    forward.y = 0;
                    forward = forward.normalized;
                    right = new Vector3(forward.z, 0, -forward.x);
                    break;
                case AxisPlane.XY:
                    forward = Vector3.up;
                    right = relativeTo.Value.transform.TransformDirection(Vector3.right);
                    break;
                case AxisPlane.YZ:
                    forward = Vector3.up;
                    right = relativeTo.Value.transform.TransformDirection(Vector3.forward);
                    break;
                }
            }
            else {
                switch (mapToPlane) {
                case AxisPlane.XZ:
                    forward = Vector3.forward;
                    right = Vector3.right;
                    break;
                case AxisPlane.XY:
                    forward = Vector3.up;
                    right = Vector3.right;
                    break;
                case AxisPlane.YZ:
                    forward = Vector3.up;
                    right = Vector3.forward;
                    break;
                }
            }

            storeInput.Value = verticalAxisValue * forward + horizontalAxisValue * right;
            if (normalize) storeInput.Value = storeInput.Value.normalized;
            else if (storeInput.Value.sqrMagnitude > 1) storeInput.Value = storeInput.Value.normalized;

            storeInput.Value *= multiplier.Value;

            return Status.Success;
        }
    }
}
