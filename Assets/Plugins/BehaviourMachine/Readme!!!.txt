Thank you for download Behaviour Machine!

If you have any questions, suggestions, comments or feature requests, please contact me at suppport@behaviourmachine.com
Join the support forum at http://behaviourmachine.com/Forum/

---------------
 How To Update
---------------

You can find a detailed description at www.behaviourmachine.com/user-manual/updating/

1. Backup your project.
2. Open a new empty scene (File -> New Scene).
3. Delete the plugins folder: "Plugins/BehaviourMachine", "Plugins/BehaviourMachine Indie", "Plugins/BehaviourMachine Pro", and "Plugins/Editor/BehaviourMachineEditor". Do not delete your GlobalBlackboard.prefab or you will lose any data stored in it, just move it outsoude BehaviourMachine folders. Remember to keep it inside a Resources folder.
4. Import the updated BehaviourMachine.unitypackage.

P.S.: If you are upgrading BehaviourMachine from the 1.2 version (or previous) you should to manually use the FsmEvent update. From the Preferences window (Unity -> Preferences -> BehaviourMachine) select the FsmEvent update button.

-----------------
 Version History
-----------------

1.4.2
NEW: Call BehaviourTree functions through Events from the new UI (TickFunctionFromUI function).

1.4.1:
NEW: DynamicList variable; stores a collection of values (aka List/Array).
NEW: Awake function node.
NEW: DynamicListAdd, DynamicListClear, DynamicListGetIndexOf, DynamicListGetCount, DynamicListInsert, DynamicListRemove, DynamicListRemoveAt action nodes.
NEW: DynamicListContains condition node.
NEW: DynamicListFor and DynamicListForEach decorator nodes.
NEW: LogVariable node; prints a variable value to the Unity console.
NEW: Normalized property for some OnGUI nodes.

1.4.0:
- IMPORTANT: To prevent accidentally data loss during updates it's a good parctice to have the GlobalBlackboard.prefab outside BehaviourMachine folders. Just move the "Assets/Plugins/BehaviourMachine/Resources" folder elsewhere in your project (The root folder "Assets/" is fine). If you already have an "Assets/Resources" folder then you can just move the GlobalBlackboard.prefab to this folder. Don't forget that the GlobalBlackboard.prefab should be inside a Resources folder.
NEW: Uniduino and UniOSC third party nodes for Indie and Pro.
NEW: Coroutine function node.
NEW: UnityEventState, a state that allows you to use the power of UnityEvents (For Unity 4.6+).
NEW: You can "call" a FunctionNode to tick their children via scripting (tree.TickFunction("FunctionNodeName")) or using the TickFunction and TickFunctionInSelf nodes.
NEW: FunctionNode is no more abstract, use this node to create a function that will only be ticked by you (not Unity) using the TickFunction nodes/method (see item above);
NEW: Improved performance for Update, LateUpdate, FixedUpdate and EveryFrame nodes.
NEW: Remap, IsMoving, IsRotating, AnimatorGetApplyRootMotion and AnimatorSetApplyRootMotion nodes.
NEW: Rich text in node name (thanks to Didin).
NEW: The StateMachine, BehaviourTree, ActionState and Blackboard Unity methods (Awake, OnEnable, OnDisable, etc) are now public and virtual to let you easily extend their functionalities.
NEW: Automatically creates a GlobalBlackboard.prefab if there is none in the project.
FIX: Moving nodes from Action/Mecanim category to Action/Animator category.
FIX: Sequence and Selector nodes were running twice when the GameObject is deactivate.
FIX: 'FLoat' typo in Blackboard menu (thanks to Slulego).
FIX: Vector3Normalize node (thanks to fiar).
FIX: Duplicated node scripts were crashing the editor.
FIX: Copying states were causing blackboard reference issues.
Fix: Dragging node scripts from the Project view were freezing Unity drag and drop system.

1.3.1:
NEW: AnyState.canTransitionToSelf property. If the AnyState's "Can Transition To Self" property is unchecked the transitions in the AnyState where the destination state is the enabled will be ignored; if this property is checked then the transitions in the AnyState where the destination state is enabled will not be ignored meaning that the enabled state can transition to itself.
FIX: StateBehaviour.SendEventTrigger() was moved to InternalBlackboard.SendEventTrigger().
FIX: Unity 5 build.

