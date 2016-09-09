//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Moves the "Game Object" in the direction and distance of "Translation".
    /// <seealso cref="BehaviourMachine.MovePosition" />
    /// </summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Moves the \"Game Object\" in the direction and distance of \"Translation\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform.Translate.html")]
    public class Translate : ActionNode {

        /// <summary>
        /// The game object to be moved.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to be moved")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Amount to translate.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Amount to translate")]
        public Vector3Var translation;

        /// <summary>
        /// Amount to translate in the x axis (overrides translation.x).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Amount to translate in the x axis (overrides translation.x)")]
        public FloatVar x;

        /// <summary>
        /// Amount to translate in the y axis (overrides translation.y).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Amount to translate in the y axis (overrides translation.y)")]
        public FloatVar y;

        /// <summary>
        /// Amount to translate in the z axis (overrides translation.z).
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Amount to translate in the z axis (overrides translation.z)")]
        public FloatVar z;

        /// <summary>
        /// Self, applies the translation around the transform's local axes. World, applies the translation around the global axes.
        /// </summary>
        [Tooltip("Self, applies the translation around the transform's local axes. World, applies the translation around the global axes")]
        public Space relativeTo = Space.Self;

        /// <summary>
        /// If true the translation will be applied every second; otherwise the translation will be applied instantaneously.
        /// </summary>
        [Tooltip("If true the translation will be applied every second; otherwise the translation will be applied instantaneously")]
        public bool perSecond = true;

        [System.NonSerialized]
        Transform m_Transform = null;

        public override void Reset () {
            gameObject = this.self;
            translation = new ConcreteVector3Var();
            x = new ConcreteFloatVar();
            y = new ConcreteFloatVar();
            z = new ConcreteFloatVar();
            relativeTo = Space.World;
            perSecond = true;
        }

        public override Status Update () {
            // Get the transform
            if (m_Transform == null || m_Transform.gameObject != gameObject.Value)
                m_Transform = gameObject.Value != null ? gameObject.Value.transform : null;

            // Validate members?
            if  (m_Transform == null)
                return Status.Error;

            // Get the desiredTranslation
            Vector3 desiredTranslation =  !translation.isNone ? translation.Value : Vector3.zero;

            // Override values?
            if (!x.isNone) desiredTranslation.x = x.Value;
            if (!y.isNone) desiredTranslation.y = y.Value;
            if (!z.isNone) desiredTranslation.z = z.Value;

            // Translate per second?
            if (perSecond)
                m_Transform.Translate(desiredTranslation * this.owner.deltaTime, relativeTo);
            else
                m_Transform.Translate(desiredTranslation, relativeTo);

            return Status.Success;
        }
    }
}