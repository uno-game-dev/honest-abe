//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Plays the clip with an optional certain delay.
    /// </summary>
    [NodeInfo ( category = "Action/Audio/",
                icon = "AudioSource",
                description = "Plays the clip with an optional certain delay",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/AudioSource.Play.html")]
    public class AudioPlay : ActionNode {

        /// <summary>
        /// A game object that has an audio source in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has an audio source in it")]
        public GameObjectVar gameObject;

        #if !UNITY_4_0_0
        /// <summary>
        /// Delay time specified in seconds.
        /// </summary>
    	[VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Delay time specified in seconds")]
        public FloatVar delay;
        #endif

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            #if !UNITY_4_0_0
            delay = new ConcreteFloatVar();
            #endif
        } 

        public override Status Update () {
            // Get audio
            var audio = gameObject.Value != null ? gameObject.Value.GetComponent<AudioSource>() : null;

            // Validate member
            if (audio == null)
                return Status.Error;

            #if !UNITY_4_0_0
            // Use delay?
            if (delay.isNone)
                audio.Play();
            else
                audio.PlayDelayed(delay.Value);
            #else
            audio.Play();
            #endif

            return Status.Success;
        }
    }
}