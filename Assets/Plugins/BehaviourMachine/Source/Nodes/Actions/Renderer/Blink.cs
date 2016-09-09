//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Enables and disables the renderer component over time.
    /// </summary>
    [NodeInfo ( category = "Action/Renderer/",
                icon = "MeshRenderer",
                description = "Enables and disables the renderer component over time")]
    public class Blink : ActionNode {

        /// <summary>
        /// The game object to blink.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to blink")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The time that the game object will be visible.
        /// </summary>
        [VariableInfo(tooltip = "The time that the game object will be visible")]
        public FloatVar onTime;

        /// <summary>
        /// The time that the game object will be invisible.
        /// </summary>
        [VariableInfo(tooltip = "The time that the game object will be invisible")]
        public FloatVar offTime;

        [System.NonSerialized]
        Renderer m_Renderer = null;

        [System.NonSerialized]
        float m_Timer = 0f;

        public override void Reset () {
            gameObject = this.self;
            onTime = 1f;
            offTime = 1f;

            m_Timer = 0f;
        }

        public override Status Update () {
            // Get the renderer
            if (m_Renderer == null || m_Renderer.gameObject != gameObject.Value)
                m_Renderer = gameObject.Value != null ? gameObject.Value.GetComponent<Renderer>() : null;

            // Validate members?
            if  (m_Renderer == null)
                return Status.Error;

            // Update timer
            m_Timer -= owner.deltaTime;

            // Change renderer enabled?
            if (m_Timer < 0f) {
                m_Renderer.enabled = !m_Renderer.enabled;
                if (m_Renderer.enabled)
                    m_Timer += onTime.Value;
                else
                    m_Timer += offTime.Value;
            }

            return Status.Success;
        }
    }

}
