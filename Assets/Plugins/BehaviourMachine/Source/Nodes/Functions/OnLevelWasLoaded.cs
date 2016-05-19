//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// OnLevelWasLoaded is called after a new level was loaded.
    /// </summary>
    [NodeInfo(  category = "Function/",
                icon = "Function",
                description = "OnLevelWasLoaded is called after a new level was loaded",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnLevelWasLoaded.html")]
    public class OnLevelWasLoaded : FunctionNode {

        /// <summary>
        /// Stores the index of the level that was loaded.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Store", canBeConstant = false, tooltip = "Stores the index of the level that was loaded")]
        public IntVar level;

        public override void OnEnable () {
            if (this.enabled) {
                this.blackboard.onLevelWasLoaded += LevelLoaded;
                m_Registered = true;
            }
        }

        public override void OnDisable () {
            this.blackboard.onLevelWasLoaded -= LevelLoaded;
            m_Registered = false;
        }

        /// <summary>
        /// Callback registered to get root.OnLevelWasLoaded events.
        /// </summary>
        void LevelLoaded (int level) {
            this.level.Value = level;

            // Tick children
            this.OnTick();
        }

        public override void Reset () {
            base.Reset();
            level = new ConcreteIntVar();
        }
    }
}