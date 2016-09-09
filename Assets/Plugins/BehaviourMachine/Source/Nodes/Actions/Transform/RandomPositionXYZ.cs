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
    public class RandomPositionXYZ : ActionNode {

        /// <summary>
        /// The game object to set the position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to set the position")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The minimum position.x.
        /// </summary>
        [VariableInfo(tooltip = "The minimum position.x")]
        public FloatVar minX;

        /// <summary>
        /// The maximun position.x.
        /// </summary>
        [VariableInfo(tooltip = "The maximun position.x")]
        public FloatVar maxX;

        /// <summary>
        /// The minimum position.y.
        /// </summary>
        [VariableInfo(tooltip = "The minimum position.y")]
        public FloatVar minY;

        /// <summary>
        /// The maximun position.y.
        /// </summary>
        [VariableInfo(tooltip = "The maximun position.y")]
        public FloatVar maxY;

        /// <summary>
        /// The minimum position.z.
        /// </summary>
        [VariableInfo(tooltip = "The minimum position.z")]
        public FloatVar minZ;

        /// <summary>
        /// The maximun position.z.
        /// </summary>
        [VariableInfo(tooltip = "The maximun position.z")]
        public FloatVar maxZ;

        /// <summary>
        /// Self, sets the position relative to the parent's position. World, sets the position in world space.
        /// </summary>
        [Tooltip("Self, sets the position relative to the parent's position. World, sets the position in world space")]
        public Space relativeTo = Space.Self;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            minX = new ConcreteFloatVar();
            maxX = new ConcreteFloatVar();
            minY = new ConcreteFloatVar();
            maxY = new ConcreteFloatVar();
            minZ = new ConcreteFloatVar();
            maxZ = new ConcreteFloatVar();
            relativeTo = Space.Self;
        }

        public override Status Update () {
            // Get the transform
            var transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members
            if  (transform == null)
                return Status.Error;

            // Get position
            Vector3 position = new Vector3 (Random.Range(minX, maxX),
                                            Random.Range(minY, maxY),
                                            Random.Range(minZ, maxZ));

            // Local position?
            if (relativeTo == Space.Self)
                transform.localPosition = position;
            else
                transform.position = position;

            return Status.Success;
        }
    }
}