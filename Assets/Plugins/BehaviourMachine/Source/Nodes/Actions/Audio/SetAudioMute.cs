//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Un-/Mutes the AudioSource.
    /// </summary>
    [NodeInfo ( category = "Action/Audio/",
                icon = "AudioSource",
                description = "Un-/Mutes the AudioSource",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/AudioSource-mute.html")]
    public class SetAudioMute : ActionNode {

        /// <summary>
        /// A game object that has an audio source in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has an audio source in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// True, mute and sets the volume=0; False, Un-Mute and restore the original volume.
        /// </summary>
    	[VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "True, mute and sets the volume=0; Flase, Un-Mute and restore the original volume")]
        public BoolVar mute;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            mute = new ConcreteBoolVar();
        } 

        public override Status Update () {
            // Get audio
            var audio = gameObject.Value != null ? gameObject.Value.GetComponent<AudioSource>() : null;

            // Validate members
            if (audio == null)
                return Status.Error;

            audio.mute = mute.isNone ? !audio.mute : mute.Value;

            return Status.Success;
        }
    }
}