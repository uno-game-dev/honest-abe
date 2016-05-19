//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Sets the audio volume of an AudioSource.
    /// </summary>
    [NodeInfo ( category = "Action/Audio/",
                icon = "AudioSource",
                description = "Sets the audio volume of an AudioSource",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/AudioSource-volume.html")]
    public class SetAudioVolume : ActionNode {

        /// <summary>
        /// A game object that has an audio source in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has an audio source in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new volume of the audio source (0.0 to 1.0).
        /// </summary>
    	[VariableInfo(tooltip = "The new volume of the audio source (0.0 to 1.0)")]
        public FloatVar newVolume;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newVolume = .5f;
        } 

        public override Status Update () {
            // Get audio
            var audio = gameObject.Value != null ? gameObject.Value.GetComponent<AudioSource>() : null;

            // Validate members
            if (audio == null)
                return Status.Error;

            audio.volume = newVolume.Value;

            return Status.Success;
        }
    }
}