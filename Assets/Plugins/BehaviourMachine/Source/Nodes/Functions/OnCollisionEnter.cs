//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnCollisionEnter.html")]
    public class OnCollisionEnter : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onCollisionEnter += CollisionEnter;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onCollisionEnter -= CollisionEnter;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get blackboard.onCollisionEnter events.
        /// </summary>
        void CollisionEnter (Collision collision) {
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