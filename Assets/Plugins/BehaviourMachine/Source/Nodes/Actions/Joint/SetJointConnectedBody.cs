//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Connects a joint to a rigidbody.
    /// </summary>
    [NodeInfo(  category = "Action/Joint/",
                icon = "ConfigurableJoint",
                description = "Connects a joint to a rigidbody",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Joint-connectedBody.html")]
    public class SetJointConnectedBody : ActionNode {

    	/// <summary>
        /// The game object that has a CharacterJoint, HingeJoint, SpringJoint, FixedJoint or ConfigurableJoint in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has a CharacterJoint, HingeJoint, SpringJoint, FixedJoint or ConfigurableJoint in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The game object that has the Rigidbody to be connected.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has the Rigidbody to be connected")]
        public GameObjectVar newConnectedBody;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newConnectedBody = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Get the joint
            Joint joint = gameObject.Value != null ? gameObject.Value.GetComponent<Joint>() : null;
            // Get the rigidbody
            Rigidbody rigidbody =  newConnectedBody.Value != null ? newConnectedBody.Value.GetComponent<Rigidbody>() : null;

            // Validate members
            if (joint == null || rigidbody == null)
                return Status.Error;

            // Do connect
            joint.connectedBody = rigidbody;
            return Status.Success;
        }
    }
}