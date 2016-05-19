//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Component used to raise the OnAnimatorMove callback.
    /// </summary>
    [AddComponentMenu("")]
    public class AnimatorMoveCallback : MonoBehaviour {

    	/// <summary>
        /// Callback for processing animation movements for modifying root motion. 
        /// </summary>
        public event SimpleCallback onAnimatorMove;

        /// <summary>
        /// Unity callback for processing animation movements for modifying root motion.
        /// </summary>
        void OnAnimatorMove () {
            if (onAnimatorMove != null)
                onAnimatorMove();
        }
    }
}