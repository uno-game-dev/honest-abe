//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the status returned by the child. Returns Error if there is no child.
    /// </summary>
    [NodeInfo ( category = "Decorator/",
                icon = "Status",
                description = "Changes the status returned by the child. Returns Error if there is no child")]
    public class ConvertStatus : DecoratorNode {

        /// <summary>
        /// The status to be returned if the child status is Ready.
        /// </summary>
        [Tooltip("The status to be returned if the child status is Ready")]
        public Status convertReadyTo = Status.Ready;

        /// <summary>
        /// The status to be returned if the child status is Success.
        /// </summary>
        [Tooltip("The status to be returned if the child status is Success")]
        public Status convertSuccessTo = Status.Success;

        /// <summary>
        /// The status to be returned if the child status is Failure.
        /// </summary>
        [Tooltip("The status to be returned if the child status is Failure")]
        public Status convertFailureTo = Status.Failure;

        /// <summary>
        /// The status to be returned if the child status is Error.
        /// </summary>
        [Tooltip("The status to be returned if the child status is Error")]
        public Status convertErrorTo = Status.Error;

        /// <summary>
        /// The status to be returned if the child status is Running.
        /// </summary>
        [Tooltip("The status to be returned if the child status is Running")]
        public Status convertRunningTo = Status.Running;

    	public override void Reset () {
            convertReadyTo = Status.Ready;
            convertSuccessTo = Status.Success;
            convertFailureTo = Status.Failure;
            convertErrorTo = Status.Error;
            convertRunningTo = Status.Running;
        }

        public override Status Update () {

            // Validate members
            if (child == null)
                return Status.Error;

            child.OnTick();
            Status childStatus = child.status;

            switch (childStatus) {
                case Status.Ready:
                    childStatus = convertReadyTo;
                    break;
                case Status.Success:
                    childStatus = convertSuccessTo;
                    break;
                case Status.Failure:
                    childStatus = convertFailureTo;
                    break;
                case Status.Error:
                    childStatus = convertErrorTo;
                    break;
                case Status.Running:
                    childStatus = convertRunningTo;
                    break;
            }

            return childStatus;
        }
    }
}