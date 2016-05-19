//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourMachine {
	/// <summary>
	/// Adds a text comment, doesn't do anything, just for notes. Always returns the status "Returned Status".
	/// </summary>
	[NodeInfo ( category = "Debug/",
				icon = "console.infoicon.sml",
				description = "Adds a text comment, doesn't do anything, just for notes. Always returns the status \"Returned Status\"")]
	public class Comment : ActionNode {

		/// <summary>
		/// The status to be returned when the node is ticked.
		/// <summary>
		[Tooltip("The status to be returned when the node is ticked")]
		public Status returnedStatus;

		/// <summary>
		/// The comment text.
		/// <summary>
		[Tooltip("The comment text")]
		[NodeTextArea("Comment...", 3)]
		public string comment;

		public override void Reset () {
			returnedStatus = Status.Success;
			comment = string.Empty;
		}

	    public override Status Update () {
	        return returnedStatus;
	    }
	}
}