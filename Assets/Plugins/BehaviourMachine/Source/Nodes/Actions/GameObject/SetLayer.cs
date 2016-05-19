//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Game Object" layer.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Sets the \"Game Object\" layer",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject-layer.html")]
    public class SetLayer : ActionNode {

        /// <summary>
        /// The game object to change layer.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to change layer")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new layer.
        /// </summary>
        [VariableInfo(tooltip = "The new layer")]
        public IntVar newLayer;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newLayer = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null || newLayer.isNone)
                return Status.Error;

            gameObject.Value.layer = newLayer.Value;

            return Status.Success;
        }
    }
}