//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Game Object" tag.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "FilterByLabel",
                description = "Sets the \"Game Object\" tag",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject-tag.html")]
    public class SetTag : ActionNode {

        /// <summary>
        /// The game object to change tag.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to change tag")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new tag.
        /// </summary>
        [VariableInfo(tooltip = "The new tag")]
        public StringVar newTag;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newTag = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null || newTag.isNone)
                return Status.Error;

            gameObject.Value.tag = newTag.Value;

            return Status.Success;
        }
    }
}