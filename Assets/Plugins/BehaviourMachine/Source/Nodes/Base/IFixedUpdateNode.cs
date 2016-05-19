//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;

namespace BehaviourMachine {

    /// <summary> 
    /// Decorate ActionNodes with this interface when they should be ticked in the FixedUpdate.
    /// <seealso cref="BehaviourMachine.InternalActionState" />
    /// </summary>
    public interface IFixedUpdateNode {}
}