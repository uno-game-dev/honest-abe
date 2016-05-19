//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Makes the collision detection system ignore all collisions between collider1 and collider2.
    /// </summary>
    [NodeInfo ( category = "Action/Physics/",
                icon = "Physics",
                description = "Makes the collision detection system ignore all collisions between collider1 and collider2",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics.IgnoreCollision.html")]
    public class IgnoreCollision : ActionNode {

        /// <summary>
        /// A game object that has a collider in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has a collider in it")]
    	public GameObjectVar collider1;

        /// <summary>
        /// A game object that has a collider in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has a collider in it")]
        public GameObjectVar collider2;

        public override void Reset () {
            collider1 = this.self;
            collider2 = this.self;
        }

        public override Status Update () {
            // Get colliders
            Collider c1 = collider1.Value != null ? collider1.Value.GetComponent<Collider>() : null;
            Collider c2 = collider2.Value != null ? collider2.Value.GetComponent<Collider>() : null;

            // Validate members
            if (c1 == null || c2 == null)
                return Status.Error;

            // Ignore collision between these colliders
            Physics.IgnoreCollision(c1, c2);
            return Status.Success;
        }
    }
}
