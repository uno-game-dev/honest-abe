//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Wrapper class for the InternalMonoState component.
    /// <summary>
    [RequireComponent(typeof(StateMachine))]
    public class MonoState : InternalMonoState {}
}