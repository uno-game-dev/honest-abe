//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Is the "Game Object" tagged with "Tag"?
    /// </summary>
    [NodeInfo(  category = "Condition/GameObject/",
                icon = "GameObject",
                description = "Is the \"Game Object\" tagged with \"Tag\"?",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject-tag.html")]
    public class HasTag : ConditionNode {

    	/// <summary>
        /// The game object to test the tag.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to test the tag")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The tag to test for.
        /// </summary>
        [VariableInfo(tooltip = "The tag to test for")]
        public StringVar tag;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
            tag = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members
            if  (gameObject.Value == null || tag.isNone)
                return Status.Error;

            if (gameObject.Value.CompareTag(tag.Value)) {
                // Send event?
                if (onSuccess.id != 0)
                    owner.root.SendEvent(onSuccess.id);

                return Status.Success;
            }
            else
                return Status.Failure;
        }
    }
}