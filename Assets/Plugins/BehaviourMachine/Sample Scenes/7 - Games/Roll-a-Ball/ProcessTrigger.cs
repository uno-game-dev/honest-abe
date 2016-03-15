using UnityEngine;
using System.Collections;
using BehaviourMachine;

namespace BehaviourMachine.Samples {
    
    public class ProcessTrigger : StateBehaviour {

        IntVar m_Count;
        AudioSource m_Audio;

        void Awake () {
            m_Count = blackboard.GetIntVar("Count");
            m_Audio = GetComponent<AudioSource>();
        }

        void OnEnable  () {
            blackboard.onTriggerEnter += StateOnTriggerEnter;
        }

        void OnDisable  () {
            blackboard.onTriggerEnter -= StateOnTriggerEnter;
        }

        void StateOnTriggerEnter (Collider other) {
            if (other.CompareTag("Finish")) {
                m_Audio.Play();
                other.gameObject.SetActive(false);
                m_Count.Value = m_Count.Value + 1;
            }
            SendEvent(GlobalBlackboard.FINISHED);
        }
    }
}