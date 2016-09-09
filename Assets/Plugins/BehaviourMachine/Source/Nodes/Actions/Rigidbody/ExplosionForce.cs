//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Applies an explosion force to all game objects with a rigidbody inside a radius.
    /// <summary>
    [NodeInfo(  category = "Action/Rigidbody/",
                icon = "ConstantForce",
                description = "Applies an explosion force to all game objects with a rigidbody inside a radius",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.AddExplosionForce.html")]
    public class ExplosionForce : ActionNode, IFixedUpdateNode {

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
        /// Applies the force as if it was applied from beneath the object.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Applies the force as if it was applied from beneath the object")]
        public FloatVar upwardsModifier;

        /// <summary>
        /// The type of the force to apply.
        /// <summary>
        [Tooltip("The type of the force")]
        public ForceMode mode = ForceMode.Force;

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
            upwardsModifier = new ConcreteFloatVar();
            mode = ForceMode.Force;
            layers = -1;
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != explosionPosition.Value)
                m_Transform = explosionPosition.Value != null ? explosionPosition.Value.transform : null;

            if ((explosionPoint.isNone && m_Transform == null) || explosionForce.isNone || explosionRadius.isNone)
                return Status.Error;

            // Get explosition position
            Vector3 position = explosionPoint.isNone ? m_Transform.position : explosionPoint.Value;

            // Get upwardsModifier
            var _upwardsModifier = (!upwardsModifier.isNone) ? upwardsModifier.Value : 0f;

            // Get the colliders in the explosion
            foreach (var collider in Physics.OverlapSphere(position, explosionRadius.Value, layers)) {
                // It has a rigidbody?
                var rigidbody = collider.GetComponent<Rigidbody>();
                if (rigidbody != null) {
                    rigidbody.AddExplosionForce(explosionForce.Value, position, explosionRadius.Value, _upwardsModifier, mode);
                }
            }

            return Status.Success;
        }
    }
}