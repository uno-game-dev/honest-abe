//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// The \"Game Object\" is active?
    /// </summary>
    [NodeInfo(  category = "Condition/GameObject/",
                icon = "GameObject",
                description = "The \"Game Object\" is active?",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject-activeSelf.html")]
    public class IsActive : ConditionNode {

    	/// <summary>
        /// The game object to test activeSelf.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to test activeSelf")]
        public GameObjectVar gameObject;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Validate members
            if  (gameObject.Value == null)
                return Status.Error;

            if (gameObject.Value.activeSelf) {
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