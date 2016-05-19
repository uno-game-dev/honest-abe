//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
	/// <summary>
	/// Prints a message to the Unity Console.
	/// </summary>
	[NodeInfo ( category = "Debug/",
				icon = "console.infoicon.sml",
				description = "Prints a message to the Unity Console")]
	public class Log : ActionNode {

		public enum LogType {Log, LogWarning, LogError}

		/// <summary>
		/// The message to be displayed.
		/// <summary>
		[VariableInfo(tooltip = "The message to be displayed")]
		public StringVar message;

		/// <summary>
		/// The log type.
		/// <summary>
		[Tooltip("The log type")]
		public LogType logType;

		public override void Reset () {
			message = string.Empty;
			logType = LogType.Log;
		}

	    public override Status Update () {
	    	// Validate members
	    	if (message.isNone)
	    		return Status.Error;

	    	switch (logType) {
	    		case LogType.Log:
	    			Debug.Log(message.Value, tree);
	    			break;
	    		case LogType.LogWarning:
	    			Debug.LogWarning(message.Value, tree);
	    			break;
	    		case LogType.LogError:
	    			Debug.LogError(message.Value, tree);
	    			break;
	    	}

	        return Status.Success;
	    }
	}
}