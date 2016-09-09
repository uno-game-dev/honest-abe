//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {
    /// <summary>
    /// Uses any MonoBehaviour as a state.
    /// Just drag the MonoBehaviour you wish to use as state in the "Mono Behaviour" property.
    /// </summary>
    [AddComponentMenu("")]
    public class InternalMonoState : InternalStateBehaviour {

        #region Members
        [SerializeField]
        GameObject m_Target;
        [MonoBehaviourAttribute]
        [SerializeField]
        protected MonoBehaviour m_MonoBehaviour;
        #endregion Members

        #region Properties
        /// <summary>
        /// The target GameObject.
        /// </summary>
        public GameObject target {
            get {return m_Target;}

            set {
                m_Target = value;
                m_MonoBehaviour = value != null ? value.GetComponent<MonoBehaviour>() : null;
                this.OnValidate();
            }
        }

        /// <summary>
        /// T`he MonoBehaviour in this state.
        /// </summary>
        public MonoBehaviour monoBehaviour {
            get {return m_MonoBehaviour;} 

            set {
                if (m_MonoBehaviour != value) {
                    // Update target if the new MonoBehaviour is not null
                    if (value != null && (m_Target == null || m_Target != value.gameObject)) {
                        m_Target = value.gameObject;
                    }
                    m_MonoBehaviour = value;
                    this.OnValidate();
                }
            }
        }
        #endregion Properties

        #region Unity Callbacks
        /// <summary> 
        /// Unity callback called when the object becomes enabled and active.
        /// Enables the MonoBehaviour.
        /// </summary>
        void OnEnable () {
            if (m_MonoBehaviour != null)
                m_MonoBehaviour.enabled = true;
            else
                Print.LogError("No MonoBehaviour in " + stateName + " (MonoState)", this);
        }

        /// <summary> 
        /// Unity callback called when the behaviour becomes disabled or inactive.
        /// Disables the MonoBehaviour.
        /// </summary>
        void OnDisable () {
            if (m_MonoBehaviour != null)
                m_MonoBehaviour.enabled = false;
            else
                Print.LogError("No MonoBehaviour in " + stateName + " (MonoState)", this);
        }

        /// <summary> 
        /// Unity callback called when the user hits the Reset button in the Inspector's context menu or when adding the component the first time (Editor only).
        /// Update the MonoBehaviour and the enabled parameter of the MonoBehaviour.
        /// </summary>
        public override void OnValidate () {
            base.OnValidate();

            if (m_MonoBehaviour != null) {
                if (m_MonoBehaviour.gameObject != m_Target)
                    m_MonoBehaviour = null;
                else
                    m_MonoBehaviour.enabled = this.enabled;
            }
        }
        #endregion Unity Callbacks
    }
}
