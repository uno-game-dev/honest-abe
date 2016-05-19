//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Store system event ids.
    /// </summary>
    [Obsolete("Use the GlobalBlackboard class instead (e.g. SendEvent(GlobalBlackboard.FINISHED);)")]
    public class SystemEvent {

        /// <summary>
        /// The id of the APPLICATION_FOCUS system event.
        /// </summary>
    	public static readonly int APPLICATION_FOCUS = -1;

        /// <summary>
        /// The id of the APPLICATION_PAUSE system event.
        /// </summary>
        public static readonly int APPLICATION_PAUSE = -2;

        /// <summary>
        /// The id of the APPLICATION_QUIT system event.
        /// </summary>
        public static readonly int APPLICATION_QUIT = -3;

        /// <summary>
        /// The id of the BECAME_INVISIBLE system event.
        /// </summary>
        public static readonly int BECAME_INVISIBLE = -4;

        /// <summary>
        /// The id of the BECAME_VISIBLE system event.
        /// </summary>
        public static readonly int BECAME_VISIBLE = -5;

        /// <summary>
        /// The id of the COLLISION_ENTER system event.
        /// </summary>
        public static readonly int COLLISION_ENTER = -6;

        /// <summary>
        /// The id of the COLLISION_ENTER_2D system event.
        /// </summary>
        public static readonly int COLLISION_ENTER_2D = -7;

        /// <summary>
        /// The id of the COLLISION_EXIT system event.
        /// </summary>
        public static readonly int COLLISION_EXIT = -8;

        /// <summary>
        /// The id of the COLLISION_EXIT_2D system event.
        /// </summary>
        public static readonly int COLLISION_EXIT_2D = -9;

        /// <summary>
        /// The id of the FINISHED system event.
        /// </summary>
        public static readonly int FINISHED = -10;

        /// <summary>
        /// The id of the JOINT_BREAK system event.
        /// </summary>
        public static readonly int JOINT_BREAK = -11;

        /// <summary>
        /// The id of the MOUSE_ENTER system event.
        /// </summary>
        public static readonly int MOUSE_ENTER = -12;

        /// <summary>
        /// The id of the MOUSE_EXIT system event.
        /// </summary>
        public static readonly int MOUSE_EXIT = -13;

        /// <summary>
        /// The id of the MOUSE_DOWN system event.
        /// </summary>
        public static readonly int MOUSE_DOWN = -14;

        /// <summary>
        /// The id of the MOUSE_UP system event.
        /// </summary>
        public static readonly int MOUSE_UP = -15;

        /// <summary>
        /// The id of the MOUSE_UP_BUTTON system event.
        /// </summary>
        public static readonly int MOUSE_UP_BUTTON = -16;

        /// <summary>
        /// The id of the TRIGGER_ENTER system event.
        /// </summary>
        public static readonly int TRIGGER_ENTER = -17;

        /// <summary>
        /// The id of the TRIGGER_ENTER_2D system event.
        /// </summary>
        public static readonly int TRIGGER_ENTER_2D = -18;

        /// <summary>
        /// The id of the TRIGGER_EXIT system event.
        /// </summary>
        public static readonly int TRIGGER_EXIT = -19;

        /// <summary>
        /// The id of the TRIGGER_EXIT_2D system event.
        /// </summary>
        public static readonly int TRIGGER_EXIT_2D = -20;
    }
}