1.3.0:
- IMPORTANT: If you are upgrading from an older version you should to manually run the FsmEvent updater in the Preferences window: open the Preferences window ("Unity -> Preferences"), select the BehaviourMachine tab and click in the FsmEvent update button.
- NEW: The base class for all states is now the InternalStateBehaviour, instead of StateBehaviour.
- NEW: Improved InternalStateBehaviour.blackboard performance, now the blackboard is cached in Editor, no need to call GetComponent() during runtime.
- NEW: BehaviourTree.SendEvent(). Uses the supplied event on the tree, if fails it will send the event to all enabled states in the tree.
- FIX: Refactoring on StateBehaviour.SendEvent(), it will always process the event top-down starting from the root parent (no need to call root.SendEvent(...)) if it exists. Adding StateBehaviour.ProcessEvent() that will actually use the event, StateBehaviour.SendEvent() will just call ProcessEvent() on the root parent if it exists.
- FIX: Refactoring on the enable/disable state logic. ParentBehaviour.enabledSate property was removed, Now the ParentBehaviour uses the EnabledState, and DisableState callbacks. The StateMachine has the enabledState property to access the enabled state. The BehaviourTree has the enabledStates property to access all enabled states on the tree.
- FIX: ActionNode.Awake not being called after an assembly reload.
- FIX: Adding nodes during runtime is breaking ActionNode.OnTick.
- FIX: Improving performance on the Sequence and Selector nodes.
- FIX: SetSkybox node status.

1.2.0:
- NEW: StateMachine's concurrent state. You can set a concurrent state in a StateMachine that will be enabled when the StateMachine is enabled and disabled when the StateMachine is disabled.
- NEW: IsAxisButton, IsAxisButtonDown and IsAxisButtonUp, TransformDirection, InverseTransformDirection, IgnoreCollision nodes.
- NEW: StateBehaviour.OnTick virtual method called by a StateNode.
- FIX: ActionNode.OnGUI in final build.
- FIX: Remove StateMachine due to the EmptyState's dependence.
- FIX: Copy states.

1.0.1:
- NEW: IsButton, IsButtonDown and IsButtonUp nodes.
- FIX: BehaviourTree ResetStatus when disabling.
- FIX: Windows 7 Behaviour Window bug.

1.0:
- NEW: Initial version.

0.9.0:
- NEW: A lot of new sample scenes.
- NEW: A lot of new nodes.
- NEW: All console messages have a colored tag {b} for easy debugging.
- NEW: MonoState.target GameObject property and inspector combobox to easily select MonoBehaviours (thanks to PrimeDerektive).
- FIX: A lot of refactoring, adding new design patterns to facilitate changes in internal code. Runtime code are totally decoupled from editor!
- FIX: Moving files/folders for better project organization.
- FIX: Animator.rootMotion property not showing in the inspector (thanks to Derek).
- FIX: SetAudioClip always returning error (thanks to cfregin).
- FIX: Losing enum data when removing/adding a node's public field (thanks to cfregin).

