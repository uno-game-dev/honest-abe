//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Update your nodes and wait 'Frequency' seconds before updating again. Very useful for artifical intelligence.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "Update your nodes and wait 'Frequency' seconds before updating again. Very useful for artifical intelligence",
                url = "http://docs.unity3d.com/Manual/Coroutines.html")]
    public class Coroutine : FunctionNode {


        public FloatVar frequence;

        [System.NonSerialized]
        float m_Timer;

        #region ActionNode Callbacks
        public override void Reset () {
            base.Reset();
            frequence = .2f;
        }

        public override void OnEnable () {
            if (this.enabled) {
                InternalGlobalBlackboard.update += Routine;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            InternalGlobalBlackboard.update -= Routine;
            m_Registered = false;
        }
        #endregion ActionNode Callbacks


        void Routine () {
            // Get the frequence
            float freq = frequence.Value > 0f ? frequence.Value : 0f;
            // Get the deltaTime
            float dt = owner.deltaTime;
            // Update timer
            m_Timer += dt;

            // Its time to update the tree?
            if (m_Timer >= freq) {
                owner.deltaTimeAmount += m_Timer - dt;
                this.OnTick();
                owner.deltaTimeAmount -= m_Timer - dt;
                m_Timer = frequence.Value > 0f ? m_Timer % freq : 0f;
            }
        }
    }
}
