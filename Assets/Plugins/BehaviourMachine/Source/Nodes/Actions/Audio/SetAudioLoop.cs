//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Sets an AudioSource loop.
    /// </summary>
    [NodeInfo ( category = "Action/Audio/",
                icon = "AudioSource",
                description = "Sets an AudioSource loop",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/AudioSource-loop.html")]
    public class SetAudioLoop : ActionNode {

        /// <summary>
        /// A game object that has an audio source in it.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "A game object that has an audio source in it")]
        public GameObjectVar gameObject;

        /// <summary>
        /// True, to loop; false, to play once.
        /// </summary>
    	[VariableInfo(requiredField = false, nullLabel = "Toggle", tooltip = "True, to loop; false, to play once")]
        public BoolVar loop;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            loop = new ConcreteBoolVar();
        } 

        public override Status Update () {
            // Get audio
            var audio = gameObject.Value != null ? gameObject.Value.GetComponent<AudioSource>() : null;

            // Validate members
            if (audio == null)
                return Status.Error;

            // Set audio loop
            audio.loop = loop.isNone ? !audio.loop : loop.Value;
            return Status.Success;
        }
    }
}