0.5.0:
- NEW: Easy and powerful integration with other scripts and native objects (GetProperty/SetProperty nodes + direct access to properties/fields (static or instance) of any Unity Object via reflection.
- NEW: Redesigned sample scenes: added sounds, new features and more aesthetic elements.
- NEW: Serialization feature; suppport for abstract fields!
- NEW: Serialization feature; the type Variable on nodes can be serialized to ANY variable value/type, very powerful!
- NEW: Material and Texture options for GetProperty and SetProperty
- NEW: Enum support for the SetProperty node.
- NEW: ReloadLevel, TextureSet, MaterialSet, RandomSeed, RandomChild, FloatToInt, FloatToString, IntToFloat, IntToString, StringToInt, BoolToFloat, BoolToInt, BoolToString, BoolToVector3, BoolToGameObject, BoolToObject, BoolToMaterial, BoolToQuaternion, BoolToRect and BoolToTexture nodes.
- NEW: RandomSequence and RandomSelector now has a weight parameter for each child.
- NEW: Easy send system events! You can use state.SendEvent(SystemEvent.FINISHED) to send the FINISHED event to the state.
- NEW: Inspector node titlebar, looks and works exactly as the Unity component's titlebar.
- NEW: Video option added to the toolbar menu: 'Tools / BehaviourMachine / Help / Videos'.
- FIX: Single child error of the RandomSequence and RandomSelector nodes.
- FIX: Displaying ObjectType in the Inspector.
- FIX: SetProperty and GetProperty layout error on the Inspector.
- FIX: Duplicating/pasting nodes in runtime.
- FIX: Selecting the newly created node when dragging a script to a behaviour tree canvas.
- FIX: Changing StateBehaviour.hierarchyName to StateBehaviour.fullStateName.
- FIX: Now the StateBehaviour.blackboard property caches the component.

0.4.1:
- NEW: ParallelSequence and ParallelSelector nodes; Parallel was removed (use the ParallelSequence instead).
- NEW: Undo/Redo operations when dragging states in editor.
- NEW: Drag multiple states in editor.
- NEW: Free movement of the state in the editor, looks and behaves like the Animator window!
- FIX: Drag GameObjects/Objects to a variable value in the Blackboard View on the Behaviour window.

0.4.0:
- NEW: BehaviourTree's execution was redesigned, now following common conventions; see AIGameDev (http://aigamedev.com/).
- NEW: New Node API, with new virtual methods (OnEnable, OnDisable, Start, End, Update). ICallbackNode was removed.
- NEW: Wait, PrioritySelector, PrioritySequence, RandomSelector, RandomSequence nodes.
- NEW: New BehaviourTree's property (resetStatusOnDisable).
- NEW: The Update node in the ActionState was replaced by the EveryFrame node.
- NEW: Add transition popup with options for set event.
- FIX: Loop execution with composite nodes with running child.
- FIX: Improved runtime performance on OnMouseStay, Update, FixedUpdate, LateUpdate, OnTriggerStay/2D, OnCollisionStay/2D, OnMouseDrag nodes.
- FIX: Improved loading nodes on BehaviourTrees.
- FIX: Improved BehaviourTree's runtime performance (lightning fast).
- FIX: Improved CompositeNode Add/Remove/Insert methods.
- FIX: Reset node on inspector.
- FIX: IsAllBoolTrue and IsAnyBoolTrue error.
- FIX: Removing m_InstanceID warnning on WP8 build.

0.3.0:
- IMPORTANT: Upgrade from older versions will lose the variables from all Blackboards D:
- NEW: Mecanim parameters as variables!
- NEW: Redesigned GUI control for variables, no more Radio buttons.
- NEW: Selected variable shows up on combobox.
- NEW: Options to find and edit nodes script in the gear.
- NEW: Shader get/set nodes.
- NEW: Comment node.
- NEW: Per Second property in some math nodes.
- NEW: Copy/Paste/Cut/Duplicate/Delete keyboard shortcuts for ActionState and BehaviourTree nodes.
- NEW: Duplicate keyboard shortcut for states.
- NEW: Custom icons on the inspector (thanks to Macário).
- NEW: Redesigned sample scenes with description for all states.
- FIX: Improved node serialization!
- FIX: Improved node editor.
- FIX: Improved GUI nodes.
- FIX: Improved state Copy/Paste/Delete: copying/deleting FSMs will include its hierarchy, transitions will keep their relative info.
- FIX: Game GUI controls did not appear when there is no Behaviour window.
- FIX: TypeUtlity and EditorTypeUtility TypeInfo conflict (thanks to Tiaan Geldenhuys).
- FIX: FSM's start state and AnyState loses references on play mode.
- FIX: Array size less than zero.
- FIX: Cancel edit node name in Behaviour window.
- FIX: Using UnityEditorInternal.ReorderableList on the ActionStateEditor, Unity 4.5+.

0.2.4:
- NEW: More sample scenes.
- NEW: StateMachine and BehaviourTree description.
- FIX: Running status on FunctionNodes.
- FIX: Support for Unity 4.5.0.

0.2.3:
- NEW: Copy/Paste states.
- NEW: SendEventToAll, BroadcastEvent and SendEventUpwards.
- NEW: SetRigidbodyVelocity/SetRigidbody2DVelocity nodes.
- FIX: Windows Phone 8 Build.

0.2.2:
- NEW: Tween Nodes.
- FIX: Node Un-Selection.
- FIX: ActionNode.OnValidate in editor.

0.2.1:
- NEW: ActionState (New Visual Programming Component).
- NEW: ThirdPersonController sample scene.
- NEW: More UnityGUI nodes!
- FIX: SetDirectionSmooth.cs axis.
- FIX: DecoratorNode.cs status.

0.1.0:
- NEW: Initial version.
