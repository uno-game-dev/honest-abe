//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets a game object by tag. Returns Failure if there is no game object in the scene with the specified tag.
    /// </summary>
    [NodeInfo ( category = "Action/GameObject/",
                icon = "GameObject",
                description = "Gets a game object by tag. Returns Failure if there is no game object in the scene with the specified tag",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject.FindGameObjectsWithTag.html")]
    public class FindGameObjectByTag : ActionNode {

        /// <summary>
        /// The tag to search for.
        /// </summary>
        [VariableInfo(tooltip = "The tag to search for")]
        public StringVar tag;

        /// <summary>
        /// The game object that has the supplied tag.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The game object that has the supplied tag")]
        public GameObjectVar storeGameObject;

        public override void Reset () {
            tag = new ConcreteStringVar();
            storeGameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Validate members
            if (tag.isNone || storeGameObject.isNone)
                return Status.Error;

            storeGameObject.Value = GameObject.FindWithTag(tag.Value);

            return Status.Success;
        }
    }
}