//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Stops a single or all playing animations in the "Game Object".
    /// </summary>
    [NodeInfo(  category = "Action/Animation/",
                icon = "Animation", 
                description = "Stops a single or all playing animations in the \"Game Object\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Animation.Stop.html")]
    public class StopAnimation : ActionNode {

        /// <summary>
        /// The game object that has an Animation component.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has an Animation component")]
        public GameObjectVar gameObject;

        /// <summary>
        /// You can stop a specific animation by name.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Stop All Animations", tooltip = "You can stop a specific animation by name")]
        public StringVar animationName;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            animationName = new ConcreteStringVar();
        }

        public override Status Update () {
            // Get the animation
            var animation = gameObject.Value != null ? gameObject.Value.GetComponent<Animation>() : null;

            // Validate members
            if (animation == null)
                return Status.Error;
            
            // Stop animation
            if (animationName.isNone)
                animation.Stop();
            else
                animation.Stop(animationName.Value);
                
            return Status.Success;
        }
    }
}