//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

#if !UNITY_4 && !UNITY_4_1 && !UNITY_4_3 && !UNITY_4_5
using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Wrapper class for the InternalUnityEventState component.
    /// <summary>
    public class UnityEventState : InternalUnityEventState {}
}
#endif