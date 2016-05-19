//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Returns Success if the Animation component is playing an animation; Failure otherwise.
    /// </summary>
    [NodeInfo(  category = "Condition/Animation/",
                icon = "Animation",
                description = "Returns Success if the Animation component is playing an animation; Failure otherwise",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Animation-isPlaying.html")]
    public class IsAnimationPlaying : ConditionNode {

    	/// <summary>
        /// The game object that has an Animation component.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has an Animation component")]
        public GameObjectVar gameObject;

        [System.NonSerialized]
        Animation m_Animation = null;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Get the animation
            if (m_Animation == null || m_Animation.gameObject != gameObject.Value)
                m_Animation = gameObject.Value != null ? gameObject.Value.GetComponent<Animation>() : null;

            // Validate members
            if (m_Animation == null)
                return Status.Error;

            if (m_Animation.isPlaying) {
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