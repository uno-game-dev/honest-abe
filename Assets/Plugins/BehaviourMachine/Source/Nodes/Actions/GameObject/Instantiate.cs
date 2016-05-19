//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Clones the game object "Original" and stores in "Store Clone".
    /// </summary>
    [NodeInfo(  category = "Action/GameObject/",
                icon = "PrefabNormal",
                description = "Clones the game object \"Original\" and stores in \"Store Clone\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Object.Instantiate.html")]
    public class Instantiate : ActionNode {

        /// <summary>
        /// An existing game object that you want to make a copy of. Usually a prefab.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "An existing game object that you want to make a copy of. Usually a prefab")]
        public GameObjectVar original;

        /// <summary>
        /// Position of the new object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Position of the new object")]
        public Vector3Var position;

        /// <summary>
        /// Orientation of the new object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "Orientation of the new object")]
        public QuaternionVar rotation;

        /// <summary>
        /// Stores the cloned game object.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the cloned game object")]
        public GameObjectVar storeClone;

        public override void Reset () {
            original = new ConcreteGameObjectVar(this.self);
            position = new ConcreteVector3Var();
            rotation = new ConcreteQuaternionVar();
            storeClone = new ConcreteGameObjectVar(this.self);
        }

        public override Status Update () {
            // Validate members
            if (original.Value == null)
                return Status.Error;

            var targetPosition = (!position.isNone) ? position.Value : original.Value.transform.position;
            var targetRotation = (!rotation.isNone) ? rotation.Value : original.Value.transform.rotation;

            if (storeClone != null)
                storeClone.Value = GameObject.Instantiate(original.Value, targetPosition, targetRotation) as GameObject;
            else
                GameObject.Instantiate(original.Value, targetPosition, targetRotation);

            return Status.Success;
        }
    }
}