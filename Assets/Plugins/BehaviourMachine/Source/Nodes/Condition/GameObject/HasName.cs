//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// The "Game Object" has the same name as "Name"?
    /// </summary>
    [NodeInfo(  category = "Condition/GameObject/",
                icon = "GameObject",
                description = "The \"Game Object\" has the same name as \"Name\"?",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Object-name.html")]
    public class HasName : ConditionNode {

    	/// <summary>
        /// The game object to test the tag.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to test the tag")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The name to test for.
        /// </summary>
        [VariableInfo(tooltip = "The name to test for")]
        public StringVar Name;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
            Name = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members?
            if  (gameObject.Value == null || Name.isNone)
                return Status.Error;

            if (gameObject.Value.name == Name.Value) {
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