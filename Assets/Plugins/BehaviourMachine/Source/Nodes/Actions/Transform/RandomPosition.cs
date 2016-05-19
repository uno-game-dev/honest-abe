//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets a random position in the "Game Object".
    /// </summary>
    [NodeInfo ( category = "Action/Transform/",
                icon = "Transform",
                description = "Sets a random position in the \"Game Object\"")]
    public class RandomPosition : ActionNode {

        /// <summary>
        /// The game object to set the position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to set the position")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The minimum position.
        /// </summary>
        [VariableInfo(tooltip = "The minimum position")]
        public Vector3Var minimum;

        /// <summary>
        /// The maximun position.
        /// </summary>
        [VariableInfo(tooltip = "The maximun position")]
        public Vector3Var maximun;

        /// <summary>
        /// Self, sets the position relative to the parent's position. World, sets the position in world space.
        /// </summary>
        [Tooltip("Self, sets the position relative to the parent's position. World, sets the position in world space")]
        public Space relativeTo = Space.Self;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            minimum = new ConcreteVector3Var();
            maximun = new ConcreteVector3Var();
            relativeTo = Space.Self;
        }

        public override Status Update () {
            // Get the transform
            var transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if  (transform == null || minimum.isNone || maximun.isNone)
                return Status.Error;

            // Get position
            Vector3 position = new Vector3 (Random.Range(minimum.Value.x, maximun.Value.x),
                                            Random.Range(minimum.Value.y, maximun.Value.y),
                                            Random.Range(minimum.Value.z, maximun.Value.z));

            // Local position?
            if (relativeTo == Space.Self)
                transform.localPosition = position;
            else
                transform.position = position;

            return Status.Success;
        }
    }
}