//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Wrapper class for the InternalStateMachine component.
    /// <summary>
    [RequireComponent(typeof(Blackboard))]
    public class StateMachine : InternalStateMachine {}
}