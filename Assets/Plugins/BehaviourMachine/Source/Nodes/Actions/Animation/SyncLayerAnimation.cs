//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Synchronizes playback speed of all animations in the supplied layer.
    /// <summary>
    [NodeInfo(  category = "Action/Animation/",
                icon = "Animation", 
                description = "Synchronizes playback speed of all animations in the supplied layer",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Animation.SyncLayer.html")]
    public class SyncLayerAnimation : ActionNode {

        /// <summary>
        /// The game object that has an Animation component.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has an Animation component")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The layer to sync.
        /// <summary>
        [VariableInfo(tooltip = "The layer to sync")]
        public IntVar layerToSync;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            layerToSync = new ConcreteIntVar();
        }
        
        public override Status Update () {
            // Get the animation
            var animation = gameObject.Value != null ? gameObject.Value.GetComponent<Animation>() : null;

            // Validate members
            if (animation == null || layerToSync.isNone)
                return Status.Error;
            
            // Sync layer
            animation.SyncLayer(layerToSync.Value);
            return Status.Success;
        }
    }
}