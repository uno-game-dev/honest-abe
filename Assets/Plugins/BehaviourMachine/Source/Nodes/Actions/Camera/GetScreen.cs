using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Get screen information.
    /// </summary>
    [NodeInfo(  category = "Action/Camera/",
                icon = "Camera",
                description = "Get screen information",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Screen.html")]
    public class GetScreen : ActionNode {

    	/// <summary>
        /// Store the Screen.width.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Use", tooltip = "Store the Screen.width")]
        public FloatVar storeWidth;

        /// <summary>
        /// Store the Screen.width.
        /// </summary>
        [VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Use", tooltip = "Store the Screen.width")]
        public FloatVar storeHeight;

        
        public override void Reset () {
            storeWidth = new ConcreteFloatVar();
            storeHeight = new ConcreteFloatVar();
        }

        public override Status Update () {
            storeWidth.Value = Screen.width;
            storeHeight.Value = Screen.height;
            return Status.Success;
        }
    }
}