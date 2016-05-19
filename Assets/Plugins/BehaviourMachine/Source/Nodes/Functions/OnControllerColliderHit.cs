//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnControllerColliderHit is called when the controller hits a collider while performing a Move.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnControllerColliderHit is called when the controller hits a collider while performing a Move",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnControllerColliderHit.html")]
    public class OnControllerColliderHit : FunctionNode {

        /// <summary>
        /// Stores the other game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the other game object")]
        public GameObjectVar other;

        /// <summary>
        /// Stores the impact point in world space.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the impact point in world space")]
        public Vector3Var point;

        public override void Reset () {
            base.Reset();
            other = new ConcreteGameObjectVar();
            point = new ConcreteVector3Var();
        }

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onControllerColliderHit += OnControllerHit;

                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onControllerColliderHit -= OnControllerHit;

            m_Registered = false;
        }

        void OnControllerHit (ControllerColliderHit hit) {
            other.Value = hit.gameObject;
            point.Value = hit.point;

            // Tick children
            this.OnTick();
        }
    }
}