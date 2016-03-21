//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    [NodeInfo ( category = "Sample/Mecanim/",
                icon = "Animator")]
    public class Jump : ActionNode {

        public string jumpTrigger;
        public string stateName;
        public int layer;
        public FloatVar forward;
        public FloatVar turn;

        float m_Timer;
        Animator m_Animator;

        public override void Reset () {
            jumpTrigger = "jump";
            stateName = "Jump";
            layer = 0;
            forward = new ConcreteFloatVar();
            turn = new ConcreteFloatVar();
        }

        public override void Awake () {
            m_Animator = self.GetComponent<Animator>();
        }

    	public override void Start () {
            self.GetComponent<Animator>().SetTrigger(jumpTrigger);
            m_Timer = Time.realtimeSinceStartup + .2f;
        }

        public override Status Update () {
            forward.Value = 1f;
            turn.Value = 0f;

            if (Time.realtimeSinceStartup > m_Timer && !m_Animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName))
                return Status.Success;
            else
                return Status.Running;
        }
    }
}