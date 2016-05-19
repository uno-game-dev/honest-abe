//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Tick its child and waits an interval of time to tick its child again.
    /// </summary>
    [NodeInfo(  category = "Decorator/",
                icon = "UnityEditor.AnimationWindow",
                description = "Tick its child and waits an interval of time to tick its child again")]
    public class CoolDown : DecoratorNode {

        /// <summary>
        /// The interval time in seconds to wait before execute the child again.
        /// </summary>
        [VariableInfo(tooltip = "The interval time in seconds to wait before execute the child again")]
        public FloatVar intervalTime;

        [System.NonSerialized]
        float m_Timer = 0f;

        public override void Reset () {
            intervalTime = 1f;
        }

        public override Status Update () {
            // Validate members
            if (intervalTime.isNone || child == null)
                return Status.Error;

            if (owner.ignoreTimeScale) {
               if (Time.realtimeSinceStartup > m_Timer) {
                    child.OnTick();
                    m_Timer = Time.realtimeSinceStartup + intervalTime.Value;
                    return child.status;
                }
            }
            else {
                if (Time.time > m_Timer) {
                    child.OnTick();
                    m_Timer = Time.time + intervalTime.Value;
                    return child.status;
                }
            }

            return Status.Running;
        }
    }
}