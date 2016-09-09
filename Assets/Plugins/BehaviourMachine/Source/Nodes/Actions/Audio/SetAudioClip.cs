//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Sets the default AudioClip to play in the AudioSource.
    /// </summary>
    [NodeInfo ( category = "Action/Audio/",
                icon = "AudioSource",
                description = "Sets the default AudioClip to play in the AudioSource",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/AudioSource-clip.html")]
    public class SetAudioClip : ActionNode {

        /// <summary>
        /// A game object that has an audio source in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has an audio source in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new default clip.
        /// </summary>
    	[VariableInfo(tooltip = "The new default clip")]
        public ObjectVar newClip;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newClip = new ConcreteObjectVar();
        } 

        public override Status Update () {
            // Get audio
            var audio = gameObject.Value != null ? gameObject.Value.GetComponent<AudioSource>() : null;
            // Get clip
            var clip = newClip.Value as AudioClip; 

            // Validate members
            if (audio == null || clip == null)
                return Status.Error;

            // Set audio clip
            audio.clip = clip;
            return Status.Success;
        }
    }
}