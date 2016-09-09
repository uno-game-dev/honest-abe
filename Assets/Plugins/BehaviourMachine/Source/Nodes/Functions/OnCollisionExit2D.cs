//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnCollisionExit2D is called when a collider on another object stops touching this object's collider (2D physics only).
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnCollisionExit2D is called when a collider on another object stops touching this object's collider (2D physics only)",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnCollisionExit2D.html")]
    public class OnCollisionExit2D : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onCollisionExit2D += CollisionExit2D;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onCollisionExit2D -= CollisionExit2D;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get blackboard.onCollisionExit2D events.
        /// </summary>
        void CollisionExit2D (Collision2D collision2D) {
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