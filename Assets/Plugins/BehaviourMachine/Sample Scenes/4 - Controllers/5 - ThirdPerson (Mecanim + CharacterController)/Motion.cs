//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    [NodeInfo ( category = "Sample/Mecanim/",
                icon = "CharacterController")]
    public class Motion : ActionNode {

        public string forwardParameter;
        public FloatVar forward;
        public string turnParameter;
        public FloatVar turn;
        public float rotationSpeed;
        public FloatVar speed;

        Animator m_Animator;
        Transform m_Transform;
        CharacterController m_CharacterController;

        public override void Reset () {
            forwardParameter = "forward";
            forward = new ConcreteFloatVar();
            turnParameter = "turn";
            turn = new ConcreteFloatVar();
            rotationSpeed = 90f;
            speed = new ConcreteFloatVar();
        }

        public override void Awake () {
            m_Animator = self.GetComponent<Animator>();
            m_Transform = self.transform;
            m_CharacterController = self.GetComponent<CharacterController>();
        }

        public override Status Update () {
            // Update the Animator
            m_Animator.SetFloat(forwardParameter, forward.Value);
            m_Animator.SetFloat(turnParameter, turn.Value);

            // Rotate
            m_Transform.Rotate(0f, turn.Value * rotationSpeed * owner.deltaTime, 0f);

            // Move
            Vector3 direction = m_Transform.TransformDirection(0f, 0f, Mathf.Max(forward.Value, 0f));
            m_CharacterController.SimpleMove(direction * speed.Value);

            return Status.Success;
        }
    }
}