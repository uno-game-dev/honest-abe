//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Makes "Game Object" to not be destroyed automatically when loading a new scene.
    /// </summary>
    [NodeInfo ( category = "Action/GameObject/",
                icon = "GameObject",
                description = "Makes \"Game Object\" to not be destroyed automatically when loading a new scene",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Object.DontDestroyOnLoad.html")]
    public class DontDestroyOnLoad : ActionNode {
        /// <summary>
        /// The game object to not be destroyed.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to not be destroyed")]
        public GameObjectVar gameObject;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null)
                return Status.Error;

            Object.DontDestroyOnLoad(gameObject.Value);

            return Status.Success;
        }
    }
}