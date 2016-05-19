//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Applies an explosion force to all game objects with a rigidbody2D inside a radius.
    /// <summary>
    [NodeInfo(  category = "Action/Rigidbody2D/",
                icon = "ConstantForce",
                description = "Applies an explosion force to all game objects with a rigidbody2D inside a radius")]
    public class ExplosionForce2D : ActionNode, IFixedUpdateNode {

        /// <summary>
        /// The position point of the explosion.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Explosion Position instead", tooltip = "The position point of the explosion")]
        public Vector3Var explosionPoint;

        /// <summary>
        /// The position of the explosion.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The position of the explosion")]
        public GameObjectVar explosionPosition;

        /// <summary>
        /// The force of the explosion.
        /// <summary>
        [VariableInfo(tooltip = "The force of the explosion")]
        public FloatVar explosionForce;

        /// <summary>
        /// The explosion radius.
        /// <summary>
        [VariableInfo(tooltip = "The explosion radius")]
        public FloatVar explosionRadius;

        /// <summary>
        /// You can filter the game objects that will receive the explosion force by layer.
        /// <summary>
        [Tooltip("You can filter the game objects that will receive the explosion force by layer")]
        public LayerMask layers = -1;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            explosionPoint = new ConcreteVector3Var();
            explosionPosition = new ConcreteGameObjectVar(this.self);
            explosionForce = new ConcreteFloatVar();
            explosionRadius = new ConcreteFloatVar();
            layers = -1;
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != explosionPosition.Value)
                m_Transform = explosionPosition.Value != null ? explosionPosition.Value.transform : null;

             // Validate members?
            if ((explosionPoint.isNone && m_Transform == null) || explosionForce.isNone || explosionRadius.isNone)
                return Status.Error;

            // Get explosition position
            Vector2 explosionPosV2 = explosionPoint.isNone ? new Vector2(m_Transform.position.x, m_Transform.position.y) : explosionPoint.vector2Value;
            
            // Get the colliders in the explosion
            foreach (var collider in Physics2D.OverlapCircleAll(explosionPosV2, explosionRadius.Value, layers)) {
                // It has a rigidbody2D?
                var rigidbody2D = collider.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null) {
                    // Get the explosion direction
                    Vector3 position = collider.transform.position;
                    var explosionDirection = new Vector2 (position.x, position.y) - explosionPosV2;

                    // Calculate distance
                    var distance = explosionDirection.magnitude;

                    // Is the distance not zero?
                    if (distance != 0f)
                        // Apply explosion force
                        rigidbody2D.AddForce(explosionDirection.normalized * explosionForce.Value / distance);
                }
            }

            return Status.Success;
        }
    }
}
#endif