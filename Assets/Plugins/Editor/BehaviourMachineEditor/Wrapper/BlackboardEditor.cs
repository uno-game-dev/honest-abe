//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using BehaviourMachine;

namespace BehaviourMachineEditor {

    /// <summary>
    /// Wrapper class for BlackboardEditor.
    /// <seealso cref="BehaviourMachine.Blackboard" />
    /// </summary>
    [CustomEditor(typeof(Blackboard))]
    public class BlackboardEditor : InternalBlackboardEditor {}
}