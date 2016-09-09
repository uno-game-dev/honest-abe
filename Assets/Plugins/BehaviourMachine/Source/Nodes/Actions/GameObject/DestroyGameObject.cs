//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Destroys a game object.
    /// <summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "GameObject",
                description = "Destroys a game object",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Object.Destroy.html")]
    public class DestroyGameObject : ActionNode {

        /// <summary>
        /// The game object to be destroyed.
        /// <summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to be destroyed")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Optionally delays object destruction in \"Delay Time\" seconds.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Optionally delays object destruction in \"Delay Time\" seconds")]
        public FloatVar delayTime;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            delayTime = new ConcreteFloatVar();
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null)
                return Status.Error;

            if (delayTime.isNone)
                GameObject.Destroy(gameObject.Value);
            else
                GameObject.Destroy(gameObject.Value, delayTime.Value);

            return Status.Success;
        }
    }
}