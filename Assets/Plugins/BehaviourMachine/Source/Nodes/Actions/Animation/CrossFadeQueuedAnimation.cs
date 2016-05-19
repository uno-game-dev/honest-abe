//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Cross fades an animation after previous animations have finished playing.
    /// </summary>
    [NodeInfo(  category = "Action/Animation/",
                icon = "Animation", 
                description = "Cross fades an animation after previous animations has finished playing",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Animation.CrossFadeQueued.html")]
    public class CrossFadeQueuedAnimation : ActionNode {

        /// <summary>
        /// The game object that has an Animation component.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has an Animation component")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The name of the animation to cross fade.
        /// </summary>
        [VariableInfo(tooltip = "The name of the animation to cross fade")]
        public StringVar animationName;
        
        /// <summary>
        /// The time in seconds to complete the fade.
        /// </summary>
        [VariableInfo(tooltip = "The time in seconds to complete the fade")]
        public FloatVar fadeLength;
        
        /// <summary>
        /// CompleteOthers: Animation will only start once all other animations have stopped playing.
        /// PlayNow: The animation will start playing immediately on a duplicated animation state.
        /// </summary>
        [Tooltip("- CompleteOthers: Animation will only start once all other animations have stopped playing\n- PlayNow: The animation will start playing immediately on a duplicated animation state")]
        public QueueMode queue =  QueueMode.CompleteOthers;

        /// <summary>
        /// StopSameLayer: Animations in the same layer as animation will be faded out while Animation Name is faded in.
        /// StopAll: All animations will be faded out while Animation Name is faded in.
        /// </summary>
        [Tooltip("- StopSameLayer: Animations in the same layer as animation will be faded out while Animation Name is faded in\n- StopAll: All animations will be faded out while Animation Name is faded in")]
        public PlayMode playMode = PlayMode.StopSameLayer;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            animationName = new ConcreteStringVar();
            fadeLength = .3f;
            queue = QueueMode.CompleteOthers;
            playMode = PlayMode.StopSameLayer;
        }

        public override Status Update () {
            // Get the animation
            var animation = gameObject.Value != null ? gameObject.Value.GetComponent<Animation>() : null;

            if (animation == null || animationName.isNone)
                return Status.Error;
            
            animation.CrossFadeQueued(animationName.Value, fadeLength.Value, queue, playMode);

            return Status.Success;
        }
    }
}