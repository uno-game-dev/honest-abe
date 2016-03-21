//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using BehaviourMachine;

namespace BehaviourMachineEditor {

    /// <summary>
    /// Wrapper class for AnyStateEditor.
    /// <seealso cref="BehaviourMachine.AnyState" />
    /// </summary>
    [CustomEditor(typeof(AnyState))]
    public class AnyStateEditor : InternalAnyStateEditor {}
}