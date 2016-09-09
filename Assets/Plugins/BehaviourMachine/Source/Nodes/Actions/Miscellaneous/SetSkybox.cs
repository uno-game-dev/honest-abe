//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Sets a new skybox.
    /// </summary>
    [NodeInfo ( category = "Action/Miscellaneous/",
                icon = "Skybox",
                description = "Sets a new skybox",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/RenderSettings-skybox.html")]
    public class SetSkybox : ActionNode {

        /// <summary>
        /// The new skybox.
        /// </summary>
        [VariableInfo(tooltip = "The new skybox")]
    	public MaterialVar newSkybox;

        public override void Reset () {
            newSkybox = new ConcreteMaterialVar();
        }

        public override Status Update () {
            // Validate members
            if (newSkybox.isNone)
                return Status.Error;

            // Set the skybox
            RenderSettings.skybox = newSkybox.Value;

            return Status.Success;
        }
    }
}