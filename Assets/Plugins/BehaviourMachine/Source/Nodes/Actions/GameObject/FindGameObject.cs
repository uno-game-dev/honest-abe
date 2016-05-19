//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets a game object by name. Returns Failure if there is no game object in the scene with the specified name.
    /// </summary>
    [NodeInfo ( category = "Action/GameObject/",
                icon = "GameObject",
                description = "Gets a game object by name. Returns Failure if there is no game object in the scene with the specified name",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject.Find.html")]
    public class FindGameObject : ActionNode {

        /// <summary>
        /// The name of the game object to search for. You can also specify a path to a child object (eg. Player/Arm/Hand).
        /// </summary>
        [VariableInfo(tooltip = "The name of the game object to search for. You can also specify a path to a child object (eg. Player/Arm/Hand)")]
        public StringVar gameObjectName;

        /// <summary>
        /// Store the game object.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the game object")]
        public GameObjectVar storeGameObject;

        public override void Reset () {
            gameObjectName = new ConcreteStringVar();
            storeGameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Validate members
            if (gameObjectName.isNone || storeGameObject.isNone)
                return Status.Error;

            storeGameObject.Value = GameObject.Find(gameObjectName.Value);

            return storeGameObject.Value != null ? Status.Success : Status.Failure;
        }
    }
}