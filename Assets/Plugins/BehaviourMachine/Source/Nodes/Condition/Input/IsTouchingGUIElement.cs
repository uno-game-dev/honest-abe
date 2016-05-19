//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Returns Success if the player is touching a GUI element (GUITexture or GUIText).
    /// </summary>
    [NodeInfo(  category = "Condition/Input/",
                icon = "BuildSettings.iPhone.Small",
                description = "Returns Success if the player is touching a GUI element (GUITexture or GUIText)")]
    public class IsTouchingGUIElement : ConditionNode {

        /// <summary>
        /// The game object that has the GUITexture or GUIText to test.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object that has the GUITexture or GUIText to test")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Only use the touch with this fingerId.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Any Id", tooltip = "Only use the touch with this fingerId")]
    	public IntVar fingerId;

        /// <summary>
        /// Only test touches in this phase.
        /// </summary>
        [Tooltip("Only test touches in this phase")]
        public TouchPhase touchPhase;

        /// <summary>
        /// Store the fingerId of the touch, used only if the "Finger Id" parameter is Any Id.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, tooltip = "Store the fingerId of the touch, used only if the \"Finger Id\" parameter is Any Id")]
        public IntVar storeFingerId;

        /// <summary>
        /// Store the position of the touch in screen coordinates.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, tooltip = "Store the position of the touch in screen coordinates")]
        public Vector3Var storeTouchPos;

        [System.NonSerialized]
        GUIElement m_GUIElement = null;

        public override void Reset () {
            base.Reset();

            gameObject = new ConcreteGameObjectVar(this.self);
            fingerId = new ConcreteIntVar();
            touchPhase = TouchPhase.Began;
            storeFingerId = new ConcreteIntVar();
            storeTouchPos = new ConcreteVector3Var();
        }

        public override Status Update () {
            // Is there at least one touch?
            if (Input.touchCount < 0)
                return Status.Failure;

            // Get the GUIElement
            if (m_GUIElement == null || m_GUIElement.gameObject != gameObject.Value)
                m_GUIElement = gameObject.Value != null ? gameObject.Value.GetComponent<GUIElement>() : null;

            // Validate GUIElement
            if (m_GUIElement == null)
                return Status.Error;

            // Get the touches
            var touches = Input.touches;

            // Any touch?
            if (fingerId.isNone) {
                for (int i = 0; i < touches.Length; i++) {
                    // Get the touch
                    var touch = touches[i];
                    // Test the touch phase and the gui element
                    if (touch.phase == touchPhase && m_GUIElement.HitTest(touch.position)) {
                        storeFingerId.Value = touch.fingerId;
                        storeTouchPos.Value = touch.position;

                        // Send event?
                        if (onSuccess.id != 0)
                            owner.root.SendEvent(onSuccess.id);

                        return Status.Success;
                    }
                }
            }
            else {
                for (int i = 0; i < touches.Length; i++) {
                    // Get the touch
                    var touch = touches[i];
                    // Test the fingerId, the touch phase and the gui element
                    if (touch.fingerId == fingerId.Value && touch.phase == touchPhase && m_GUIElement.HitTest(touch.position)) {
                        storeTouchPos.Value = touch.position;

                        // Send event?
                        if (onSuccess.id != 0)
                            owner.root.SendEvent(onSuccess.id);

                        return Status.Success;
                    }
                }
            }

            return Status.Failure;
        }
    }
}