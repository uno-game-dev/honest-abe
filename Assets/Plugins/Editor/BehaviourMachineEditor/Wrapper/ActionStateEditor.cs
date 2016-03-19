//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using BehaviourMachine;

namespace BehaviourMachineEditor {

    /// <summary>
    /// Wrapper class for ActionStateEditor.
    /// <seealso cref="BehaviourMachine.ActionState" />
    /// </summary>
    [CustomEditor(typeof(ActionState))]
    public class ActionStateEditor : InternalActionStateEditor {}
}