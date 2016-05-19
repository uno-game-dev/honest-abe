//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Play an animation in the Target. The animation will be played abruptly without any blending. Returns Failure if animation can't be played.
    /// </summary>
    [NodeInfo(  category = "Action/Animation/",
                icon = "Animation", 
                description = "Play an animation in the Target. The animation will be played abruptly without any blending. Returns Failure if animation can't be played",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Animation.Play.html")]
    public class PlayAnimation : ActionNode {

        /// <summary>
        /// The game object that has an animation.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has an animation")]
        public GameObjectVar gameObject;

        /// <summary>
        /// You can play a specific animation by name.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Default Animation", tooltip = "You can play a specific animation by name")]
        public StringVar animationName;
        
        /// <summary>
        /// StopSameLayer: all animations in the same layer will be stopped.
        /// StopAll: all animations currently playing will be stopped.
        /// </summary>
        [Tooltip("- StopSameLayer: all animations in the same layer will be stopped\n- StopAll: all animations currently playing will be stopped")]
        public PlayMode playMode = PlayMode.StopSameLayer;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            animationName = new ConcreteStringVar();
            playMode = PlayMode.StopSameLayer;
        }

        public override Status Update () {
            // Get the animation
            var animation = gameObject.Value != null ? gameObject.Value.GetComponent<Animation>() : null;

            if (animation == null)
                return Status.Error;
            
            // Use the default animation?
            if (animationName.isNone) {
                if (!animation.Play(playMode))
                    return Status.Failure;
            }
            else if (!animation.Play(animationName.Value, playMode))
                return Status.Failure;

            return Status.Success;
        }
    }
}