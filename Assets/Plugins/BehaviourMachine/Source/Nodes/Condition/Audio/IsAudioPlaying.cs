//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Is the clip playing right now?
    /// </summary>
    [NodeInfo ( category = "Condition/Audio/",
                icon = "AudioSource",
                description = "Is the clip playing right now?",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/AudioSource-isPlaying.html")]
	public class IsAudioPlaying : ConditionNode {

		/// <summary>
		/// A game object that has an audio source in it.
		/// </summary>
		[VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has an audio source in it")]
		public GameObjectVar gameObject;

		public override Status Update () {
			// Get audio
            var audio = gameObject.Value != null ? gameObject.Value.GetComponent<AudioSource>() : null;

            // Validate members
            if (audio == null)
                return Status.Error;

            // The audio is playing?
            if (audio.isPlaying) {
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