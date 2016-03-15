//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using BehaviourMachine;

namespace BehaviourMachineEditor {

    /// <summary>
    /// Wrapper class for MonoStateEditor.
    /// <seealso cref="BehaviourMachine.MonoState" />
    /// </summary>
    [CustomEditor(typeof(MonoState))]
    public class MonoStateEditor : InternalMonoStateEditor {}
}