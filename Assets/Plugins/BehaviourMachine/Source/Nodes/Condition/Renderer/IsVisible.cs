//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// The "Game Object" is visible by any camera?
    /// </summary>
    [NodeInfo(  category = "Condition/Renderer/",
                icon = "MeshRenderer",
                description = "The \"Game Object\" is visble by any camera?",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Object-name.html")]
	public class IsVisible : ConditionNode {

		/// <summary>
        /// The game object to test.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to test")]
        public GameObjectVar gameObject;

        public override void Reset () {
            base.Reset ();

        	gameObject = new ConcreteGameObjectVar(this.self);
        }

        [System.NonSerialized]
        Renderer m_Renderer = null;

        public override Status Update () {
        	// Get the renderer
            if (m_Renderer == null || m_Renderer.gameObject != gameObject.Value)
                m_Renderer = gameObject.Value != null ? gameObject.Value.GetComponent<Renderer>() : null;

            // Validate members
            if (m_Renderer == null)
            	return Status.Error;

            if (m_Renderer.isVisible) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }
	}
}