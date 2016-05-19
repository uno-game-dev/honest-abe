//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Changes the shader of the "Game Object".
    /// </summary>
    [NodeInfo ( category = "Action/Renderer/",
                icon = "MeshRenderer",
                description = "Changes the shader of the \"Game Object\"")]
    public class SetRendererShader : ActionNode {

        /// <summary>
        /// The game object to change the shader.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to change the shader")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The name of the new shader.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use \"New Shader\" instead", tooltip = "The name of the new shader")]
        public StringVar newShaderName;

        /// <summary>
        /// The new shader.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use \"New Shader Name\" instead", tooltip = "The new shader")]
        public ObjectVar newShader;

        [System.NonSerialized]
        Renderer m_Renderer = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newShaderName = "Diffuse";
            newShader = new ConcreteObjectVar();
        }

        public override Status Update () {
            // Get the renderer
            if (m_Renderer == null || m_Renderer.gameObject != gameObject.Value)
                m_Renderer = gameObject.Value != null ? gameObject.Value.GetComponent<Renderer>() : null;


            // Validate members
            if (m_Renderer == null)
                return Status.Error;

            // Validate newShaderName
            if (!newShaderName.isNone) {
                m_Renderer.material.shader = Shader.Find(newShaderName.Value);
                return Status.Success;
            }
            // Validate newShader
            else if (!newShader.isNone && newShader.Value != null) {
                m_Renderer.material.shader = newShader.Value as Shader;
                return Status.Success;
            }

            return Status.Error;
        }

    }
}