//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Get the shader of the "Game Object".
    /// </summary>
    [NodeInfo ( category = "Action/Renderer/",
                icon = "MeshRenderer",
                description = "Get the shader of the \"Game Object\"")]
    public class GetRendererShader : ActionNode {

        /// <summary>
        /// The game object to get the shader.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to get the shader")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Store the shader.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the shader")]
        public ObjectVar storeShader;

        [System.NonSerialized]
        Renderer m_Renderer = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            storeShader = new ConcreteObjectVar();
        }

        public override Status Update () {
            // Get the renderer
            if (m_Renderer == null || m_Renderer.gameObject != gameObject.Value)
                m_Renderer = gameObject.Value != null ? gameObject.Value.GetComponent<Renderer>() : null;


            // Validate members
            if (m_Renderer == null || storeShader.isNone)
                return Status.Error;

            // Store the shader
            storeShader.Value = m_Renderer.material.shader;

            return Status.Success;
        }

    }
}