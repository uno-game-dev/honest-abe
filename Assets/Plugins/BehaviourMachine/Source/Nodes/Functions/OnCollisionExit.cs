//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnCollisionExit.html")]
    public class OnCollisionExit : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onCollisionExit += CollisionExit;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onCollisionExit -= CollisionExit;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get blackboard.onCollisionExit events.
        /// </summary>
        void CollisionExit (Collision collision) {
            other.Value = collision.gameObject;

            // Tick children
            this.OnTick();
        }

        public override void Reset () {
            base.Reset();
            other = new ConcreteGameObjectVar();
        }
    }
}