//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Pauses playing the clip.
    /// </summary>
    [NodeInfo ( category = "Action/Audio/",
                icon = "AudioSource",
                description = "Pauses playing the clip",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/AudioSource.Pause.html")]
    public class AudioPause : ActionNode {

        /// <summary>
        /// A game object that has an audio source in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has an audio source in it")]
        public GameObjectVar gameObject;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
        } 

        public override Status Update () {
            // Get audio
            var audio = gameObject.Value != null ? gameObject.Value.GetComponent<AudioSource>() : null;

            // Validate members
            if (audio == null)
                return Status.Error;

            audio.Pause();

            return Status.Success;
        }
    }
}