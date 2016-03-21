using UnityEngine;
using System.Collections;
using BehaviourMachine;

namespace BehaviourMachine.Samples {
    public class Move : StateBehaviour {

        public float speed;
        private Rigidbody m_Rigidbody;

        void Awake () {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate () {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

            m_Rigidbody.AddForce(movement * speed * Time.deltaTime);
        }
    }
}