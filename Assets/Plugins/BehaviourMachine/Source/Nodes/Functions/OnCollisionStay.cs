//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody that is touching rigidbody/collider.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnCollisionStay is called once per frame for every collider/rigidbody that is touching rigidbody/collider",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnCollisionStay.html")]
    public class OnCollisionStay : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        public override void Reset () {
            base.Reset();
            other = new ConcreteGameObjectVar();
        }

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onCollisionStay += CollisionStay;

                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onCollisionStay -= CollisionStay;

            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get blackboard.onCollisionStay events.
        /// </summary>
        void CollisionStay (Collision collision) {
            other.Value = collision.gameObject;

            // Tick children
            this.OnTick();
        }
    }
}