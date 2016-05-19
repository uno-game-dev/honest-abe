//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

	/// <summary>
    /// Store an Animation's float paramater.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Animator")]
    public class FloatParameter : FloatVar {

        /// <summary>
        /// The target Animator.
        /// </summary>
        [Tooltip("The target Animator")]
        public Animator animator;

        /// <summary>
        /// The parameter name.
        /// </summary>
        [Tooltip("The parameter name")]
        public string parameter;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override float Value {
            get {
                return animator != null ? animator.GetFloat(parameter) : 0f;
            } 
            set {
                if (animator != null) 
                    animator.SetFloat(this.parameter, value);
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public FloatParameter () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public FloatParameter (GameObject self) : base () {
            this.parameter = "New Float";
            this.animator = self.GetComponent<Animator>();
        }

        /// <summary>
        /// User-defined conversion from FloatParameter to float
        /// </summary>
        public static implicit operator float (FloatParameter variable) {
            return variable.Value;
        }
    }

    /// <summary>
    /// Store an Animation's int paramater.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Animator")]
    public class IntParameter : IntVar {

        /// <summary>
        /// The target Animator.
        /// </summary>
        [Tooltip("The target Animator")]
        public Animator animator;

        /// <summary>
        /// The parameter name.
        /// </summary>
        [Tooltip("The parameter name")]
        public string parameter;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override int Value {
            get {
                return animator != null ? animator.GetInteger(parameter) : 0;
            } 
            set {
                if (animator != null) 
                    animator.SetInteger(this.parameter, value);
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public IntParameter () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public IntParameter (GameObject self) : base () {
            this.parameter = "New Int";
            this.animator = self.GetComponent<Animator>();
        }

        /// <summary>
        /// User-defined conversion from IntParameter to int
        /// </summary>
        public static implicit operator int (IntParameter variable) {
            return variable.Value;
        }
    }

    /// <summary>
    /// Store an Animation's int paramater.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Animator")]
    public class BoolParameter : BoolVar {

        /// <summary>
        /// The target Animator.
        /// </summary>
        [Tooltip("The target Animator")]
        public Animator animator;

        /// <summary>
        /// The parameter name.
        /// </summary>
        [Tooltip("The parameter name")]
        public string parameter;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override bool Value {
            get {
                return animator != null ? animator.GetBool(parameter) : false;
            } 
            set {
                if (animator != null) 
                    animator.SetBool(this.parameter, value);
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public BoolParameter () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public BoolParameter (GameObject self) : base () {
            this.parameter = "New Bool";
            this.animator = self.GetComponent<Animator>();
        }

        /// <summary>
        /// User-defined conversion from BoolParameter to bool
        /// </summary>
        public static implicit operator bool (BoolParameter variable) {
            return variable.Value;
        }
    }

    #if !UNITY_4_0_0 && !UNITY_4_1 && !UNITY_4_2
    /// <summary>
    /// Store an Animation's trigger paramater.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Animator")]
    public class TriggerParameter : BoolVar {

        /// <summary>
        /// The target Animator.
        /// </summary>
        [Tooltip("The target Animator")]
        public Animator animator;

        /// <summary>
        /// The parameter name.
        /// </summary>
        [Tooltip("The parameter name")]
        public string parameter;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override bool Value {
            get {
                return false;
            } 
            set {
                if (value && animator != null) 
                    animator.SetTrigger(this.parameter);
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public TriggerParameter () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public TriggerParameter (GameObject self) : base () {
            this.parameter = "New Trigger";
            this.animator = self.GetComponent<Animator>();
        }

        /// <summary>
        /// User-defined conversion from TriggerParameter to bool
        /// </summary>
        public static implicit operator bool (TriggerParameter variable) {
            return variable.Value;
        }
    }

    #else
    
    /// <summary>
    /// Store an Animation's int paramater.
    /// </summary>
    [System.Serializable]
    [CustomVariable("Animator")]
    public class VectorParameter : Vector3Var {

        /// <summary>
        /// The target Animator.
        /// </summary>
        [Tooltip("The target Animator")]
        public Animator animator;

        /// <summary>
        /// The parameter name.
        /// </summary>
        [Tooltip("The parameter name")]
        public string parameter;

        /// <summary>
        /// Variable value.
        /// </summary>
        public override Vector3 Value {
            get {
                return animator != null ? animator.GetVector(parameter) : Vector3.zero;
            } 
            set {
                if (animator != null) 
                    animator.SetVector(this.parameter, value);
            }
        }

        /// <summary>
        /// Constructor for none variables.
        /// </summary>
        public VectorParameter () : base () {}

        /// <summary>
        /// Constructor for constants.
        /// <param name="self">The GameObject that owns the variable.</param>
        /// </summary>
        public VectorParameter (GameObject self) : base () {
            this.parameter = "New Vector";
            this.animator = self.GetComponent<Animator>();
        }

        /// <summary>
        /// User-defined conversion from VectorParameter to Vector3
        /// </summary>
        public static implicit operator Vector3 (VectorParameter variable) {
            return variable.Value;
        }
    }
    #endif
}
