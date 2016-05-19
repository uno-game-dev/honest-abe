//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Move function taking absolute movement deltas (requires a Character Controller).
    /// It is recommended that you make only one call to SimpleMove per frame.
    /// </summary>
    [NodeInfo(  category = "Action/CharacterController/",
                icon = "CharacterController",
                description = "Move function taking absolute movement deltas (requires a Character Controller). It is recommended that you make only one call to SimpleMove per frame",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/CharacterController.SimpleMove.html")]
    public class SimpleMove : ActionNode {

        /// <summary>
        /// A game object that has a CharacterController.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has a CharacterController")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The direction to move towards.
        /// </summary>
        [VariableInfo(tooltip = "The direction to move towards")]
        public Vector3Var direction;

        /// <summary>
        /// The direction is relative to the world or the "Game Object"?
        /// </summary>
        [Tooltip("The direction is relative to the world or the \"Game Object\"?")]
        public Space space = Space.World;

        /// <summary>
        /// The speed to move.
        /// </summary>
        [VariableInfo(tooltip = "The speed to move")]
        public FloatVar speed;

        [System.NonSerialized]
        CharacterController m_Controller = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            space = Space.World;
            direction = new ConcreteVector3Var();
            speed = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Get the controller
            if (m_Controller == null || m_Controller.gameObject != gameObject.Value)
                m_Controller = gameObject.Value != null ? gameObject.Value.GetComponent<CharacterController>() : null;

            // Validate members
            if (m_Controller == null || direction.isNone || speed.isNone)
                return Status.Error;

            // World or Self?
            if (space == Space.Self)
                m_Controller.SimpleMove(m_Controller.transform.TransformDirection(direction.Value) * speed.Value);
            else
                m_Controller.SimpleMove(direction.Value * speed.Value);

            return Status.Success;
        }
    }
}