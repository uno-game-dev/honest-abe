//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Adds a value to a IntVar.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Math",
                description = "Multiply \"Variable\" by \"Multiply By\"")]
    public class IntAdd : ActionNode {

    	/// <summary>
        /// The variable to change value.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "The IntVar to change value")]
        public IntVar variable;

        /// <summary>
        /// Add factor.
        /// </summary>
        [VariableInfo(tooltip = "Add factor")]
        public IntVar addBy;

        /// <summary>
        /// If true the operation will be applied every second; otherwise the operation will be applied instantaneously
        /// </summary>
        [Tooltip("If true the operation will be applied every second; otherwise the operation will be applied instantaneously")]
        public bool perSecond = false;

        [System.NonSerialized]
        float m_Timer = 0f;

        public override void Reset () {
            variable = new ConcreteIntVar();
            addBy = new ConcreteIntVar();
            perSecond = false;
        }

        public override Status Update () {
            // Validate members
            if (variable.isNone || addBy.isNone)
                return Status.Error;

            if (perSecond) {
                m_Timer += owner.deltaTime;
                if (m_Timer >= 1f) {
                    m_Timer -= 1f;
                    variable.Value += addBy.Value;
                }
            }
            else
                variable.Value += addBy.Value;

            return Status.Success;
        }
    }
}