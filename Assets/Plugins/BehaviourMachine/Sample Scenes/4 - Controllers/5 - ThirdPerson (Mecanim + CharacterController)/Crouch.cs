//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    [NodeInfo ( category = "Sample/Mecanim/",
                icon = "Animator")]
    public class Crouch : ActionNode {

        public string crouchParameter;
        public string crouchButton;
        public FloatVar speed;
        public float crouchSpeed;
        public float standUpSpeed;

        Animator m_Animator;

        public override void Reset () {
            crouchParameter = "crouching";
            crouchButton = "Fire1";
            speed = new ConcreteFloatVar();
            crouchSpeed = .7f;
            standUpSpeed = 5f;
        }

        public override void Awake () {
            m_Animator = self.GetComponent<Animator>();
        }

    	public override void Start () {
            m_Animator.SetBool(crouchParameter, true);
            speed.Value = crouchSpeed;
        }

        public override Status Update () {
            if (Input.GetButtonUp(crouchButton))
                return Status.Success;
            else
                return Status.Running;
        }

        public override void End () {
            m_Animator.SetBool(crouchParameter, false);
            speed.Value = standUpSpeed;
        }
    }
}