//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets the "Game Object" name.
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Sets the \"Game Object\" name",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject-name.html")]
    public class SetName : ActionNode {

        /// <summary>
        /// The game object to change name.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to change name")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The new name.
        /// </summary>
        [VariableInfo(tooltip = "The new name")]
        public StringVar newName;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            newName = new ConcreteStringVar();
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null || newName.isNone)
                return Status.Error;

            gameObject.Value.name = newName.Value;
            return Status.Success;
        }
    }
}