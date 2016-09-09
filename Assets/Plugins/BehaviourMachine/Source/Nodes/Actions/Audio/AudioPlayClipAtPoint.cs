//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Plays an "Audio Clip" at a given "Position" in world space.
    /// </summary>
    [NodeInfo ( category = "Action/Audio/",
                icon = "AudioClip",
                description = "Plays an \"Audio Clip\" at a given \"Position\" in world space",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/AudioSource.PlayClipAtPoint.html")]
    public class AudioPlayClipAtPoint : ActionNode {

        /// <summary>
        /// The clip being played.
        /// </summary>
    	[VariableInfo(tooltip = "The clip being played")]
        public ObjectVar audioClip;

        /// <summary>
        /// Position in world space from which sound originates.
        /// </summary>
        [VariableInfo(tooltip = "Position in world space from which sound originates")]
        public Vector3Var position;

        /// <summary>
        /// The scale of the volume (0-1).
        /// </summary>
        [VariableInfo(tooltip = "The scale of the volume (0-1)")]
        public FloatVar volumeScale;

        public override void Reset () {
            audioClip = new ConcreteObjectVar();
            position = new ConcreteVector3Var();
            volumeScale = 1f;
        } 

        public override Status Update () {
            // Get clip
            var clip = audioClip.Value as AudioClip;

            // Validate members
            if (clip == null || position.isNone)
                return Status.Error;

            AudioSource.PlayClipAtPoint(clip, position.Value, volumeScale.Value);

            return Status.Success;
        }
    }
}