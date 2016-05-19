//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4 && !UNITY_4_1 && !UNITY_4_3 && !UNITY_4_5
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace BehaviourMachine {

    /// <summary> 
    /// Base class for the UnityEventStates.
    /// </summary>
    [AddComponentMenu("")]
    public class InternalUnityEventState : InternalStateBehaviour {

        [SerializeField]
        UnityEvent m_OnEnable;

        [SerializeField]
        UnityEvent m_Update;

        [SerializeField]
        UnityEvent m_OnDisable;

        public virtual void OnEnable () {
            m_OnEnable.Invoke();
        }

        public virtual void Update () {
            m_Update.Invoke();
        }

        public virtual void OnDisable () {
            m_OnDisable.Invoke();
        }
    }
}
#endif