//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnAnimatorMove is called for processing animation movements for modifying root motion. The Animator.rootMotion property will be ignored when using this node.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnAnimatorMove is called for processing animation movements for modifying root motion. The Animator.rootMotion property will be ignored when using this node",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnAnimatorMove.html")]
    public class OnAnimatorMove : FunctionNode {

        [System.NonSerialized]
        AnimatorMoveCallback m_AnimatorMoveCallback;

        public override void OnEnable () {
            if (this.enabled) {
                if (m_AnimatorMoveCallback == null)
                    m_AnimatorMoveCallback = this.self.AddComponent<AnimatorMoveCallback>();

                m_AnimatorMoveCallback.onAnimatorMove += OnTick;

                m_Registered = true;
            }
        }

        public override void OnDisable () {
            m_AnimatorMoveCallback.onAnimatorMove -= OnTick;

            m_Registered = false;
        }


    }
}