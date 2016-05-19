//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
	/// <summary>
	/// Prints a varable value to the Unity Console.
	/// </summary>
	[NodeInfo ( category = "Debug/",
				icon = "console.infoicon.sml",
				description = "Prints a varable value to the Unity Console")]
	public class LogVariable : ActionNode {

		/// <summary>
		/// The variable to print.
		/// <summary>
		[VariableInfo(tooltip = "The variable to print")]
		public Variable variable;

		/// <summary>
		/// The log type.
		/// <summary>
		[Tooltip("The log type")]
		public Log.LogType logType;

		public override void Reset () {
			variable = new Variable();
			logType = Log.LogType.Log;
		}

	    public override Status Update () {
	    	// Validate members
	    	if (variable.isNone)
	    		return Status.Error;

	    	switch (logType) {
	    		case Log.LogType.Log:
	    			Debug.Log(variable.genericValue, owner as UnityEngine.Object);
	    			break;
	    		case Log.LogType.LogWarning:
	    			Debug.LogWarning(variable.genericValue, owner as UnityEngine.Object);
	    			break;
	    		case Log.LogType.LogError:
	    			Debug.LogError(variable.genericValue, owner as UnityEngine.Object);
	    			break;
	    	}

	        return Status.Success;
	    }
	}
}