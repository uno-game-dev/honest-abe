//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// The "Game Object" is in "Layer"?
    /// </summary>
    [NodeInfo(  category = "Condition/GameObject/",
                icon = "GameObject",
                description = "The \"Game Object\" is in \"Layer\"?",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject-layer.html")]
    public class HasLayer : ConditionNode {

    	/// <summary>
        /// The game object to test the layer.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to test the layer")]
        public GameObjectVar gameObject;

        /// <summary>
        /// The layer to test for.
        /// </summary>
        [VariableInfo(tooltip = "The layer to test for")]
        public IntVar layer;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
            layer = new ConcreteIntVar();
        }

        public override Status Update () {
            // Validate members?
            if  (gameObject.Value == null || layer.isNone)
                return Status.Error;

            if (gameObject.Value.layer == layer.Value) {
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