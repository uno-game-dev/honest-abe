//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Plays an animation after previous animations have finished playing.
    /// </summary>
    [NodeInfo(  category = "Action/Animation/",
                icon = "Animation", 
                description = "Plays an animation after previous animations has finished playing",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Animation.PlayQueued.html")]
    public class PlayQueuedAnimation : ActionNode {

        /// <summary>
        /// The game object that has an Animation component.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has an Animation component")]
        public GameObjectVar gameObject;

        /// <summary>
        /// You can play a specific animation by name.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Default Animation", tooltip = "You can play a specific animation by name")]
        public StringVar animationName;

        /// <summary>
        /// CompleteOthers: Animation will only start once all other animations have stopped playing.
        /// PlayNow: The animation will start playing immediately on a duplicated animation state.
        /// </summary>
        [Tooltip("- CompleteOthers: Animation will only start once all other animations have stopped playing\n- PlayNow: The animation will start playing immediately on a duplicated animation state")]
        public QueueMode queue =  QueueMode.CompleteOthers;
        
        /// <summary>
        /// StopSameLayer: all animations in the same layer will be stopped.
        /// StopAll: all animations currently playing will be stopped.
        /// </summary>
        [Tooltip("- StopSameLayer: all animations in the same layer will be stopped\n- StopAll: all animations currently playing will be stopped")]
        public PlayMode playMode = PlayMode.StopSameLayer;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            animationName = new ConcreteStringVar();
            queue = QueueMode.CompleteOthers;
            playMode = PlayMode.StopSameLayer;
        }

        public override Status Update () {
            // Get the gameObject animation
            var animation = gameObject.Value != null ? gameObject.Value.GetComponent<Animation>() : null;

            if (animation == null || animationName.isNone)
                return Status.Error;
            
            animation.PlayQueued(animationName.Value, queue, playMode);

            return Status.Success;
        }
    }
}