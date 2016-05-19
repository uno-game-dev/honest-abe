//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    ///  <summary>
    /// Tick this node and the game object will be draggable in the scene.
    ///  </summary>
    [NodeInfo(  category = "Action/Transform/",
                icon = "Transform",
                description = "Tick this node and the game object will be draggable in the scene.")]
    public class Draggable : ActionNode {

        [System.NonSerialized]
        float m_ScreenPointZ;
        [System.NonSerialized]
        Vector3 m_Offset;
        [System.NonSerialized]
        Transform m_Transform;
        [System.NonSerialized]
        bool m_MouseDown = false;

        Transform transform {
            get {
                if (m_Transform == null)
                    m_Transform = self.transform;
                return m_Transform;
            }
        }

        public override void Start () {
            blackboard.onMouseDown += OnMouseDown;
            blackboard.onMouseDrag += OnMouseDrag;
            blackboard.onMouseUp += OnMouseUp;

            // Workaround to call OnMouseDown even if the node is not registered yet
            if (this.root is OnMouseDrag || this.root is OnMouseDown)
                OnMouseDown();
        }

        public override void End () {
            blackboard.onMouseDown -= OnMouseDown;
            blackboard.onMouseDrag -= OnMouseDrag;
            blackboard.onMouseUp -= OnMouseUp;
            m_MouseDown = false;
        }

    	public override Status Update () {
            return Status.Running;
        }

        #region Callbacks
        void OnMouseDown () {
            m_ScreenPointZ = Camera.main.WorldToScreenPoint(transform.position).z;
            m_Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_ScreenPointZ));
            m_MouseDown = true;
        }

        void OnMouseDrag () {
            if (m_MouseDown) {
                var currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_ScreenPointZ);
                transform.position = Camera.main.ScreenToWorldPoint(currentScreenPoint) + m_Offset;
            }
        }

        void OnMouseUp () {
            m_MouseDown = false;
        }
        #endregion Callbacks
    }
}