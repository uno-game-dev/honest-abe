//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnCollisionEnter2D is called when an incoming collider makes contact with this object's collider (2D physics only).
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnCollisionEnter2D is called when an incoming collider makes contact with this object's collider (2D physics only)",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html")]
    public class OnCollisionEnter2D : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onCollisionEnter2D += CollisionEnter2D;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onCollisionEnter2D -= CollisionEnter2D;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get blackboard.onCollisionEnter2D events.
        /// </summary>
        void CollisionEnter2D (Collision2D collision2D) {
            other.Value = collision2D.gameObject;

            // Tick children
            this.OnTick();
        }

        public override void Reset () {
            base.Reset();
            other = new ConcreteGameObjectVar();
        }
    }
}
#endif