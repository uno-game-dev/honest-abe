//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Plays an AudioClip, and scales the AudioSource volume by "Volume Scale".
    /// </summary>
    [NodeInfo ( category = "Action/Audio/",
                icon = "AudioClip",
                description = "Plays an AudioClip, and scales the AudioSource volume by \"Volume Scale\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/AudioSource.PlayOneShot.html")]
    public class AudioPlayOneShot : ActionNode {

        /// <summary>
        /// A game object that has an audio source in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has an audio source in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The clip being played.
        /// </summary>
    	[VariableInfo(tooltip = "The clip being played")]
        public ObjectVar audioClip;

        /// <summary>
        /// The scale of the volume (0-1).
        /// </summary>
        [VariableInfo(tooltip = "The scale of the volume (0-1)")]
        public FloatVar volumeScale;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            audioClip = new ConcreteObjectVar();
            volumeScale = 1f;
        } 

        public override Status Update () {
            // Get audio
            var audio = gameObject.Value != null ? gameObject.Value.GetComponent<AudioSource>() : null;
            var clip = audioClip.Value as AudioClip;

            // Validate members
            if (audio == null || clip == null)
                return Status.Error;

            audio.PlayOneShot(clip, volumeScale.Value);

            return Status.Success;
        }
    }
}