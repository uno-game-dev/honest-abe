//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Blends the animation "Animation Name" towards "Target Weight" over the next time seconds.
    /// </summary>
    [NodeInfo(  category = "Action/Animation/",
                icon = "Animation", 
                description = "Blends the animation \"Animation Name\" towards \"Target Weight\" over the next time seconds",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Animation.Blend.html")]
    public class BlendAnimation : ActionNode {

        /// <summary>
        /// The game object that has an Animation component.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has an Animation component")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The animation name to blend.
        /// </summary>
        [VariableInfo (tooltip = "The animation name to blend")]
        public StringVar animationName;
        
        /// <summary>
        /// The weight to blend the animation.
        /// </summary>
        [VariableInfo(tooltip = "The weight to blend the animation")]
        public FloatVar targetWeight;
        
        /// <summary>
        /// The time in seconds to complete the fade.
        /// </summary>
        [VariableInfo(tooltip = "The time in seconds to complete the fade")]
        public FloatVar targetFadeLength;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            animationName = new ConcreteStringVar();
            targetWeight = 1f;
            targetFadeLength = .3f;
        }

        public override Status Update () {
            // Get the animation
            var animation = gameObject.Value != null ? gameObject.Value.GetComponent<Animation>() : null;

            // Validate members
            if (animation == null || animationName.isNone || targetWeight.isNone || targetFadeLength.isNone)
                return Status.Error;
            
            animation.Blend(animationName.Value, targetWeight.Value, targetFadeLength.Value);
            return Status.Success;
        }
    }
}