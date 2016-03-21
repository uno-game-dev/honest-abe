//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using BehaviourMachine;

namespace BehaviourMachineEditor {

    /// <summary>
    /// Wrapper class for BehaviourTreeEditor.
    /// <seealso cref="BehaviourMachine.BehaviourTree" />
    /// </summary>
    [CustomEditor(typeof(BehaviourTree))]
    public class BehaviourTreeEditor : InternalBehaviourTreeEditor {}
}